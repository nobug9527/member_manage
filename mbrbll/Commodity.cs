using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace mbr.bll
{
    public class Commodity
    {
        mbr.dal.Commodity dal = new dal.Commodity();
        public Model.MemberInfo GetMemberInfoByPhoneOrId(string info,string manageid) {
            return dal.GetMemberInfoByPhoneOrId(info,manageid);
        }

        public List<Object> GetGoodSList(int manageid) 
        {
            return dal.GetGoodsList(manageid);
        }

        public int GoodsNumSub(int goodid, int num) {
           return dal.GoodsNumSub(goodid, num);
        }

        public int Insertmemberlog(Int64 id, string total) {
            return dal.Insertmemberlog(id, total);
        }

        public int Insertincome(int manageid, int total) {
            return dal.Insertincome(manageid, total);
        }

        public DataTable GetTypes(int manageid) {
            return dal.GetType(manageid);
        }

        public DataTable GetGoodsByTypeId(string typeid) {
            return dal.GetGoodsByTypeId(typeid);
        }
    }
}
