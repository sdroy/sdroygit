using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOTS.Models;
using System.Web.Security;
using OOTS.Web.Models;

namespace OOTS.Classes
{
    public class DocumentsOperations
    {

        #region AuthorizationRelated
        public Guid GetUserIDByUserName(string username)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                var query = (from p in ode.aspnet_Users
                             where p.UserName == username
                             select p).First();
                return query.UserId;
            }
        }
        public Guid GetRoleIDByRoleName(string rolename)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                var query = (from p in ode.aspnet_Roles
                             where p.RoleName == rolename
                             select p).First();
                return query.RoleId;
            }
        }


        public void InsertUsersFilesAuthorizations(Guid userID, Guid fileID)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                File file = (from p in ode.Files
                             where p.ID == fileID
                             select p).First();
                aspnet_Users user = (from p in ode.aspnet_Users
                                     where p.UserId == userID
                                     select p).First();
                if (!file.aspnet_Users.Contains(user))
                {
                    file.aspnet_Users.Add(user);
                    ode.SaveChanges();
                }
            }
        }
        public void InsertRolesFilesAuthorizations(Guid roleID, Guid fileID)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                File file = (from p in ode.Files
                             where p.ID == fileID
                             select p).First();
                aspnet_Roles role = (from p in ode.aspnet_Roles
                                     where p.RoleId == roleID
                                     select p).First();
                if (!file.aspnet_Roles.Contains(role))
                {
                    file.aspnet_Roles.Add(role);
                    ode.SaveChanges();
                }
            }

        }

        public void PopulateNiceNameAndDescription(IList<FileModel> files)
        {
            List<string> virttualPaths = new List<string>();
            foreach (var item in files)
            {
                virttualPaths.Add(UtilityOperations.GetVirtualPath(item.FullPath));
            }
            DocumentsOperations documentsOperations = new DocumentsOperations();
            List<OOTS.Models.File> filelist = documentsOperations.GetFilesByVirtualPaths(virttualPaths);
            foreach (var item in files)
            {
                List<OOTS.Models.File> tempfilelist = filelist.Where(x => x.Name == item.Name).ToList();
                if (tempfilelist.Count != 0)
                {
                    item.NiceNameOrAreaName = tempfilelist[0].NiceNameOrAreaName;
                    item.Description = tempfilelist[0].Description;
                }
            }

        }

        public IList<FileModel> FilterBasedOnAuthorizationsAndPopulateNiceNameAndDescription(IList<FileModel> files, IList<OOTS.Models.File> filelist)
        {
            IList<FileModel> resultantfiles = new List<FileModel>();
            List<string> virttualPaths = new List<string>();      
            foreach (var item in files)
            {
                String virtualPath = UtilityOperations.GetVirtualPath(item.FullPath);
                List<OOTS.Models.File> tempfilelist = filelist.Where(x => x.VirtualPath == virtualPath).ToList();
                if (tempfilelist.Count != 0)
                {
                    item.NiceNameOrAreaName = tempfilelist[0].NiceNameOrAreaName;
                    item.Description = tempfilelist[0].Description;
                    resultantfiles.Add(item);
                }
            }

            return resultantfiles;

        }

        #endregion
        #region AuditTrials

        public void InsertUserLoginAuditTrails(Guid UserID)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                UserLoginAuditTrail userLoginAuditTrail = new UserLoginAuditTrail();
                userLoginAuditTrail.UserID = UserID;
                userLoginAuditTrail.DateTimeLogged = DateTime.Now;
                ode.AddToUserLoginAuditTrails(userLoginAuditTrail);
                ode.SaveChanges();
            }
        }

        public List<UserLoginAuditTrail> GetUserActivityAuditTrailsBySpecificUser(String userName)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                ode.ContextOptions.LazyLoadingEnabled = false;
                List<UserLoginAuditTrail> query = (from p in ode.UserLoginAuditTrails.Include("aspnet_Users")
                                                   where p.aspnet_Users.UserName == userName
                                                   select p).ToList<UserLoginAuditTrail>();
                return query;
            }
        }

        public void InsertFilesDownloadAuditTrails(Guid FileID, Guid UserID)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {

                FilesDownloadAuditTrail filesDownloadAuditTrail = new FilesDownloadAuditTrail();
                filesDownloadAuditTrail.UserID = UserID;
                filesDownloadAuditTrail.FileID = FileID;
                filesDownloadAuditTrail.DateTimeDownloaded = DateTime.Now;
                ode.AddToFilesDownloadAuditTrails(filesDownloadAuditTrail);
                ode.SaveChanges();
            }
        }

        public List<FilesDownloadAuditTrail> GetTotalFilesDownloadedAuditTrails()
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                ode.ContextOptions.LazyLoadingEnabled = false;
                List<FilesDownloadAuditTrail> query = (from p in ode.FilesDownloadAuditTrails.Include("File")
                                                       select p).ToList<FilesDownloadAuditTrail>();
                return query;
            }
        }

        public List<FilesDownloadAuditTrail> GetFilesDownloadedAuditTrailsBySpecificUser(String userName)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                ode.ContextOptions.LazyLoadingEnabled = false;
                List<FilesDownloadAuditTrail> query = (from p in ode.FilesDownloadAuditTrails.Include("File")
                                                       where p.aspnet_Users.UserName == userName
                                                       select p).ToList<FilesDownloadAuditTrail>();
                return query;
            }
        }

        #endregion
        #region FileOperations

        public void InsertFile(string fileName, string virtualPath, string niceNameOrAreaName, string description)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                File file = new File();
                file.ID = Guid.NewGuid();
                file.Name = fileName;
                file.NiceNameOrAreaName = niceNameOrAreaName;
                file.Description = description;
                file.VirtualPath = virtualPath;
                file.DateTimeUploaded = DateTime.Now;
                ode.AddToFiles(file);
                ode.SaveChanges();
            }
        }

        public List<File> GetFilesByUserName(string userName)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                ode.ContextOptions.LazyLoadingEnabled = false;
                List<File> fileset1 = (from p in ode.aspnet_Users
                                       from q in p.Files
                                       where p.UserName == userName
                                       select q).ToList<File>();

                string[] roles = Roles.GetRolesForUser(userName);

                List<File> fileset2 = (from p in ode.aspnet_Roles
                                       from q in p.Files
                                       where roles.Contains(p.RoleName)
                                       select q).ToList<File>();
                fileset1.AddRange(fileset2);
                return fileset1;
            }
        }

        public List<File> GetFiles()
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                ode.ContextOptions.LazyLoadingEnabled = false;
                List<File> query = (from p in ode.Files
                                    select p).ToList<File>();
                return query;
            }
        }

        public List<File> GetFilesByVirtualPaths(List<string> virtualPaths)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                ode.ContextOptions.LazyLoadingEnabled = false;
                List<File> query = (from p in ode.Files
                                    where virtualPaths.Contains(p.VirtualPath)
                                    select p).ToList<File>();
                return query;
            }
        }


        public Guid GetFileIDByVirtualPath(string virtualpath)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                var query = (from p in ode.Files
                             where p.VirtualPath == virtualpath
                             select p).First();
                return query.ID;
            }

        }


        public void RenameFile(string newname, string previousVirtualPath, string newVirtualPath)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                File file = (from p in ode.Files
                             where p.VirtualPath == previousVirtualPath
                             select p).First();
                file.Name = newname;
                file.VirtualPath = newVirtualPath;
                ode.SaveChanges();
            }
        }

        public void DeleteFile(string previousVirtualPath)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                File file = (from p in ode.Files
                             where p.VirtualPath == previousVirtualPath
                             select p).First();
                file.aspnet_Users.Clear();
                file.aspnet_Roles.Clear();
                ode.DeleteObject(file);
                ode.SaveChanges();
            }
        }
        #endregion
    }
}