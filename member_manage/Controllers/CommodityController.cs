using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DButility;

namespace member_manage.Controllers
{
    public class CommodityController : Controller
    {
        //
        // GET: /Commodity/
        mbr.bll.Commodity bll = new mbr.bll.Commodity();
        public ActionResult Index()
        {
            int state = validate();
            if (state == 1) {
                return View();
            }
            else if (state == 3) {
                return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");
            }
            else{
                return RedirectToAction("Login", "Home");
            }
            
        }
        public JsonResult GetMemberInfo() {
            int state = validate();
            if (state == 1)
            {
                Regex reg = new Regex("^[0-9]+.?[0-9]*$");
                string info = Request.Form.Get("getinfo");
                if (reg.IsMatch(info))
                {
                    string manageid = Utils.getcookie("member_manage_id");
                    Model.MemberInfo model = bll.GetMemberInfoByPhoneOrId(info, manageid);
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
                    else
                    {
                        return Json(new
                        {
                            state = 2
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        state = 3
                    });
                }
            }
            else if (state == 3)
            {
                //别处登录
                return Json(new
                {
                    state = 4
                });
            }
            else
            {
                //已下线
                return Json(new
                {
                    state = 5
                });
            }


        }

        public ActionResult GetCommodityList() {
            int state = validate();
            if (state == 1)
            {
                return PartialView();
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

        public ActionResult CenterList() {
            int state = validate();
            if (state == 1)
            {
                int manageid = Convert.ToInt32(Utils.getcookie("member_manage_id"));
                List<Object> list = bll.GetGoodSList(manageid);
                return PartialView(list);
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

        public ActionResult RightContent() {
            int state = validate();
            if (state == 1)
            {
                return PartialView();
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

        public JsonResult SubmitXF() {
            int state = validate();
            if (state == 1)
            {
                string datastr = Request.Form.Get("datastr");
                string memberid = Request.Form.Get("memberid");
                string manageid = Utils.getcookie("member_manage_id");
                datastr = datastr.Substring(0, datastr.Length - 1);
                if (datastr != null && memberid != null)
                {
                    int total = 0;
                    string[] temparr = datastr.Split(',');
                    foreach (string str in temparr)
                    {
                        int goodid = Convert.ToInt16(str.Substring(0, str.IndexOf('*')));
                        int num = Convert.ToInt16(str.Substring(str.IndexOf('*') + 1));
                        total += bll.GoodsNumSub(goodid, num);
                    }
                    bll.Insertincome(Convert.ToInt16(manageid), total);
                    bll.Insertmemberlog(Convert.ToInt64(memberid), total.ToString());
                    return Json(new
                    {
                        state = 1,
                    });
                }
                return Json(new
                {
                    state = 2,
                });
            }
            else if (state == 3)
            {
                return Json(new
                {
                    state = 3,
                });
            }
            else
            {
                return Json(new
                {
                    state = 4,
                });
            }

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

        public int validate() {
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
            else {
                return 2;
            }

        }
    }
}
