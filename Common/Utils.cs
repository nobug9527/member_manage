using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;

namespace Common
{
    public static class Utils
    {
        /// <summary>
        /// 写一个cookie
        /// </summary>
        /// <param name="strname">cookid名</param>
        /// <param name="key">键名</param>
        /// <param name="value">值名</param>
        /// <param name="expires">过期时间</param>
        public static void wirtecookie(string strname,string value,int expires) {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["strname"];
            if (cookie == null) 
            {
                cookie = new HttpCookie(strname);
            }
            cookie.Value = HttpContext.Current.Server.UrlEncode(value);
            //cookie.Value = value;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static string getcookie(string strname) {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strname] != null) {
                return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[strname].Value.ToString());
                //return HttpContext.Current.Request.Cookies[strname].Value.ToString();
            }
            return "";
        }
    }
}
