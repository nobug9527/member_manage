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
    public class MemberInfo
    {
        //数据库连接字符串
        public static string connectionstring = ConfigurationManager.ConnectionStrings["membermanage"].ConnectionString;
        
        /// <summary>
        /// 根据编号no查询会员是否存在
        /// </summary>
        /// <param name="no"></param>
        /// <returns>bool</returns>
        public bool Exist(Int64 no)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select count(1) from memberinfo");
            strsql.Append(" where no=");
            strsql.Append(no);
            int result = Convert.ToInt16(DbHelperSQL.GetSingle(strsql.ToString()));
            return (result == 0) ? false : true;
        }
        /// <summary>
        /// 获取全部会员数据
        /// </summary>
        /// <returns></returns>
        public List<Model.MemberInfo> GetList() {
            List<Model.MemberInfo> list = new List<Model.MemberInfo>();
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from memberinfo where islock=0 and manageid=2");
            DataSet ds = DbHelperSQL.ExecuteDataSet(strsql.ToString());
            //string temp = "";
            if (ds.Tables[0].Rows.Count > 0) {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //temp += dr["no"].ToString();
                    Int64 no = Convert.ToInt64(dr["no"].ToString());
                    Model.MemberInfo model = (GetModel(no));
                    list.Add(model);
                }
            }


            return list;
        }

        /// <summary>
        /// 根据编号No获取一个实体类
        /// </summary>
        /// <param name="no">编号</param>
        /// <returns>model.memberinfo</returns>
        public Model.MemberInfo GetModel(Int64 no) {
            Model.MemberInfo model = new Model.MemberInfo();
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from memberinfo ");
            strsql.Append("where islock = 0 and no = ");
            strsql.Append(no);
            DataSet ds = DbHelperSQL.ExecuteDataSet(strsql.ToString());
            if (ds.Tables[0].Rows.Count > 0) {
                model.no = no;
                model.name = ds.Tables[0].Rows[0]["name"].ToString();
                model.phonenum = ds.Tables[0].Rows[0]["phonenum"].ToString();
                model.balance = Convert.ToDecimal(ds.Tables[0].Rows[0]["balance"].ToString().Substring(0, ds.Tables[0].Rows[0]["balance"].ToString().IndexOf('.')));
                model.sycs = Convert.ToInt32(ds.Tables[0].Rows[0]["sycs"]);
                model.gender = ds.Tables[0].Rows[0]["gender"].ToString();
                DateTime date = Convert.ToDateTime(ds.Tables[0].Rows[0]["born"]);
                model.born = date.ToShortDateString();
                model.starttime = ds.Tables[0].Rows[0]["starttime"].ToString();
            }
            return model;
        }

        public List<Model.MemberInfo> GetList(int pageindex,int pagesize,out int record,string strwhere,string datatable,string adminid,string tiaojian) {
            string strcount;
            string strwhereinfo;
            if (strwhere != "")
            {
                strcount = "select count(*) from " + datatable + " where (name='" + strwhere + "' or phonenum='" + strwhere + "') and islock=0 and manageid = " + adminid + "";
                strwhereinfo = " where (name = '" + strwhere + "' or phonenum = '" + strwhere + "') and islock = 0 and manageid = " + adminid + " ";
            }

            else {
                strcount = "select count(*) from " + datatable + " where islock = 0 and manageid = " + adminid + "";
                strwhereinfo = " where islock=0 and manageid = " + adminid + "";
            }
                

            record = Convert.ToInt32(DbHelperSQL.GetSingle(strcount));
            string strsql = DbHelperSQL.Pageing(pageindex, pagesize, record, strwhereinfo, datatable,tiaojian);
            DataSet ds = DbHelperSQL.ExecuteDataSet(strsql);
            List<Model.MemberInfo> list = new List<Model.MemberInfo>();
            if (ds.Tables[0].Rows.Count > 0) {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //temp += dr["no"].ToString();
                    Int64 no = Convert.ToInt64(dr["no"].ToString());
                    Model.MemberInfo model = (GetModel(no));
                    model.xuhao = dr["xuhao"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 锁定用户账户
        /// </summary>
        /// <param name="id"></param>
        public void Lock(Int64 id) 
        {
            string strsql = "update memberinfo set islock=1 where no = " + id;
            DbHelperSQL.Getexecuteresult(strsql);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public int Add(Model.MemberInfo member,string no,string manageid) 
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("insert into memberinfo (no,name,phonenum,gender,born,manageid,balance,islock,sycs) values ");
            strsql.Append("("+no+",'" + member.name + "','" + member.phonenum + "','" + member.gender + "','" + member.born.ToString() + "',"+manageid+",0,0,0)");
            return DbHelperSQL.Getexecuteresult(strsql.ToString());
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public int Modify(Model.MemberInfo member) 
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("update memberinfo set name='" + member.name + "',phonenum='" + member.phonenum + "',gender='" + member.gender + "',born='" + member.born + "' ");
            strsql.Append(" where no = " + member.no + "");
            return DbHelperSQL.Getexecuteresult(strsql.ToString());
        }

        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="no"></param>
        /// <param name="intomoney"></param>
        /// <param name="czcs"></param>
        /// <returns></returns>
        public int Invest(string no,string intomoney,string czcs) {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("update memberinfo set balance+=" + intomoney + ",sycs+=" + czcs + " where no = " + no + "");
            return DbHelperSQL.Getexecuteresult(strsql.ToString());
        }

        /// <summary>
        /// 修改费率
        /// </summary>
        /// <param name="rate1"></param>
        /// <param name="rate2"></param>
        /// <returns></returns>
        public int ModifyRates(string rate1,string rate2,string adminid) {

            StringBuilder strsql = new StringBuilder();
            strsql.Append("update rates set money = " + rate1 + " where name = 'ptmoney' and manageid = "+adminid+"");
            StringBuilder strsql1 = new StringBuilder();
            strsql1.Append("update rates set money = " + rate2 + " where name = 'oncemoney' and manageid = "+adminid+"");
            if (DbHelperSQL.Getexecuteresult(strsql.ToString()) > 0 && DbHelperSQL.Getexecuteresult(strsql1.ToString()) > 0)
            {
                return 1;
            }
            else {
                return 0;
            }
        }

        /// <summary>
        /// 消费
        /// </summary>
        /// <param name="type"></param>
        /// <param name="no"></param>
        public void Consume(int type, Int64 no)
        {
            if (type == 1)
            {
                string start = DateTime.Now.ToString("g");
                StringBuilder strsql = new StringBuilder();
                strsql.Append("update memberinfo set starttime = '" + start + "' where no = " + no + "");
                DbHelperSQL.Getexecuteresult(strsql.ToString());
                
            }
            else {
                StringBuilder strsql = new StringBuilder();
                strsql.Append("update memberinfo set sycs=sycs-1 where no = " + no + "");
                int i1= DbHelperSQL.Getexecuteresult(strsql.ToString());
                StringBuilder strsql1 = new StringBuilder();
                string nowtime = DateTime.Now.ToString("g");
                strsql1.Append("insert into memberlog values(" + no + ",'消费','按次消费一次','"+nowtime+"')");
                int i2= DbHelperSQL.Getexecuteresult(strsql1.ToString());
            }
        }
        /// <summary>
        /// 用户点击结账，更新用户表信息并插入日志表记录
        /// </summary>
        /// <param name="no"></param>
        /// <param name="money"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        public void JieZhang(Int64 no, int money, string starttime, string endtime)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("update memberinfo set balance = balance-" + money + ",starttime=null where no = " + no + "");
            DbHelperSQL.Getexecuteresult(strsql.ToString());
            string remarks = "消费时间："+starttime+"---"+endtime+",消费"+money+"元";
            StringBuilder strsql1 = new StringBuilder();
            strsql1.Append("insert into memberlog values("+no+",'消费','"+remarks+"','"+starttime+"')");
            DbHelperSQL.Getexecuteresult(strsql1.ToString());
        }

        /// <summary>
        /// 充值存储过程
        /// </summary>
        /// <param name="intomoney"></param>
        /// <param name="czcs"></param>
        /// <param name="no"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public int investproc(int intomoney, int czcs, Int64 no, string remarks)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["membermanage"].ConnectionString;
            SqlParameter[] cmdParms ={
                                     new SqlParameter("@intomoney",SqlDbType.Money),
                                     new SqlParameter("@czcs",SqlDbType.Int),
                                     new SqlParameter("@no",SqlDbType.BigInt),
                                     new SqlParameter("@remarks",SqlDbType.VarChar),
                                     new SqlParameter("@operatime",SqlDbType.VarChar),
                                     };
            cmdParms[0].Value = intomoney;
            cmdParms[1].Value = czcs;
            cmdParms[2].Value = no;
            cmdParms[3].Value = remarks;
            string nowtime = DateTime.Now.ToString("g");
            cmdParms[4].Value = nowtime;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "member_invest";
                    cmd.Parameters.AddRange(cmdParms);
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
        }

        public bool judgeqxwherelock(Int64 id, int adminid)
        {
            string strsql = "select count(*) from memberinfo where no = " + id + " and  manageid = " + adminid + "";
            int result =Convert.ToInt32(DbHelperSQL.GetSingle(strsql));
            if (result == 0) {
                return false;
            }
            else{
                return true;
            }
        }
    }
}
