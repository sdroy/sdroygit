using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace OOTS.Classes
{
    public class UtilityOperations
    {
        public static string GetOOTSRootPath(HttpServerUtilityBase server)
        {
            string rootpath = server.MapPath("~/OOTSFiles");
            return rootpath;
        }
        public static string GetVirtualPath(string physicalPath)
        {
            string applicationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            string url = physicalPath.Substring(applicationPath.Length).Replace('\\', '/').Insert(0, "~/");
            return (url);
        }

        public static void DownloadFiles(List<string> archives, HttpContext httpContext)
        {
            if (archives.Count == 0)
                return;
            FileAttributes attr1 = System.IO.File.GetAttributes(archives[0]);
            if (archives.Count == 1 && ((attr1 & FileAttributes.Directory) != FileAttributes.Directory))
            {
                string filename = Path.GetFileName(archives[0]);
                httpContext.Response.Buffer = true;
                httpContext.Response.Clear();
                httpContext.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                httpContext.Response.ContentType = "application/octet-stream";
                httpContext.Response.WriteFile(archives[0]);
            }
            else
            {
                string zipName = String.Format("archive-{0}.zip",
                                      DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                httpContext.Response.Buffer = true;
                httpContext.Response.Clear();
                httpContext.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                httpContext.Response.ContentType = "application/zip";
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    foreach (string path in archives)
                    {
                        try
                        {
                            FileAttributes attr = System.IO.File.GetAttributes(path);

                            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                                zip.AddDirectory(path, Path.GetFileNameWithoutExtension(path));
                            else
                                zip.AddFile(path, "");
                        }
                        catch (Exception)
                        {
                        }
                    }
                    zip.Save(httpContext.Response.OutputStream);
                }
            }
        }

        public static List<string> StringToList(string jlist, List<string> list)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            list = serializer.Deserialize<List<string>>(jlist);
            return list;
        }

        public static void LogDocumentsDownloaded(List<string> virtualPaths)
        {
            string userID = System.Web.Security.Membership.GetUser().ProviderUserKey.ToString();
            DocumentsOperations documentsOperations = new DocumentsOperations();
            foreach (string path in virtualPaths)
            {
                Guid fileID = documentsOperations.GetFileIDByVirtualPath(path);
                documentsOperations.InsertFilesDownloadAuditTrails(fileID,new Guid(userID));
            }
             
        }

        public static DateTime GetExpiryDate()
        {
            string userName = System.Web.Security.Membership.GetUser().UserName;
            AuthenticationsAndAuthorizationsOperations AuthenticationsAndAuthorizationsOperations = new AuthenticationsAndAuthorizationsOperations();
            DateTime expirydate = AuthenticationsAndAuthorizationsOperations.GetExpiryDate(userName);
            return expirydate;
        }
    }
}