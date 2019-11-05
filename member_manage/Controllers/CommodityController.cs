using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using System.Data;
using System.Data.SqlClient;

namespace member_manage.Controllers
{
    public class CommodityController : Controller
    {
        //
        // GET: /Commodity/
        mbr.bll.Commodity bll = new mbr.bll.Commodity();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetMemberInfo() {
            string info = Request.Form.Get("getinfo");
            string manageid = Utils.getcookie("member_manage_id");
            Model.MemberInfo model = bll.GetMemberInfoByPhoneOrId(info,manageid);
            if (model.no != 0)
            {
                return Json(new
                {
                    state = 1,
                    name = model.name,
                    phone = model.phonenum,
                    no = model.no
                });
            }
            else {
                return Json(new
                {
                    state = 2
                });
            }

        }

        public ActionResult GetCommodityList() {
            return PartialView();
        }

        public ActionResult CenterList() {

            List<Object> list = bll.GetGoodSList();
            return PartialView(list);
        }

        public void testcode() {
            DataTable dt1 = new DataTable("dt1");
            DataColumn dc = null;
            dc = dt1.Columns.Add("name", Type.GetType("System.String"));
            DataRow newRow;

            newRow = dt1.NewRow();
            newRow["name"] = "测试一";
            dt1.Rows.Add(newRow);

            newRow = dt1.NewRow();
            newRow["name"] = "测试二";
            dt1.Rows.Add(newRow);

            newRow = dt1.NewRow();
            newRow["name"] = "测试三";
            dt1.Rows.Add(newRow);
            List<Object> list1 = new List<object>();
            string str = "标题一";
            list1.Add(str);
            list1.Add(dt1);

            //111111111111111111111111111111111111111111111111111111111
            DataTable dt2 = new DataTable("dt1");
            DataColumn dc1 = null;
            dc1 = dt2.Columns.Add("name", Type.GetType("System.String"));
            DataRow newRow1;

            newRow1 = dt2.NewRow();
            newRow1["name"] = "测试四";
            dt2.Rows.Add(newRow1);

            newRow1 = dt2.NewRow();
            newRow1["name"] = "测试五";
            dt2.Rows.Add(newRow1);

            newRow1 = dt2.NewRow();
            newRow1["name"] = "测试六";
            dt2.Rows.Add(newRow1);

            List<Object> list2 = new List<object>();
            string str2 = "标题二";
            list2.Add(str2);
            list2.Add(dt2);

            List<Object> list = new List<object>();
            list.Add(list1);
            list.Add(list2);
        }

    }
}
