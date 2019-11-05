using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mbr.bll;
using System.Security;
using System.Net;
using Common;
using DButility;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Management;
using System.Security.Cryptography;

namespace member_manage.Controllers
{
    
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult memberlist() {
            return null;
        }
        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult LoginPost() {
            string username = Request.Form.Get("username");
            string password = Request.Form.Get("password");
            string strsql = "select no from admininfo where username = '" + username + "' and password = '" + password + "'";
            string adminid = DbHelperSQL.GetSingle(strsql).ToString();
            if (adminid == "" || adminid == null) {
                return null;
            }
            string ipaddress = Request.ServerVariables.Get("Remote_Addr").ToString();
            string datenow = DateTime.Now.ToString();
            string md5str = ipaddress + datenow;
            //string loginyz = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ipaddress, "MD5").ToLower();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(md5str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++) {
                byte2String = byte2String + targetData[i].ToString("x2");
            }
            Utils.wirtecookie("member_manage_username", username, 1440);
            Utils.wirtecookie("member_manage_password", password, 1440);
            Utils.wirtecookie("member_manage_id", adminid, 1440);
            Utils.wirtecookie("loginstates", byte2String, 1440);
            string strsql1 = "update admininfo set loginstates = '" + byte2String + "' where no = " + adminid + "";
            DbHelperSQL.Getexecuteresult(strsql1);
            return RedirectToAction("Index", "Member");
        }

        public void test() {
            
        }


    }
}
