using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using OOTS.Classes;
using System.Web.Security;
using OOTS.Web.Models;
using System.IO;

namespace OOTS.Controllers
{
    [Authorize(Roles = "UserAdmin")]
    public class UserAdminRepositoryController : Controller
    {
        public ActionResult UserAdminRepository()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            List<SelectListItem> dropDownList = new List<SelectListItem>();
            foreach (MembershipUser item in users)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item.UserName, Value = item.ProviderUserKey.ToString() };
                dropDownList.Add(selectListItem);
            }
            string[] roles = System.Web.Security.Roles.GetAllRoles();
            List<SelectListItem> rolesDropDownList = new List<SelectListItem>();
            foreach (string item in roles)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item, Value = item };
                rolesDropDownList.Add(selectListItem);
            }
            ViewData["UsersData"] = dropDownList;
            ViewData["RolesData"] = rolesDropDownList;
            string rootpath = UtilityOperations.GetOOTSRootPath(Server);
            return View(new FileModel(rootpath));
        }

        public ActionResult ManageRoles()
        {
            string[] roles = System.Web.Security.Roles.GetAllRoles();
            List<SelectListItem> rolesDropDownList = new List<SelectListItem>();
            foreach (string item in roles)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item, Value = item };
                rolesDropDownList.Add(selectListItem);
            }

            ViewData["RolesData"] = rolesDropDownList;
            return View();
        }

        public ActionResult UsersAndRoles()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            List<SelectListItem> dropDownList = new List<SelectListItem>();
            foreach (MembershipUser item in users)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item.UserName, Value = item.UserName };
                dropDownList.Add(selectListItem);
            }
            string[] roles = System.Web.Security.Roles.GetAllRoles();
            List<SelectListItem> rolesDropDownList = new List<SelectListItem>();
            foreach (string item in roles)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item, Value = item };
                rolesDropDownList.Add(selectListItem);
            }
            ViewData["UsersData"] = dropDownList;
            ViewData["RolesData"] = rolesDropDownList;
            return View();
        }

        public ActionResult UsersExpiryManagement()
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

        public void SetUserExpiry(string userName, string expiryDate)
        {
            AuthenticationsAndAuthorizationsOperations authenticationsAndAuthorizationsOperations = new AuthenticationsAndAuthorizationsOperations();
            authenticationsAndAuthorizationsOperations.InsertExpiryDate(userName, Convert.ToDateTime(expiryDate));
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

        public ActionResult GetDocumentUserAdminMenu(string path, List<string> list)
        {
            if (list != null && !string.IsNullOrEmpty(list[0]))
                ViewData["CheckedList"] = list;

            return PartialView("DocumentUserAdminMenu");
        }
        public void DownloadFiles(string jlist)
        {
            List<string> list = null;
            list = UtilityOperations.StringToList(jlist, list);           
            HttpContext httpContext = this.HttpContext.ApplicationInstance.Context;
            UtilityOperations.DownloadFiles(list, httpContext);
        }

        public void AssignTheSelectedFilesToTheSelectedUser(string jlist, string userID)
        {
            List<string> list = null;
            list = UtilityOperations.StringToList(jlist, list);            
            foreach (string path in list)
            {
                AssignAccessToFileOrFolder(userID, path);
                AssignAccessToParentFolders(userID, path);
                AssignAccessToChildFilesorFolders(userID, path);
            }
        }

        private static void AssignAccessToFileOrFolder(string userID, string path)
        {
            DocumentsOperations documentsOperations = new DocumentsOperations();
            string virtualPath = UtilityOperations.GetVirtualPath(path);
            Guid fileID = documentsOperations.GetFileIDByVirtualPath(virtualPath);
            documentsOperations.InsertUsersFilesAuthorizations(new Guid(userID), fileID);
        }

        public void AssignAccessToParentFolders(string userID, string path)
        {
            DirectoryInfo parent = Directory.GetParent(path);
            string rootpath = UtilityOperations.GetOOTSRootPath(Server);
            if (parent.FullName == rootpath)
                return;
            else
            {
                AssignAccessToFileOrFolder(userID, parent.FullName);
                AssignAccessToParentFolders(userID, parent.FullName);
            }
        }

        public void AssignAccessToChildFilesorFolders(string userID, string path)
        {
            FileAttributes attr = System.IO.File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                foreach (string f in Directory.GetFiles(path))
                {
                    AssignAccessToFileOrFolder(userID, f);
                }
                foreach (string d in Directory.GetDirectories(path))
                {
                    AssignAccessToFileOrFolder(userID, d);
                    AssignAccessToChildFilesorFolders(userID, d);
                }
            }
        }

        public void AssignTheSelectedFilesToTheSelectedRole(string jlist, string roleName)
        {
            List<string> list = null;
            list = UtilityOperations.StringToList(jlist, list);
            DocumentsOperations documentsOperations = new DocumentsOperations();
            Guid roleID = documentsOperations.GetRoleIDByRoleName(roleName);
            foreach (string path in list)
            {
                AssignRoleAccessToFileOrFolder(roleID.ToString(), path);
                AssignRoleAccessToParentFolders(roleID.ToString(), path);
                AssignRoleAccessToChildFilesorFolders(roleID.ToString(), path);
            }
        }

        private void AssignRoleAccessToFileOrFolder(string roleID, string path)
        {
            DocumentsOperations documentsOperations = new DocumentsOperations();
            string virtualPath = UtilityOperations.GetVirtualPath(path);
            Guid fileID = documentsOperations.GetFileIDByVirtualPath(virtualPath);
            documentsOperations.InsertRolesFilesAuthorizations(new Guid(roleID), fileID);
        }

        public void AssignRoleAccessToParentFolders(string roleID, string path)
        {
            DirectoryInfo parent = Directory.GetParent(path);
            string rootpath = UtilityOperations.GetOOTSRootPath(Server);
            if (parent.FullName == rootpath)
                return;
            else
            {
                AssignRoleAccessToFileOrFolder(roleID, parent.FullName);
                AssignRoleAccessToParentFolders(roleID, parent.FullName);
            }
        }

        public void AssignRoleAccessToChildFilesorFolders(string roleID, string path)
        {
            FileAttributes attr = System.IO.File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                foreach (string f in Directory.GetFiles(path))
                {
                    AssignRoleAccessToFileOrFolder(roleID, f);
                }
                foreach (string d in Directory.GetDirectories(path))
                {
                    AssignRoleAccessToFileOrFolder(roleID, d);
                    AssignRoleAccessToChildFilesorFolders(roleID, d);
                }
            }
        }


        public void CreateRole(string newRoleName)
        {
            if (!Roles.RoleExists(newRoleName))
            {
                Roles.CreateRole(newRoleName);
            }

        }

        public void DeleteRole(string newRoleName)
        {
            Roles.DeleteRole(newRoleName, false);

        }

        public string[] CheckRolesForSelectedUser(string selectedUserName)
        {
            string[] selectedUsersRoles = Roles.GetRolesForUser(selectedUserName);
            return selectedUsersRoles;
        }

        public void AddUserToRole(string selectedUserName, string roleName)
        {
            Roles.AddUserToRole(selectedUserName, roleName);
        }

        public void RemoveUserFromRole(string selectedUserName, string roleName)
        {
            Roles.RemoveUserFromRole(selectedUserName, roleName);
        }

        public ActionResult GetRolesForTheSelectedUser(string UserListForDeletion)
        {
            string[] selectedRoles = Roles.GetRolesForUser(UserListForDeletion);
            List<SelectListItem> rolesDropDownList = new List<SelectListItem>();
            foreach (string item in selectedRoles)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item, Value = item };
                rolesDropDownList.Add(selectListItem);
            }
            return Json(rolesDropDownList);
        }

    }
}
