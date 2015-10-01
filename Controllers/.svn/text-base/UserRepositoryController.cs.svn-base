using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using OOTS.Classes;
using System.Web.Security;
using OOTS.Web.Models;

namespace OOTS.Controllers
{
    [Authorize]
    public class UserRepositoryController : Controller
    {
        //
        // GET: /UserRepository/

        public ActionResult UserRepository()
        {
            string rootpath = UtilityOperations.GetOOTSRootPath(Server);
            return View(new FileModel(rootpath));
        }
        [GridAction]
        public ActionResult SelectFiles(string filePath)
        {
            string userName = Membership.GetUser().UserName;
            DocumentsOperations documentsOperations = new DocumentsOperations();
            IList<OOTS.Models.File> databasefiles = documentsOperations.GetFilesByUserName(userName);
            //return View(new GridModel<OOTS.Models.File>
            //{
            //    Total = files.Count,
            //    Data = files
            //});

            string rootpath = UtilityOperations.GetOOTSRootPath(Server);
            IList<FileModel> files = FileModel.GetFiles(filePath == "" || filePath == "/" ? rootpath : filePath);

            IList<FileModel> authorizedFiles = documentsOperations.FilterBasedOnAuthorizationsAndPopulateNiceNameAndDescription(files, databasefiles);
            return View(new GridModel<FileModel>
            {
                Total = authorizedFiles.Count,
                Data = authorizedFiles
            });
        }

        public ActionResult GetDocumentUserMenu(string path, List<string> list)
        {
            if (list != null && !string.IsNullOrEmpty(list[0]))
                ViewData["CheckedList"] = list;

            return PartialView("DocumentUserMenu");

        }
        public void DownloadFiles(string jlist)
        {
            List<string> list = null;
            list = UtilityOperations.StringToList(jlist, list);
            List<string> physicalPaths = new List<string>();
            List<string> virtualPaths = new List<string>();
            foreach (string path in list)
            {
                //string physicalPath = Server.MapPath(path);
                virtualPaths.Add(UtilityOperations.GetVirtualPath(path));
                physicalPaths.Add(path);
            }
            HttpContext httpContext = this.HttpContext.ApplicationInstance.Context;
            UtilityOperations.LogDocumentsDownloaded(virtualPaths);
            UtilityOperations.DownloadFiles(physicalPaths, httpContext);
        }
    }
}
