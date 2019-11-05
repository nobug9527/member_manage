using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DButility;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace member_manage.tools
{
    /// <summary>
    /// submit_ajax 的摘要说明
    /// </summary>
    public class submit_ajax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string action = HttpContext.Current.Request.QueryString["action"];
            
            switch (action) 
            {
                case  "checkphone":
                    checkphone(context);
                    break;
                case "getmodifyinfo":
                    getmodifyinfo(context);
                    break;
                case "getoncemoney":
                    getoncemoney(context);
                    break;
                case "getjzinfo":
                    getjiezhanginfo(context);
                    break;
                case "login":
                    login(context);
                    break;
                case "modifypassword":
                    modifypassword(context);
                    break;

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 检测注册手机号是否已被使用
        /// </summary>
        /// <param name="context"></param>
        public void checkphone(HttpContext context) 
        {
            string phonenum = HttpContext.Current.Request.Form["phonenum"];
            string id = HttpContext.Current.Request.Form["id"];
            string adminid = Utils.getcookie("member_manage_id");
            if (id == null || id == "")
            {

                string strsql = "select no from memberinfo where phonenum = '" + phonenum + "' and islock=0 and manageid = "+adminid+"";
                object obj = DbHelperSQL.GetSingle(strsql);
                if (obj == null)
                    context.Response.Write("{\"status\":200, \"msg\":\"手机号尚未被注册\"}");
                else
                    context.Response.Write("{\"status\":999, \"msg\":\"已被注册！\"}");
            }
            else {
                string strsql = "select no from memberinfo where phonenum = '" + phonenum + "' and islock=0 and no<>" + id + "";
                object obj = DbHelperSQL.GetSingle(strsql);
                if (obj == null)
                    context.Response.Write("{\"status\":200, \"msg\":\"手机号尚未被注册\"}");
                else
                    context.Response.Write("{\"status\":999, \"msg\":\"已被注册！\"}");

            }

            
        }

        /// <summary>
        /// 获取要修改的会员信息
        /// </summary>
        /// <param name="context"></param>
        public void getmodifyinfo(HttpContext context) 
        {
            string no = HttpContext.Current.Request.Form["id"];
            string strsql = "select * from memberinfo where no = " + no + "";
            DataSet ds = DbHelperSQL.ExecuteDataSet(strsql);
            string name = ds.Tables[0].Rows[0]["name"].ToString();
            string phonenum = ds.Tables[0].Rows[0]["phonenum"].ToString();
            string born = ds.Tables[0].Rows[0]["born"].ToString();
            string gender = ds.Tables[0].Rows[0]["gender"].ToString();
            context.Response.Write("{\"status\":200, \"name\":\"" + name + "\",\"phonenum\":\"" + phonenum + "\",\"born\":\"" + born + "\",\"gender\":\"" + gender + "\"}");
        }

        /// <summary>
        /// 获取充值一次的价格
        /// </summary>
        /// <param name="context"></param>
        public void getoncemoney(HttpContext context) 
        {
            string adminid = Utils.getcookie("member_manage_id");
            string strsql = "select money from rates where name = 'oncemoney' and manageid = "+adminid+"";
            string oncemoney = DbHelperSQL.GetSingle(strsql).ToString();
            string strsql1 = "select money from rates where name = 'ptmoney' and manageid = "+adminid+"";
            string ptrate = DbHelperSQL.GetSingle(strsql1).ToString();
            context.Response.Write("{\"status\":200,\"oncemoney\":\"" + oncemoney + "\",\"ptrate\":\"" + ptrate + "\",\"date\":[\"2019/10/10\",\"2019/11/11\"],\"value\":[20,60]}");
           
        }

        /// <summary>
        /// 获取结账信息
        /// </summary>
        /// <param name="context"></param>
        public void getjiezhanginfo(HttpContext context) {
            string adminid = Utils.getcookie("member_manage_id");
            string no = HttpContext.Current.Request.Form["id"];
            string starttimestr = HttpContext.Current.Request.Form["starttime"];
            string endtimestr = DateTime.Now.ToString("g");
            TimeSpan totaltime = Convert.ToDateTime(endtimestr)-Convert.ToDateTime(starttimestr);
            //DateTime totaltime1 = Convert.ToDateTime(totaltime);
            int minute =Convert.ToInt32(totaltime.Minutes);
            int hour =Convert.ToInt32(totaltime.Hours);
            int days =Convert.ToInt32(totaltime.Days);
            string strsql = "select money from rates where name = 'ptmoney' and manageid = "+adminid+"";
            int ptrate =Convert.ToInt32(DbHelperSQL.GetSingle(strsql));
            int totalmoney = 0;
            string usetime="";
            if (days > 0) {
                usetime += days.ToString() + "天";
                totalmoney += days * 24 * ptrate;
            }
            if (hour > 0) {
                usetime += hour.ToString() + "小时";
                totalmoney += hour * ptrate;
            }
            usetime += minute.ToString() + "分钟";

            string strsql2 = "select value from config where manageid = " + adminid + " and name = 'zdxf'";
            int zdxf = Convert.ToInt32(DbHelperSQL.GetSingle(strsql2));
            if (zdxf == 1) {
                if (totalmoney == 0)
                {
                    string adminid1 = Utils.getcookie("member_manage_id");
                    string strsql1 = "select money from rates where name = 'oncemoney' and manageid = " + adminid1 + "";
                    int oncemoney = Convert.ToInt32(DbHelperSQL.GetSingle(strsql1).ToString());
                    totalmoney = oncemoney;
                }
            }

            context.Response.Write("{\"endtime\":\"" + endtimestr + "\",\"usetime\":\"" + usetime + "\",\"totalmoney\":\"" + totalmoney + "\"}");
        }
        /// <summary>
        /// 登录时判断帐号是否存在/帐号密码是否错误
        /// 返回提示信息
        /// </summary>
        /// <param name="context"></param>
        public void login(HttpContext context) {
            string username = HttpContext.Current.Request.Form["username"];
            string password = HttpContext.Current.Request.Form["password"];
            string strsql = "select count(*) from admininfo where username = '" + username + "'";
            if (Convert.ToInt32(DbHelperSQL.GetSingle(strsql)) > 0)
            {
                string strsql1 = "select count(*) from admininfo where username = '" + username + "' and password = '" + password + "'";
                if(Convert.ToInt32(DbHelperSQL.GetSingle(strsql1)) > 0)
                    context.Response.Write("{\"status\":1}");
                else
                    context.Response.Write("{\"status\":2}");
            }
            else {
                context.Response.Write("{\"status\":3}");
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="context"></param>
        public void modifypassword(HttpContext context)
        {
            string formpassword = HttpContext.Current.Request.Form["formpassword"];
            int manageid =Convert.ToInt32(Utils.getcookie("member_manage_id"));
            string strsql = "select password from admininfo where no = " + manageid + "";
            string oldpassword = DbHelperSQL.GetSingle(strsql).ToString();
            if (formpassword != oldpassword)
            {
                context.Response.Write("{\"status\":2}");
            }
            else {
                context.Response.Write("{\"status\":1}");
            }
        }
    }
}