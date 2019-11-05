using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DButility;

namespace mbr.dal
{
    public class Record
    {
        public static string connectionstring = ConfigurationManager.ConnectionStrings["membermanage"].ConnectionString;

        /// <summary>
        /// 获取单个用户的消费记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRecordListbyid(Int64 id)
        {
            string strsql = "select memberinfo.name,operatype,remarks,operatime from memberinfo, memberlog where memberinfo.no = memberlog.memberid and memberid =  " + id + " order by CONVERT (datetime,operatime) desc";
            return DbHelperSQL.ExecuteDataSet(strsql).Tables[0];
        }

/// <summary>
/// 获取分页后的消费记录
/// </summary>
/// <param name="pageindex"></param>
/// <param name="pagesize"></param>
/// <param name="record"></param>
/// <param name="strwhere"></param>
/// <param name="tiaojian"></param>
/// <param name="datatable"></param>
/// <returns></returns>
        public DataTable GetRecordList(int pageindex,int pagesize,out int record,string strwhere,string tiaojian,string datatable,string adminid) {
            string strcount;
            string strwhereinfo;
            if (strwhere != "")
            {
                strcount = "select count(*) from memberlog where operatype='" + strwhere + "'";
                strwhereinfo = " and operatype ='" + strwhere + "' and manageid = "+adminid+" ";
            }
            else 
            {
                strcount = "select count(*) from memberlog";
                strwhereinfo = " and manageid = "+adminid+" ";
            }
            record = Convert.ToInt32(DbHelperSQL.GetSingle(strcount));
            string strsql = DbHelperSQL.Pageing(pageindex, pagesize, record, strwhereinfo, datatable, tiaojian);
            return DbHelperSQL.ExecuteDataSet(strsql).Tables[0];
        }
    }
}
