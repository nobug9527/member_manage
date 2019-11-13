using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DButility;
using System.Data;

namespace mbr.dal
{
    public class Commodity
    {
        public Model.MemberInfo GetMemberInfoByPhoneOrId(string info,string manageid) {
            string strsql = "select no from memberinfo where (no = " + info + " or phonenum = '" + info + "') and islock=0 and manageid = "+manageid+"";
            return GetMemberModelById(Convert.ToInt64(DbHelperSQL.GetSingle(strsql)));
        }

        public Model.MemberInfo GetMemberModelById(Int64 id) {
            Model.MemberInfo model = new Model.MemberInfo();
            mbr.dal.MemberInfo tmpdal = new MemberInfo();
            model = tmpdal.GetModel(id);
            return model;
        }

        public List<Object> GetGoodsList(int manageid) 
        {
            string gettypeid = "select typeid from goodstype where manageid="+manageid+"";
            DataTable tbtype = DbHelperSQL.ExecuteDataSet(gettypeid).Tables[0];
            List<Object> list = new List<object>();
            foreach (DataRow datarow in tbtype.Rows) 
            {
                int typeid = Convert.ToInt16(datarow["typeid"]);
                list.Add(GetListObject(typeid,manageid));
            }
            return list;
        }

        public List<Object> GetListObject(int typeid,int manageid) 
        {
            List<Object> list = new List<object>();
            list.Add(typeid);
            string gettypename = "select typename from goodstype where typeid = " + typeid + " and manageid = "+manageid+"";
            string typename = DbHelperSQL.GetSingle(gettypename).ToString();
            list.Add(typename);
            string getgoodsbytype = "select * from commodity where typeid = " + typeid + " and manageid ="+manageid+" and isuse=1";
            DataTable dt = DbHelperSQL.ExecuteDataSet(getgoodsbytype).Tables[0];
            list.Add(dt);
            return list;
        }

        public int GoodsNumSub(int goodid, int num) {
            string strsql = "update commodity set stock = stock-" + num + " where id = " + goodid + "";
            DbHelperSQL.Getexecuteresult(strsql);
            string getprice = "select price from commodity where id = " + goodid + "";
            string price = DbHelperSQL.GetSingle(getprice).ToString();
            int price1 = Convert.ToInt16(price);
            return price1 * num;
        }

        public int Insertmemberlog(Int64 id,string total) {
            string type = "消费";
            string remarks = "消费了" + total + "元";
            string nowtime = DateTime.Now.ToString("g");
            string strsql = "insert into memberlog values('" + id + "','" + type + "','" + remarks + "','" + nowtime + "')";
            return DbHelperSQL.Getexecuteresult(strsql);
        }

        public int Insertincome(int manageid,int total) {
            string date = System.DateTime.Now.ToString("d");
            string strsql = "select id from income where manageid = " + manageid + " and date = '" + date + "'";
            if (DbHelperSQL.GetSingle(strsql) == null)
            {
                string strsql1 = "insert into income values(" + manageid + ",'" + date + "','" + total + "')";
                return DbHelperSQL.Getexecuteresult(strsql1);
            }
            else {
                string strsql1 = "update income set income = income+" + total + " where id=" + DbHelperSQL.GetSingle(strsql).ToString() + "";
                return DbHelperSQL.Getexecuteresult(strsql1);
            }
        }

        public DataTable GetType(int manageid) {
            string strsql = "select * from goodstype where manageid = " + manageid + "";
            return DbHelperSQL.ExecuteDataSet(strsql).Tables[0];
        }

        public DataTable GetGoodsByTypeId(string typeid) {
            if (typeid != "")
            {
                string strsql = "select commodity.*,goodstype.typename from	commodity,goodstype where commodity.typeid=goodstype.typeid and isuse = 1 and typeid=" + typeid + "";
                return DbHelperSQL.ExecuteDataSet(strsql).Tables[0];
            }
            else 
            {
                string strsql = "select commodity.*,goodstype.typename from	commodity,goodstype where commodity.typeid=goodstype.typeid and isuse = 1";
                return DbHelperSQL.ExecuteDataSet(strsql).Tables[0];
            }

        }

        
    }
}
