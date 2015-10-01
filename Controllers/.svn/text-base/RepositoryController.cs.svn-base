using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOTS.Web.Models;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.UI;
using OOTS.Classes;

namespace OOTS.Web.Controllers
{
    [Authorize(Roles = "DocumentAdmin")]
    public class RepositoryController : Controller
    {
        #region Common
      

        public ActionResult Repository()
        {
            string rootpath = UtilityOperations.GetOOTSRootPath(Server);
            return View(new FileModel(rootpath));
        }

        #endregion

        #region Upload and Download
        public ActionResult Upload(string path, FormCollection collection)
        {
            if (string.IsNullOrEmpty(path) || path == "/") path = UtilityOperations.GetOOTSRootPath(Server);
            string niceName = collection["txtNiceName"];
            string description = collection["txtDescription"];
            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];
                if (file.ContentLength > 0)
                {
                    string filePath = Path.Combine(path
                        , Path.GetFileName(file.FileName));
                    DocumentsOperations documentsOperations = new DocumentsOperations();
                    string virtualPath = UtilityOperations.GetVirtualPath(filePath);
                    documentsOperations.InsertFile(file.FileName, virtualPath, niceName, description);
                    file.SaveAs(filePath);
                    return RedirectToAction("Repository", "Repository");
                }
            }

            return View();
        }





        public void DownloadFile(string file)
        {
            List<string> archives = new List<string>();
            archives.Add(file);
            HttpContext httpContext = this.HttpContext.ApplicationInstance.Context;
            UtilityOperations.DownloadFiles(archives, httpContext);
        }
        public void DownloadFiles(string jlist)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<string> list = serializer.Deserialize<List<string>>(jlist);
            List<string> archives = new List<string>();
            foreach (string path in list)
            {
                archives.Add(path);
            }


            HttpContext httpContext = this.HttpContext.ApplicationInstance.Context;
            UtilityOperations.DownloadFiles(archives, httpContext);

        }
        #endregion

        #region FileManagement

        public JsonResult CreateNewFolder(string path, String foldername)
        {
            string decodedPath = DecodePath(path);
            String folderPath = Path.Combine(decodedPath, foldername);
            if (folderPath != "\\")
                if (!Directory.Exists(folderPath))
                {
                    DocumentsOperations documentsOperations = new DocumentsOperations();
                    string virtualPath = UtilityOperations.GetVirtualPath(folderPath);
                    documentsOperations.InsertFile(foldername, virtualPath, "", "");
                    Directory.CreateDirectory(folderPath);
                }
                else
                    folderPath = "";
            return new JsonResult() { Data = decodedPath };
        }

        private string DecodePath(string path)
        {
            if (string.IsNullOrEmpty(path) || path == "/") path = UtilityOperations.GetOOTSRootPath(Server);
            string decodedPath = FileModel.Decode(path);
            return decodedPath;
        }

        public void Rename(string jlist, string path, string newname)
        {
            string decodedPath = DecodePath(path);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<string> list = serializer.Deserialize<List<string>>(jlist);
            string itempath = list[0];
            FileAttributes attr = System.IO.File.GetAttributes(itempath);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                if (Directory.Exists(itempath))
                {
                    Directory.Move(itempath, Path.Combine( decodedPath , newname));
                }
            }
            else
            {
                if (System.IO.File.Exists(itempath))
                {
                    string newItemPath = Path.Combine(decodedPath, newname);
                    string previousVirtualPath = UtilityOperations.GetVirtualPath(itempath);//.Replace("~/", "~//"); ;
                    string newVirtualPath = UtilityOperations.GetVirtualPath(newItemPath);
                    DocumentsOperations documentsOperations = new DocumentsOperations();
                    documentsOperations.RenameFile(newname, previousVirtualPath, newVirtualPath);
                    System.IO.File.Move(itempath, newItemPath);

                }
            }
        }


        public void DeleteFiles(string jlist)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<string> list = serializer.Deserialize<List<string>>(jlist);
            List<string> archives = new List<string>();
            foreach (string path in list)
            {

                FileAttributes attr = System.IO.File.GetAttributes(path);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true); //delete folder and all subdirectories 
                        // return RedirectToAction("Repository", "Repository");
                    }
                }
                else
                {
                    if (System.IO.File.Exists(path))
                    {
                        string previousVirtualPath = UtilityOperations.GetVirtualPath(path);//.Replace("~/", "~//");
                        DocumentsOperations documentsOperations = new DocumentsOperations();
                        documentsOperations.DeleteFile(previousVirtualPath);
                        System.IO.File.Delete(path);
                        // return RedirectToAction("Repository", "Repository");
                    }
                }
            }

            //return View();

        }

        public ActionResult GetDocumentAdminMenu(string path, List<string> list)
        {
            if (list != null && !string.IsNullOrEmpty(list[0]))
                ViewData["CheckedList"] = list;
            if (path == "" || path == "/")
                return PartialView("DocumentAdminMenu", new FileModel());
            else
            {
                DirectoryInfo di = new DirectoryInfo(path);
                return PartialView("DocumentAdminMenu", new FileModel(di));
            }
        }

        public JsonResult GetFiles(string path)
        {
            IList<FileModel> files = FileModel.GetFiles(path);
            return new JsonResult() { Data = files };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubFoldersLoading(TreeViewItem node)
        {
            string filePath = node.Value;
            IList<FileModel> subFolders = FileModel.GeFolders(filePath);
            IEnumerable nodes = from item in subFolders
                                select new TreeViewItem
                                {
                                    Text = item.Name,
                                    Value = item.FullPath,
                                    ImageUrl = Url.Content("~/Content/Images/" + item.Category.ToString() + ".png"),
                                    LoadOnDemand = true,
                                    Enabled = true
                                };
            return new JsonResult { Data = nodes };
        }

        [GridAction]
        public ActionResult SelectFiles(string filePath)
        {
            string rootpath = UtilityOperations.GetOOTSRootPath(Server);
            IList<FileModel> files = FileModel.GetFiles(filePath == "" || filePath == "/" ? rootpath : filePath);
            DocumentsOperations documentsOperations = new DocumentsOperations();
            documentsOperations.PopulateNiceNameAndDescription(files);
            return View(new GridModel<FileModel>
            {
                Total = files.Count,
                Data = files
            });
        }

        

        #endregion
    }
}
