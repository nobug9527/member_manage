using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbr.bll
{
    public class Commodity
    {
        mbr.dal.Commodity dal = new dal.Commodity();
        public Model.MemberInfo GetMemberInfoByPhoneOrId(string info,string manageid) {
            return dal.GetMemberInfoByPhoneOrId(info,manageid);
        }

        public List<Object> GetGoodSList() 
        {
            return dal.GetGoodsList();
        }
    }
}
