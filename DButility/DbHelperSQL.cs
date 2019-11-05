using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DButility
{
    public class DbHelperSQL
    {
        //数据库连接字符串
        public static string connectionstring = ConfigurationManager.ConnectionStrings["membermanage"].ConnectionString;
        public DbHelperSQL() { }
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>true or false</returns>
        public static bool Exists(string strSql)
        {
            object obj = DbHelperSQL.GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 执行查询返回结果
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>查询结果(object)</returns>
        public static object GetSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行语句返回受影响的行数
        /// </summary>
        /// <param name="strsql">要执行的语句</param>
        /// <returns>受影响的行数</returns>
        public static int Getexecuteresult(string strsql)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(strsql, connection))
                {
                    try
                    {
                        connection.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 在线连接返回Datareader
        /// </summary>
        /// <param name="strsql">执行的语句</param>
        /// <returns>datareader</returns>
        public static SqlDataReader ExecuteReader(string strsql)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(strsql, connection))
                {
                    //cmd.CommandText = strsql;
                    try
                    {
                        connection.Open();
                        SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        return sdr;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }

        }

        /// <summary>
        /// 离线连接返回DataSet
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string strsql)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = strsql;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 提交日志文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="operate"></param>
        /// <param name="target"></param>
        public static void submitlog(string name, string operate, string target)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string strsql = "insert into operate(operator,handle,target) values('" + name + "','" + operate + "','" + target + "')";
                using (SqlCommand cmd = new SqlCommand(strsql, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
        }

        /// <summary>
        /// 组装分页SQL语句
        /// </summary>
        /// <param name="pageindex">要跳转的序号</param>
        /// <param name="pagesize">每页的记录数量</param>
        /// <param name="recound">总记录数</param>
        /// <param name="strwhere">条件</param>
        /// <param name="datatable">要查询的数据表</param>
        /// <returns></returns>
        public static String Pageing(int pageindex,int pagesize,int record,string strwhere,string datatable,string tiaojian) {
            // 分页序号语句
            StringBuilder strsql = new StringBuilder();
            strsql.AppendFormat("select ROW_NUMBER() OVER(order by "+tiaojian+") as xuhao,* from {0}",datatable);
            //if (strwhere != ""){
            //    strsql.AppendFormat(" where (name='{0}' or phonenum='{0}') and islock=0 and manageid = 2", strwhere);
            //}
            //else {
            strsql.Append(strwhere);
            
            //最终语句
            StringBuilder strsql1 = new StringBuilder();
            strsql1.Append("select * from (");
            strsql1.Append(strsql.ToString());
            strsql1.Append(") AS T");
            strsql1.AppendFormat(" where xuhao between {0} and {1}", ((pageindex - 1) * pagesize)+1, pageindex * pagesize);
            return strsql1.ToString();
        }
    }
}
