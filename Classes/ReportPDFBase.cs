using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace OOTS.Classes
{
    public class ReportPDFBase : PdfPageEventHelper
    {
 

        #region Privates
        protected const string NewLine = "\n";
        protected Document l1;
        protected BaseFont fontTimes;
        protected PdfTemplate footerTemplate;
        protected PdfContentByte cb;


        // font definitions 
        protected iTextSharp.text.Font fontFooter;
        protected iTextSharp.text.Font fontGeneralText;
        protected iTextSharp.text.Font fontBoldText;
        protected iTextSharp.text.Font fontCellHeader;
        protected iTextSharp.text.Font fontLargeBoldText;

        protected PdfWriter wr;
        #endregion

        #region Properties
        #region PDFStream
        private MemoryStream _PDFStream = new MemoryStream();
        public MemoryStream PDFStream
        {
            get { return _PDFStream; }
            set
            {
                if (_PDFStream == value)
                    return;
                _PDFStream = value;

            }
        }
        #endregion
        public byte[] DocumentBytes;

        //TODO: Change the default properties


        #region FooterLines
        private List<string> _FooterLines = new List<string>();

        public List<string> FooterLines
        {
            get { return _FooterLines; }
            set
            {
                if (_FooterLines == value)
                    return;
                _FooterLines = value;
                
            }
        }
        #endregion



        #endregion

        #region CTOR
        public ReportPDFBase()
        {
            l1 = new Document(PageSize.A4);
            PDFStream = new MemoryStream();
            buildFonts();        
        }
        #endregion

        #region buildFonts
        
        private void buildFonts()
        {
            fontTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            fontFooter = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.ITALIC, BaseColor.DARK_GRAY);
            fontGeneralText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            fontBoldText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            fontCellHeader = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            fontLargeBoldText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 17, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        }
        #endregion

        #region NewParagraph
        /// <summary>
        /// Creates a new paragraph using the general font and 15f space after.
        /// </summary>
        /// <returns>paragraph object</returns>
        public Paragraph NewParagraph()
        {
            Paragraph p = new Paragraph();
            p.Font = fontGeneralText;
            p.Leading = 12f;
            p.SpacingAfter = 10f;
            return p;
        }
        /// <summary>
        /// Adds a simple paragraph with a single line of text.
        /// </summary>
        /// <param name="text">Content of paragraph</param>
        /// <returns></returns>
        public Paragraph NewParagraph(string text)
        {
            Paragraph p = new Paragraph(text,fontGeneralText);
            p.SpacingAfter = 10f;
            p.Leading = p.Font.Size + 1f;
            return p;
        }

        #endregion
        #region NewBoldParagraph
        /// <summary>
        /// used to make a title Paragraph
        /// </summary>
        /// <returns>Paragraph object</returns>
        public Paragraph NewBoldParagraph()
        {
            Paragraph p = new Paragraph( );
            p.Font = fontLargeBoldText;
            p.SpacingAfter = 10f;
            p.Leading = p.Font.Size + 1f;
            return p;
        }
        /// <summary>
        /// used to make a title paragraph.
        /// </summary>
        /// <param name="text">Text to include</param>
        /// <returns>Paragraph object</returns>
        public Paragraph NewBoldParagraph(string text)
        {
            Paragraph p = new Paragraph(text);
            p.Font = fontBoldText;
            p.Font.Size = 17;
            p.SpacingAfter = 15f;
            p.Leading = p.Font.Size + 1f;
            return p;
        }
        #endregion

        #region Paragraph Line
        /// <summary>
        /// produces an object that can be added to a paragraph
        /// </summary>
        /// <param name="Text">Text to add</param>
        /// <returns>phrase object</returns>
        public Phrase ParaLine(string Text)
        {
            return new Phrase(Text, fontGeneralText);
        }
        #endregion

        #region SubjectBlock
        
        public PdfPTable SubjectBlock(Paragraph para)
        {
            PdfPTable table = new PdfPTable(2);
            float[] widths = new float[] { 1f, 9f };
            table.SetWidths(widths);
            table.SpacingAfter = 20.0f;
            table.SpacingBefore = 30.0f;
            para.Leading = 12f;
            table.HorizontalAlignment = 1;
            // RE Line 
            table.AddCell(CellDataNoBorder(""));
            table.AddCell(CellDataNoBorder(para));
            return table;
        }

        public PdfPTable UserName(Paragraph para)
        {
            PdfPTable table = new PdfPTable(2);
            float[] widths = new float[] { 1f, 5f };
            table.SetWidths(widths);
            table.SpacingAfter = 5.0f;
            
            para.Leading = 0f;
            table.HorizontalAlignment = 0;
            // RE Line 
            table.AddCell(CellDataNoBorder(""));
            table.AddCell(CellDataNoBorder(para));
            return table;
        }
        #endregion

        #region  Add Footer
       
        public PdfTemplate AddAddressFooter()
        {

            PdfTemplate footerTemplate = cb.CreateTemplate(500, (FooterLines.Count * 45 + 10));
            // Write Footer address.
            footerTemplate.BeginText();
            BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            footerTemplate.SetFontAndSize(bf2, 11);
            footerTemplate.SetColorStroke(BaseColor.DARK_GRAY);
            footerTemplate.SetColorFill(BaseColor.GRAY);
            int al = -200; 
            int p=0; // current line
            int v;   // vertical positioning
            foreach (string FooterLine in FooterLines)
            {
                v = 45 - (15 * p);
                float widthoftext = 500.0f - bf2.GetWidthPoint(FooterLine, 11);
                footerTemplate.ShowTextAligned(al, FooterLine, widthoftext, v, 0);
                p++;
            }
            footerTemplate.EndText();

            return footerTemplate;
        }
        #endregion

        #region Second Page Footer
        /// <summary>
        /// Page footer for 2nd and remaining pages.  Only prints page number.
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <returns></returns>
        public PdfTemplate AddPageFooter(int PageNumber)
        {
            PdfTemplate footerTemplate = cb.CreateTemplate(500, 55);
             
            footerTemplate.BeginText();
            BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            footerTemplate.SetFontAndSize(bf2, 11);
            footerTemplate.SetColorStroke(BaseColor.DARK_GRAY);
            footerTemplate.SetColorFill(BaseColor.GRAY);
            int al = -200;
            footerTemplate.SetLineWidth(3);
            footerTemplate.LineTo(500, footerTemplate.YTLM);
            
            string texttoadd = "Page: " + PageNumber.ToString();
            float widthoftext = 500.0f - bf2.GetWidthPoint(texttoadd, 11);
            footerTemplate.ShowTextAligned(al, texttoadd, widthoftext, 30, 0);
            footerTemplate.EndText();

            return footerTemplate;
        }
       #endregion

        #region HeaderLogo
        /// <summary>
        /// Adds the header logo from the resource file.
        /// </summary>
        /// <returns>(IElement) Image</returns>
        public iTextSharp.text.Image HeaderLogo(string path)
        {
            //TODO: You will want to change the logo image here.  
            //      It is currently loaded into the resource file, but can be anywhere

            Bitmap bitmap = new Bitmap(path);
            iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance((System.Drawing.Image)bitmap, BaseColor.WHITE);
            // This scales the image to a usable size.  you can change this if your image does not look right.
            gif.ScaleAbsolute(125.0f, 50.0f);
            return gif;
        }
        #endregion

 

        #region CellDataNoBorder
        /// <summary>
        /// creates a PdfpCell without a border.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellDataNoBorder(string text)
        {
            Paragraph curphrase = new Paragraph(text, fontGeneralText);
            PdfPCell x = new PdfPCell(curphrase);
            x.Border = 0;
            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            return x;
        }
        /// <summary>
        /// creates a PdfpCell without a border.
        /// </summary>
        /// <param name="text">ItextSharp Paragraph containing data</param>
        /// <returns>PdfPCell Object</returns>
        public PdfPCell CellDataNoBorder(Paragraph text)
        {
            PdfPCell x = new PdfPCell(text);
            x.Border = 0;
            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            return x;
        }
        #endregion

        #region CellData
        /// <summary>
        /// creates a PdfpCell with a border.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellData(string text)
        {
            Paragraph curphrase = new Paragraph(text, fontGeneralText);
            PdfPCell x = new PdfPCell(curphrase);
            x.BackgroundColor = iTextSharp.text.BaseColor.WHITE ;
            return x;
        }
        #endregion

        #region CellHeader
        /// <summary>
        /// creates a PdfpCell Header Cell, bold text, light grey background.
        /// </summary>
        /// <param name="text">text to be included in the cell</param>
        /// <returns>PdfPCell object</returns>
        public PdfPCell CellHeader(string text)
        {
            Paragraph curphrase = new Paragraph(text, fontCellHeader);
            PdfPCell x = new PdfPCell(curphrase  );
            x.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            return x;
        }
        #endregion

        #region WriteREBlock
        /// <summary>
        /// Writes the REBlock
        /// </summary>
        /// <param name="block">Paragraph containing RE Content </param>
        /// <returns>(IElement) PdfPTable object</returns>
        public PdfPTable WriteREBlock(Paragraph block)
        {
            PdfPTable table = new PdfPTable(2);
            float[] widths = new float[] { 1f, 9f };

            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            table.DefaultCell.BorderWidth = 0;
            table.DefaultCell.BorderColor = iTextSharp.text.BaseColor.LIGHT_GRAY;

            // RE Line 

            table.AddCell(CellDataNoBorder("RE:"));
            table.AddCell(CellDataNoBorder(block));

            table.SpacingAfter = 10.0f;

            return table;
        }
        #endregion



        #region GenerateReportBase
       
        public void GenerateReportBase()
        {
            wr = PdfWriter.GetInstance(l1, PDFStream);
            l1.Open();
            cb = wr.DirectContent;
            wr.PageEvent = this;
            l1.SetMargins(l1.LeftMargin,
                l1.RightMargin,
                l1.TopMargin,
                l1.BottomMargin + AddAddressFooter().Height);
        }
        #endregion

        #region Events
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            Debug.WriteLine("On Page Start Called");
        }
        public override void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
        {
            base.OnParagraph(writer, document, paragraphPosition);
            Debug.WriteLine("On Paragraph.....");

        }
        #endregion

        #region OnEndPage
        /// <summary>
        /// OnEndPage - This sets up the page footer. Will use the footer block for first
        /// page and page number for subsequent page
        /// </summary>
        /// <param name="writer">Default parameter</param>
        /// <param name="document">Default parameter</param>
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            if (document.PageNumber > 1)
                cb.AddTemplate(AddPageFooter(document.PageNumber), 50, 50);
            else
                cb.AddTemplate(AddAddressFooter(), 50, 50);
        }
        #endregion

    }

    
}
