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
            string strsql = "select no from memberinfo where (no = " + info + " or phonenum = '" + info + "') and islock=0 and manageid = "+1+"";
            return GetMemberModelById(Convert.ToInt64(DbHelperSQL.GetSingle(strsql)));
        }

        public Model.MemberInfo GetMemberModelById(Int64 id) {
            Model.MemberInfo model = new Model.MemberInfo();
            mbr.dal.MemberInfo tmpdal = new MemberInfo();
            model = tmpdal.GetModel(id);
            return model;
        }

        public List<Object> GetGoodsList() 
        {
            string gettypeid = "select typeid from goodstype";
            DataTable tbtype = DbHelperSQL.ExecuteDataSet(gettypeid).Tables[0];
            List<Object> list = new List<object>();
            foreach (DataRow datarow in tbtype.Rows) 
            {
                int typeid = Convert.ToInt16(datarow["typeid"]);
                list.Add(GetListObject(typeid));
            }
            return list;
        }

        public List<Object> GetListObject(int typeid) 
        {
            List<Object> list = new List<object>();
            list.Add(typeid);
            string gettypename = "select typename from goodstype where typeid = " + typeid + "";
            string typename = DbHelperSQL.GetSingle(gettypename).ToString();
            list.Add(typename);
            string getgoodsbytype = "select * from commodity where typeid = " + typeid + "";
            DataTable dt = DbHelperSQL.ExecuteDataSet(getgoodsbytype).Tables[0];
            list.Add(dt);
            return list;
        }
    }
}
