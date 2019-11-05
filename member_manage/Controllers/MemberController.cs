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
    public class MemberController : Controller
    {
        //
        // GET: /Member/
        mbr.bll.MemberInfo bll = new mbr.bll.MemberInfo();
        public ActionResult Index()
        {
            
            string username = Utils.getcookie("member_manage_username");
            string password = Utils.getcookie("member_manage_password");
            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            if (manageid == "" || manageid == null) {
                return RedirectToAction("Login", "Home");
            }
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();
            if (loginstates == loginstatesDB) {
                if (username == "" || password == "" || username == null || password == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                string strsql = "select value from config where manageid = " + manageid + " and name = 'zdxf'";
                int zdxf = Convert.ToInt32(DbHelperSQL.GetSingle(strsql));
                ViewBag.zdxf = zdxf;
                ViewBag.username = username;
                return View();                
            }
            return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");

        }



        /// <summary>
        /// 分部视图显示列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(int pageindex = 1, int pagesize = 10, string info = "")
        {
            int record;
            string datatable = "memberinfo";
            string adminid = Utils.getcookie("member_manage_id");
            List<Model.MemberInfo> list = bll.GetList(pageindex, pagesize, out record, info, datatable,adminid,"no");
            Paper paper = new Paper(pageindex, pagesize, record);
            paper.condition.Add("info", info);
            ViewBag.paper = paper;
            return PartialView(list);
        }

        public ActionResult RecordList(Int64 id) {
            //List<Model.MemberInfo> list = bll.GetList();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("name",typeof(string));
            //dt.Columns.Add("age", typeof(int));
            //DataRow newrow;
            //newrow = dt.NewRow();
            //newrow["name"] = "库伊垚";
            //newrow["age"] = 23;
            //dt.Rows.Add(newrow);

            //newrow = dt.NewRow();
            //newrow["name"] = "李子雨";
            //newrow["age"] = 24;
            //dt.Rows.Add(newrow);
            mbr.bll.Record bll1 = new Record();
            DataTable list = bll1.GetRecordListbyid(id);
            return PartialView(list);
        }

        /// <summary>
        /// 增加会员模态框
        /// </summary>
        /// <returns></returns>
        public ActionResult AddModal() 
        {
            return PartialView();
        }

        public ActionResult ModifyModal() {
            return PartialView();
            
        }

        public ActionResult InvestModal() {
            return PartialView();
        }

        public ActionResult RatesModal()
        {
            return PartialView();
        }

        public ActionResult xiaofeiModal() 
        {
            return PartialView("xiaofei");
        }

        public ActionResult jiezhangModal() {
            return PartialView("jiezhang");
        }

        public ActionResult recordModal() {
            return PartialView("record");
        }

        public ActionResult configModal()
        {
            return PartialView("config");
        }

        public ActionResult modifypasswordModal() 
        {
            return PartialView("modifypassword");
        }




        /// <summary>
        /// 锁定用户,使其不再显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Lock(Int64 id) 
        {

            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();
            if (loginstates == loginstatesDB) {
                if (bll.judgeqxwherelock(id, Convert.ToInt32(manageid)) == true)
                {
                    bll.Lock(id);
                    return RedirectToAction("List");
                }
                else
                {
                    return Content("<script type='text/javascript'>alert('您没有权限注销此会员！');window.location='/Member/LogOut';</script>");
                }
                
            
            }
            return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");


        }

        public ActionResult test() {
            return View("View1");
        }

        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="member">传入参数直接转换成Model.MemberInfo</param>
        /// <returns></returns>
        public ActionResult Add(Model.MemberInfo member)
        {
            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();

            if (loginstates == loginstatesDB)
            {

                if (ModelState.IsValid)
                {
                    string temp = DateTime.Now.ToString("yyyyMMddHHmmss")+manageid;
                    int result = bll.Add(member,temp,manageid);

                }
                return RedirectToAction("List");
            }
            return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");
        }

        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public ActionResult Modify(Model.MemberInfo member) 
        {

            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();
            if (loginstates == loginstatesDB)
            {
                int result = bll.Modify(member);
                return RedirectToAction("List");

            }
            return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <returns></returns>
        public ActionResult Invest() 
        {
            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();
            if (loginstates == loginstatesDB)
            {
                int adminid = Convert.ToInt32(Utils.getcookie("member_manage_id"));
                string getoncemoney = "select money from rates where manageid=" + adminid + " and name = 'oncemoney'";
                int oncemoney = Convert.ToInt32(DbHelperSQL.GetSingle(getoncemoney));
                string date = System.DateTime.Now.ToString("d");
                //string name = Request.QueryString.Get("name");
                string id = Request.Form.Get("no");
                string intomoney = Request.Form.Get("intomoney");
                string czcs = Request.Form.Get("czcs");
                //int result = bll.Invest(id, intomoney, czcs);
                string remarks = "充值了" + intomoney + "元," + czcs + "次";
                int result1 = bll.InvestProc(int.Parse(intomoney), int.Parse(czcs), Int64.Parse(id), remarks);
                int income = Convert.ToInt32(czcs) * oncemoney + Convert.ToInt32(intomoney);
                string strsql = "select id from income where date = '" + date + "' and manageid = " + adminid + "";
                if (DbHelperSQL.GetSingle(strsql) == null)
                {
                    string addincome = "insert into income values(" + adminid + ",'" + date + "'," + income + ")";
                    DbHelperSQL.Getexecuteresult(addincome);
                }
                else
                {
                    string updateincome = "update income set income = income+ " + income + " where manageid = " + adminid + " and date = '" + date + "' ";
                    DbHelperSQL.Getexecuteresult(updateincome);
                }

                return RedirectToAction("List");

            }
            return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");

        }

        /// <summary>
        /// 修改费率
        /// </summary>
        /// <returns></returns>
        public ActionResult Rates() 
        {
            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();
            if (loginstates == loginstatesDB)
            {
                string adminid = Utils.getcookie("member_manage_id");
                string rate1 = Request.Form.Get("rate1");
                string rate2 = Request.Form.Get("rate2");
                bll.ModifyRates(rate1, rate2, adminid);
                return RedirectToAction("List");

            }
            return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");

        }

        /// <summary>
        /// 消费
        /// </summary>
        /// <returns></returns>
        public ActionResult XiaoFei()
        {
            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();
            if (loginstates == loginstatesDB)
            {
                Int64 no = Convert.ToInt64(Request.Form.Get("xf_no"));
                int type = Convert.ToInt32(Request.Form.Get("xftype"));
                bll.Consume(type, no);
                return RedirectToAction("List");
            }
            return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");

        }
        /// <summary>
        /// 结账
        /// </summary>
        /// <returns></returns>
        public ActionResult JieZhang() {
            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();
            if (loginstates == loginstatesDB)
            {
                Int64 no = Convert.ToInt64(Request.Form.Get("jz_no"));
                int money = Convert.ToInt32(Request.Form.Get("jz_money"));
                string starttime = Request.Form.Get("jz_starttime");
                string endtime = Request.Form.Get("jz_endtime");
                bll.JieZhang(no, money, starttime, endtime);
                return RedirectToAction("List");
            }
            return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");
        }
        public ActionResult LogOut() {
            Utils.wirtecookie("member_manage_username", null, -1440);
            Utils.wirtecookie("member_manage_password", null, -1440);
            Utils.wirtecookie("member_manage_id", null, -1440);
            Utils.wirtecookie("loginstates", null, -1440);
            return RedirectToAction("Login", "Home");
        }



        public ActionResult config() {
            string manageid = Utils.getcookie("member_manage_id");
            string loginstates = Utils.getcookie("loginstates");
            string strsql1 = "select loginstates from admininfo where no = " + manageid + "";
            string loginstatesDB = DbHelperSQL.GetSingle(strsql1).ToString();
            if (loginstates == loginstatesDB)
            {
                string manage_id = Utils.getcookie("member_manage_id");
                string zdxf = Request.Form.Get("zdxf");
                if (zdxf != "on")
                {
                    string strsql = "update config set value = 0 where name = 'zdxf' and manageid = " + manage_id + "";
                    int result = DbHelperSQL.Getexecuteresult(strsql);
                    if (result > 0)
                        ViewBag.zdxf1 = 0;
                }
                else if (zdxf == "on")
                {
                    string strsql = "update config set value = 1 where name = 'zdxf' and manageid = " + manage_id + "";
                    int result = DbHelperSQL.Getexecuteresult(strsql);
                    if (result > 0)
                        ViewBag.zdxf1 = 1;
                }
                return RedirectToAction("Index");

            }
            return Content("<script type='text/javascript'>alert('您已在其他地方登陆！');window.location='/Member/LogOut';</script>");

        }

        public ActionResult modifypassword() {
            string newpwd = Request.Form.Get("newpwd");
            string manageid = Utils.getcookie("member_manage_id");
            string strsql = "update admininfo set password = '" + newpwd + "' where no = '"+manageid+"'";
            DbHelperSQL.Getexecuteresult(strsql);
            return RedirectToAction("LogOut");
        }




    }
}
