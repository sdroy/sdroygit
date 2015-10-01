using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOTS.Classes;
using System.Web.Security;
using OOTS.Models;
using Telerik.Web.Mvc;

namespace OOTS.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reports()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            List<SelectListItem> dropDownList = new List<SelectListItem>();
            foreach (MembershipUser item in users)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item.UserName, Value = item.UserName };
                dropDownList.Add(selectListItem);
            }

            ViewData["UsersData"] = dropDownList;
            return View();
        }

        public ActionResult ReportsInTable()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            List<SelectListItem> dropDownList = new List<SelectListItem>();
            foreach (MembershipUser item in users)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item.UserName, Value = item.UserName };
                dropDownList.Add(selectListItem);
            }

            ViewData["UsersData"] = dropDownList;
            return View();
        }


        public FileContentResult GenerateTotalDocumentsDownloadedReport()
        {
            String physicalPath = GetTheLogoPath();
            ReportOperations reportOperations = new ReportOperations();
            Byte[] bytestream = reportOperations.GenerateReportForTotalDocumentsDownloaded(physicalPath);
            return File(bytestream, "application/pdf", "TotalDocumentsDownloaded" + "Report.pdf");
        }

        private String GetTheLogoPath()
        {
            String logoPath = @"\Content\images\logo225x90.gif";
            String physicalPath = Server.MapPath(logoPath);
            return physicalPath;
        }


        public FileContentResult GenerateDocumentsDownloadedReportForASpecificUser(String userName)
        {
            String physicalPath = GetTheLogoPath();
            ReportOperations reportOperations = new ReportOperations();
            Byte[] bytestream = reportOperations.GenerateReportForDocumentsDownloadedBySpecificUser(physicalPath, userName);
            return File(bytestream, "application/pdf", userName + "_DocumentsDownloaded" + "Report.pdf");
        }


        public FileContentResult GenerateUserActivityReportForASpecificUser(String userName)
        {
            String physicalPath = GetTheLogoPath();
            ReportOperations reportOperations = new ReportOperations();
            Byte[] bytestream = reportOperations.GenerateReportForUserActivity(physicalPath, userName);
            return File(bytestream, "application/pdf", userName + "_UserActivity" + "Report.pdf");
        }


        [GridAction]
        public ActionResult GetTotalDocumentsDownloadedReport(string isLoad)
        {
            if (isLoad == "True")
            {
                DocumentsOperations documentsOperations = new DocumentsOperations();
                List<FilesDownloadAuditTrail> filesDownloadAuditTrails = documentsOperations.GetTotalFilesDownloadedAuditTrails();
                List<FilesDownloadAuditTrailViewModel> filesDownloadAuditTrailViewModeldata = new List<FilesDownloadAuditTrailViewModel>();
                foreach (FilesDownloadAuditTrail item in filesDownloadAuditTrails)
                {
                    FilesDownloadAuditTrailViewModel filesDownloadAuditTrailViewModel = new FilesDownloadAuditTrailViewModel();
                    filesDownloadAuditTrailViewModel.FileID = item.FileID;
                    filesDownloadAuditTrailViewModel.NiceNameOrAreaName = item.File.NiceNameOrAreaName;
                    filesDownloadAuditTrailViewModel.DateTimeDownloaded = item.DateTimeDownloaded;
                    filesDownloadAuditTrailViewModeldata.Add(filesDownloadAuditTrailViewModel);
                }
                return View(new GridModel<FilesDownloadAuditTrailViewModel>
               {
                   Total = filesDownloadAuditTrailViewModeldata.Count,
                   Data = filesDownloadAuditTrailViewModeldata
               });
            }
            else
            {
                List<FilesDownloadAuditTrailViewModel> filesDownloadAuditTrails = new List<FilesDownloadAuditTrailViewModel>();
                return View(new GridModel<FilesDownloadAuditTrailViewModel>
                {
                    Total = filesDownloadAuditTrails.Count,
                    Data = filesDownloadAuditTrails
                });
            }
        }


        [GridAction]
        public ActionResult GetDocumentsDownloadedReportBySpecificUser(string userName)
        {
            if (!String.IsNullOrEmpty(userName))
            {
                DocumentsOperations documentsOperations = new DocumentsOperations();
                List<FilesDownloadAuditTrail> filesDownloadAuditTrails = documentsOperations.GetFilesDownloadedAuditTrailsBySpecificUser(userName);
                List<FilesDownloadAuditTrailViewModel> filesDownloadAuditTrailViewModeldata = new List<FilesDownloadAuditTrailViewModel>();
                foreach (FilesDownloadAuditTrail item in filesDownloadAuditTrails)
                {
                    FilesDownloadAuditTrailViewModel filesDownloadAuditTrailViewModel = new FilesDownloadAuditTrailViewModel();
                    filesDownloadAuditTrailViewModel.FileID = item.FileID;
                    filesDownloadAuditTrailViewModel.NiceNameOrAreaName = item.File.NiceNameOrAreaName;
                    filesDownloadAuditTrailViewModel.DateTimeDownloaded = item.DateTimeDownloaded;
                    filesDownloadAuditTrailViewModeldata.Add(filesDownloadAuditTrailViewModel);
                }
                return View(new GridModel<FilesDownloadAuditTrailViewModel>
                {
                    Total = filesDownloadAuditTrailViewModeldata.Count,
                    Data = filesDownloadAuditTrailViewModeldata
                });
            }
            else
            {
                List<FilesDownloadAuditTrailViewModel> filesDownloadAuditTrails = new List<FilesDownloadAuditTrailViewModel>();
                return View(new GridModel<FilesDownloadAuditTrailViewModel>
                {
                    Total = filesDownloadAuditTrails.Count,
                    Data = filesDownloadAuditTrails
                });
            }
        }

        [GridAction]
        public ActionResult GetUserActivityReportForASpecificUser(String userName)
        {
            if (!String.IsNullOrEmpty(userName))
            {
                DocumentsOperations documentsOperations = new DocumentsOperations();
                List<UserLoginAuditTrail> userActivityAuditTrails = documentsOperations.GetUserActivityAuditTrailsBySpecificUser(userName);
                List<UserLoginAuditTrailViewModel> UserLoginAuditTrailViewModeldata = new List<UserLoginAuditTrailViewModel>();
                foreach (UserLoginAuditTrail item in userActivityAuditTrails)
                {
                    UserLoginAuditTrailViewModel userLoginAuditTrailViewModelentry = new UserLoginAuditTrailViewModel();
                    userLoginAuditTrailViewModelentry.UserName = item.aspnet_Users.UserName;
                    userLoginAuditTrailViewModelentry.DateTimeLogged = item.DateTimeLogged;
                    UserLoginAuditTrailViewModeldata.Add(userLoginAuditTrailViewModelentry);
                }
                return View(new GridModel<UserLoginAuditTrailViewModel>
                {
                    Total = UserLoginAuditTrailViewModeldata.Count,
                    Data = UserLoginAuditTrailViewModeldata
                });
            }
            else
            {
                List<UserLoginAuditTrailViewModel> UserLoginAuditTrailViewModeldata = new List<UserLoginAuditTrailViewModel>();
                return View(new GridModel<UserLoginAuditTrailViewModel>
                {
                    Total = UserLoginAuditTrailViewModeldata.Count,
                    Data = UserLoginAuditTrailViewModeldata
                });
            }
        }

    }
}
