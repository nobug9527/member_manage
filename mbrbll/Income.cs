using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using mbr.dal;
namespace mbr.bll
{
    public class Income
    {
        mbr.dal.Income dal = new dal.Income();
        public List<Object> GetIncome(int adminid,string starttime,string endtime,string unit) {
            return dal.GetIncome(adminid,starttime,endtime,unit);
        }
    }
}
