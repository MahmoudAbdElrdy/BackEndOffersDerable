using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.Helper
{
    public class SecurityHelper
    {
        #region GetPlatform
        public static String GetMobileVersion(string userAgent, string device)
        {
            try
            {
                var temp = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart();
                var version = string.Empty;

                foreach (var character in temp)
                {
                    var validCharacter = false;
                    int test = 0;

                    if (Int32.TryParse(character.ToString(), out test))
                    {
                        version += character;
                        validCharacter = true;
                    }

                    if (character == '.' || character == '_')
                    {
                        version += '.';
                        validCharacter = true;
                    }

                    if (validCharacter == false)
                        break;
                }

                return version;
            }
            catch (Exception ex)
            {
                string Error = string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                return "";
            }
        }
        public static String GetUserPlatform(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            try
            {
                var ua = request.Headers["User-Agent"].ToString();

                if (ua.Contains("Android"))
                    return string.Format("Android {0}", GetMobileVersion(ua, "Android"));

                if (ua.Contains("iPad"))
                    return string.Format("iPad OS {0}", GetMobileVersion(ua, "OS"));

                if (ua.Contains("iPhone"))
                    return string.Format("iPhone OS {0}", GetMobileVersion(ua, "OS"));

                if (ua.Contains("Linux") && ua.Contains("KFAPWI"))
                    return "Kindle Fire";

                if (ua.Contains("RIM Tablet") || (ua.Contains("BB") && ua.Contains("Mobile")))
                    return "Black Berry";

                if (ua.Contains("Windows Phone"))
                    return string.Format("Windows Phone {0}", GetMobileVersion(ua, "Windows Phone"));

                if (ua.Contains("Mac OS"))
                    return "Mac OS";

                if (ua.Contains("Windows NT 5.1") || ua.Contains("Windows NT 5.2"))
                    return "Windows XP";

                if (ua.Contains("Windows NT 6.0"))
                    return "Windows Vista";

                if (ua.Contains("Windows NT 6.1"))
                    return "Windows 7";

                if (ua.Contains("Windows NT 6.2"))
                    return "Windows 8";

                if (ua.Contains("Windows NT 6.3"))
                    return "Windows 8.1";

                if (ua.Contains("Windows NT 10"))
                    return "Windows 10";

                //fallback to basic platform:
                //return request.Browser.Platform + (ua.Contains("Mobile") ? " Mobile " : "");
                return (ua.Contains("Mobile") ? " Mobile " : "");
            }
            catch (Exception ex)
            {
                string Error = string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                //new Helpers.ExceptionLogger().Log("APIs ::  SecurityHelper", "GetUserPlatform", Error, DateTime.Now);
                return "";
            }
        }
        #endregion

        #region HashPassowrd
       // public static string HashPassowrd(string password)
        //{
        //    string mySalt = DevOne.Security.Cryptography.BCrypt.BCryptHelper.GenerateSalt();
        //    return DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(password, mySalt);
        //}
        //public static bool VerifyPassowrd(string password, string hashedPassowrd)
        //{
        //   // return DevOne.Security.Cryptography.BCrypt.BCryptHelper.CheckPassword(password, hashedPassowrd);
        //}
        #endregion

        #region GetDistance(double P1Langitude, double P1Latitude, double P2Langitude, double P2Latitude)
        public static double GetDistance(double P1Langitude, double P1Latitude, double P2Langitude, double P2Latitude)
        {
            try
            {
                double lat1 = Deg2Rad(P2Langitude);
                double lat2 = Deg2Rad(P1Langitude);
                double lon1 = Deg2Rad(P2Latitude);
                double lon2 = Deg2Rad(P1Latitude);

                double R = 6371; // km    m 6378137
                double x = (lon2 - lon1) * Math.Cos((lat1 + lat2) / 2);
                double y = (lat2 - lat1);
                double distance = Math.Sqrt(x * x + y * y) * R;
                return distance;
            }
            catch (Exception ex)
            {
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                System.Diagnostics.Debug.WriteLine(error);
            }
            return 0;
        }
        private static double Deg2Rad(double deg)
        {
            return deg * Math.PI / 180;
        }
        #endregion
    }
}
