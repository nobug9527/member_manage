using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mbr.bll;
using Common;
namespace member_manage.Controllers
{
    public class IncomeController : Controller
    {
        //
        // GET: /Income/
        mbr.bll.Income bll = new Income();
        public ActionResult Index()
        {
            string username = Utils.getcookie("member_manage_username");
            string password = Utils.getcookie("member_manage_password");
            string manageid = Utils.getcookie("member_manage_id");
            if (username == "" || password == "" || username == null || password == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public JsonResult GetIncome() {
            string starttime = Request.QueryString["starttime"];
            string endtime = Request.QueryString["endtime"];
            string unit = Request.QueryString["unit"];
            if (unit == "" || unit == null) {
                unit = "day";
            }
            var res = new JsonResult();
            int adminid =Convert.ToInt32(Utils.getcookie("member_manage_id"));
            List<Object> list = bll.GetIncome(adminid,starttime,endtime,unit);
            var date = list[0];
            var value = list[1];
            res.Data = new { date, value };
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }

    }
}
