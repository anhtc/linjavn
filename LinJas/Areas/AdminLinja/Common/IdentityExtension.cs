using System;
using LinJas.Models;
using log4net;
using Newtonsoft.Json;
using System.Security.Principal;

namespace LinJas.Areas.AdminLinja.Common
{
    public static class IdentityExtension
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(IdentityExtension));

        public static string GetUserName(this IIdentity identity)
        {
            try
            {
                var userName = string.Empty;

                if (!string.IsNullOrEmpty(identity.Name))
                {
                    var identityModel = JsonConvert.DeserializeObject<IdentityModel>(identity.Name);
                    if (identityModel != null)
                    {
                        userName = identityModel.UserName;
                    }
                }

                return userName;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        public static string GetEmail(this IIdentity identity)
        {
            try
            {
                var email = string.Empty;

                if (!string.IsNullOrEmpty(identity.Name))
                {
                    var identityModel = JsonConvert.DeserializeObject<IdentityModel>(identity.Name);
                    if (identityModel != null)
                    {
                        email = identityModel.Email;
                    }
                }

                return email;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        public static Guid GetUserId(this IIdentity identity)
        {
            try
            {
                var userId = Guid.Empty;

                if (!string.IsNullOrEmpty(identity.Name))
                {
                    var identityModel = JsonConvert.DeserializeObject<IdentityModel>(identity.Name);
                    if (identityModel != null)
                    {
                        userId = identityModel.Id;
                    }
                }

                return userId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public static string GetUrlPhoto(this IIdentity identity)
        {
            try
            {
                var urlPhoto = string.Empty;

                if (!string.IsNullOrEmpty(identity.Name))
                {
                    var identityModel = JsonConvert.DeserializeObject<IdentityModel>(identity.Name);
                    if (identityModel != null)
                    {
                        urlPhoto = identityModel.Avatar;
                    }
                }

                return urlPhoto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public static bool HasUrlPhoto(this IIdentity identity)
        {
            try
            {
                var hasUrlPhoto = false;

                if (!string.IsNullOrEmpty(identity.Name))
                {
                    var identityModel = JsonConvert.DeserializeObject<IdentityModel>(identity.Name);
                    if (identityModel != null)
                    {
                        hasUrlPhoto = !string.IsNullOrEmpty(identityModel.Avatar);
                    }
                }

                return hasUrlPhoto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public static string GetSDT(this IIdentity identity)
        {
            try
            {
                var sdt = string.Empty;

                if (!string.IsNullOrEmpty(identity.Name))
                {
                    var identityModel = JsonConvert.DeserializeObject<IdentityModel>(identity.Name);
                    if (identityModel != null)
                    {
                        sdt = identityModel.Phone;
                    }
                }

                return sdt;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}