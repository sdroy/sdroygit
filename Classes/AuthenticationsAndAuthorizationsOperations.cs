using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOTS.Models;

namespace OOTS.Classes
{
    public class AuthenticationsAndAuthorizationsOperations
    {
        public void InsertExpiryDate(string UserName, DateTime expiryDate)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                aspnet_Users user = (from p in ode.aspnet_Users
                                     where p.UserName == UserName
                                     select p).First();
                if (user.aspnet_Profile == null)
                {
                    aspnet_Profile profile = new aspnet_Profile();
                    profile.UserId = user.UserId;
                    profile.LastUpdatedDate = DateTime.Now;
                    profile.PropertyNames = "ExpiryDate";
                    profile.PropertyValuesString = expiryDate.ToString();
                    Byte[] fg = new Byte[2];
                    profile.PropertyValuesBinary = fg;
                    ode.AddToaspnet_Profile(profile);
                }
                else
                {
                    user.aspnet_Profile.PropertyValuesString = expiryDate.ToString();
                }
                ode.SaveChanges();
            }
        }
        public DateTime GetExpiryDate(string UserName)
        {
            using (OOTSDBEntities ode = new OOTSDBEntities())
            {
                aspnet_Users user = (from p in ode.aspnet_Users 
                                     where p.UserName == UserName
                                     select p).First();
                if (user.aspnet_Profile != null)
                {
                    return Convert.ToDateTime(user.aspnet_Profile.PropertyValuesString);
                }
                else
                {
                    return DateTime.MaxValue;
                }
            }
        }
    }
}