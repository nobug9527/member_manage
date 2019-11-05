using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using mbr.bll;
using paper;
using ViewModel;
using System.Web.Security;
using DButility;
using Common;
using System.Data;
using System.Data.SqlClient;

namespace member_manage.Controllers
{
    public class RecordController : Controller
    {
        //
        // GET: /Record/
        mbr.bll.Record bll = new Record();
        public ActionResult Index()
        {
            string username = Utils.getcookie("member_manage_username");
            string password = Utils.getcookie("member_manage_password");
            string manageid = Utils.getcookie("member_manage_id");
            if (username == "" || password == "" || username == null || password == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.username = username;
            return View();
        }

        public ActionResult List(int pageindex=1,int pagesize = 50,string type = "")
        {
            int record;
            string adminid = Utils.getcookie("member_manage_id");
            string datatable = " memberlog,memberinfo where memberinfo.no = memberlog.memberid";
            DataTable list = bll.GetRecordList(pageindex, pagesize, out record, type, datatable, "CONVERT (datetime,operatime) desc",adminid);
            Paper paper = new Paper(pageindex, pagesize, record);
            paper.condition.Add("type", type);
            ViewBag.paper = paper;
            return PartialView(list);
        }

    }
}
