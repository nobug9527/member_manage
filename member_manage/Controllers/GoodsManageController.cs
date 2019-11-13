using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using mbr.bll;
using Common;
using DButility;
namespace member_manage.Controllers
{
    public class GoodsManageController : Controller
    {
        //
        // GET: /GoodsManage/
        mbr.bll.Commodity bll = new mbr.bll.Commodity();
        public ActionResult Index()
        {
            int state = validate();
            if (state == 1)
            {
                string manageid = Utils.getcookie("member_manage_id");
                DataTable typelist = bll.GetTypes(Convert.ToInt16(manageid));
                ViewBag.typelist = typelist;
                return View();
            }
            else if (state == 3)
            {
                return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        public ActionResult GoodsList(string typeid="") {
            DataTable list = bll.GetGoodsByTypeId(typeid);
            return PartialView(list);
        }


        public int validate()
        {
            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            if (manageid != "" && manageid != null)
            {
                string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();
                if (loginstates != loginstatesDB)
                    return 3;
                return 1;
            }
            else
            {
                return 2;
            }

        }

    }
}
