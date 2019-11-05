using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbr.dal;
using System.Data;

namespace mbr.bll
{
    public class Record
    {
        mbr.dal.Record dal = new dal.Record();
        public DataTable GetRecordListbyid(Int64 id)
        {
            return dal.GetRecordListbyid(id);
        }

        public DataTable GetRecordList(int pageindex, int pagesize, out int record, string strwhere, string datatable, string tiaojian,string adminid)
        {
            return dal.GetRecordList(pageindex, pagesize, out record, strwhere,tiaojian,datatable,adminid);
        }
    }
}
