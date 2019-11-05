using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbr.dal;
using System.Data;
namespace mbr.bll
{
    public class MemberInfo
    {
        mbr.dal.MemberInfo dal = new dal.MemberInfo();
        public bool Exist(int no) {
            return dal.Exist(no);
        }

        public Model.MemberInfo GetModel(int no) {
            return dal.GetModel(no);
        }

        public List<Model.MemberInfo> GetList() {
            return dal.GetList();
        }

        public List<Model.MemberInfo> GetList(int pageindex, int pagesize,out int record, string strwhere, string datatable,string adminid,string tiaojian)
        {
            return dal.GetList(pageindex, pagesize,out record, strwhere, datatable,adminid,tiaojian);
        }

        public void Lock(Int64 id)
        {
            dal.Lock(id);
        }

        public int Add(Model.MemberInfo member,string no,string manageid) {
            return dal.Add(member,no,manageid);
        }

        public int Modify(Model.MemberInfo member) {
            return dal.Modify(member);
        }

        public int Invest(string no,string intomoney,string czcs) {
            return dal.Invest(no, intomoney, czcs);
        }

        public int InvestProc(int intomoney, int czcs, Int64 no, string remarks)
        {
            return dal.investproc(intomoney,czcs,no,remarks);
        }

        public int ModifyRates(string rate1, string rate2,string adminid) {
            return dal.ModifyRates(rate1, rate2,adminid);
        }

        public void Consume(int type, Int64 no) {
            dal.Consume(type, no);
        }

        public void JieZhang(Int64 no, int money,string starttime,string endtime) {
            dal.JieZhang(no, money,starttime,endtime);
        }


        public bool judgeqxwherelock(Int64 id,int adminid) {
            return dal.judgeqxwherelock(id, adminid);
        }


    }
}
