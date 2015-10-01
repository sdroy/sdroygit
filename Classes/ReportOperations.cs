using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OOTS.Models;

namespace OOTS.Classes
{
    public class ReportOperations : ReportPDFBase
    {

        public byte[] GenerateReportForTotalDocumentsDownloaded(string physicalPath)
        {
            GenerateReportBase();
            l1.Add(HeaderLogo(physicalPath));
            l1.Add(SubjectBlock(new Paragraph("Report Name: Total Documents Downloaded with Dates and Times")));            
            PdfPTable table = new PdfPTable(2);
            table.AddCell(CellHeader("DocumentName"));
            table.AddCell(CellHeader("DateTime"));

            DocumentsOperations documentsOperations = new DocumentsOperations();
            List<FilesDownloadAuditTrail> filesDownloadAuditTrails = documentsOperations.GetTotalFilesDownloadedAuditTrails();
            foreach (FilesDownloadAuditTrail item in filesDownloadAuditTrails)
            {
                table.AddCell(CellData(item.File.Name));
                table.AddCell(CellData(item.DateTimeDownloaded.ToString()));
            }

            l1.Add(table);
            FooterLines.Add("DateTime: " + DateTime.Now.ToString());
            l1.Close();
            DocumentBytes = PDFStream.GetBuffer();
            return DocumentBytes;
        }

        public byte[] GenerateReportForDocumentsDownloadedBySpecificUser(string physicalPath, string userName)
        {
            GenerateReportBase();
            l1.Add(HeaderLogo(physicalPath));
            l1.Add(SubjectBlock(new Paragraph("Report Name: Documents Downloaded with Dates and Times")));
            l1.Add(UserName(new Paragraph("User Name:  " + userName)));
            PdfPTable table = new PdfPTable(2);
            table.AddCell(CellHeader("DocumentName"));
            table.AddCell(CellHeader("DateTime"));

            DocumentsOperations documentsOperations = new DocumentsOperations();
            List<FilesDownloadAuditTrail> filesDownloadAuditTrails = documentsOperations.GetFilesDownloadedAuditTrailsBySpecificUser(userName);
            foreach (FilesDownloadAuditTrail item in filesDownloadAuditTrails)
            {
                table.AddCell(CellData(item.File.Name));
                table.AddCell(CellData(item.DateTimeDownloaded.ToString()));
            }

            l1.Add(table);
            FooterLines.Add("DateTime: " + DateTime.Now.ToString());
            l1.Close();
            DocumentBytes = PDFStream.GetBuffer();
            return DocumentBytes;
        }

        public byte[]   GenerateReportForUserActivity(string physicalPath, string userName)
        {
            GenerateReportBase();
            l1.Add(HeaderLogo(physicalPath));
            l1.Add(SubjectBlock(new Paragraph("Report Name: User Activity with Dates and Times")));
            l1.Add(UserName(new Paragraph("User Name:  " + userName)));
            PdfPTable table = new PdfPTable(2);
            table.AddCell(CellHeader("UserName"));
            table.AddCell(CellHeader("DateTime"));

            DocumentsOperations documentsOperations = new DocumentsOperations();
            List<UserLoginAuditTrail> userActivityAuditTrails = documentsOperations.GetUserActivityAuditTrailsBySpecificUser(userName);
            foreach (UserLoginAuditTrail item in userActivityAuditTrails)
            {
                table.AddCell(CellData(item.aspnet_Users.UserName));
                table.AddCell(CellData(item.DateTimeLogged.ToString()));
            }

            l1.Add(table);
            FooterLines.Add("DateTime: " + DateTime.Now.ToString());
            l1.Close();
            DocumentBytes = PDFStream.GetBuffer();
            return DocumentBytes;
        }
    }
}