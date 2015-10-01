using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOTS.Models
{
  public  class FilesDownloadAuditTrailViewModel
    {
        public Guid FileID
        {
            get;

            set;

        }
        public String NiceNameOrAreaName
        {
            get;
            set;
        }

        public System.DateTime DateTimeDownloaded
        {
            get;

            set;

        }
    }
}
