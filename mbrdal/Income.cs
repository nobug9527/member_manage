using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DButility;
namespace mbr.dal
{
    public class Income
    {
        public List<Object> GetIncome(int adminid,string starttime,string endtime,string unit) {
            List<Object> returnlist = new List<object>();
            List<string> datelist = new List<string>();
            List<int> incomelist = new List<int>();

            //单位为天时
            if (unit == "day") {
                //没有起止日期
                if ((starttime == "" || starttime == null) && (endtime == "" || endtime == null))
                {
                    string strsql = "select * from income where manageid = " + adminid + " order by convert(datetime,date) asc";
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["date"].ToString());
                        incomelist.Add(Convert.ToInt32(item["income"]));
                    }
                }
                //有起止日期
                else if (starttime != ""&&starttime!=null && endtime != ""&&endtime != null)
                {
                    string strsql = "select * from income where manageid = " + adminid + " and convert(datetime,date) between '"+starttime+"' and '"+endtime+"' order by convert(datetime,date) asc";
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["date"].ToString());
                        incomelist.Add(Convert.ToInt32(item["income"]));
                    }
                }

                //有开始日期
                else if ((starttime != ""||starttime!=null) && (endtime == ""||endtime==null))
                {
                    string strsql = "select * from income where manageid = " + adminid + " and convert(datetime,date)>='"+starttime+"' order by convert(datetime,date) asc";
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["date"].ToString());
                        incomelist.Add(Convert.ToInt32(item["income"]));
                    }
                }

                //有截止日期
                else if ((starttime == ""||starttime==null) && (endtime != ""||endtime!=null))
                {
                    string strsql = "select * from income where manageid = " + adminid + " and convert(datetime,date)<='" + endtime + "' order by convert(datetime,date) asc";
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["date"].ToString());
                        incomelist.Add(Convert.ToInt32(item["income"]));
                    }
                }
            }
            else if (unit == "month") {
                //没有起止日期
                if ((starttime == "" || starttime == null) && (endtime == "" || endtime == null)) 
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("select Sum(income) as '收入',Year(CONVERT(datetime,date)) as '年份',MONTH(CONVERT(datetime,date)) as '月份' from income");
                    strsql.Append(" group by MONTH(CONVERT(datetime,date)),Year(CONVERT(datetime,date)) order by Year(CONVERT(datetime,date)) asc,MONTH(CONVERT(datetime,date)) asc");
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql.ToString()).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["年份"].ToString() + "/" + item["月份"].ToString());
                        incomelist.Add(Convert.ToInt32(item["收入"]));
                    }
                }
                //有起止日期
                else if (starttime != "" && starttime != null && endtime != "" && endtime != null) 
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("select Sum(income) as '收入',Year(CONVERT(datetime,date)) as '年份',MONTH(CONVERT(datetime,date)) as '月份' from income ");
                    strsql.Append("where CONVERT(datetime,date) between '" + starttime + "' and '" + endtime + "' group by MONTH(CONVERT(datetime,date)),Year(CONVERT(datetime,date)) ");
                    strsql.Append("order by Year(CONVERT(datetime,date)) asc,MONTH(CONVERT(datetime,date)) asc ");
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql.ToString()).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["年份"].ToString() + "/" + item["月份"].ToString());
                        incomelist.Add(Convert.ToInt32(item["收入"]));
                    }
                }

                //有开始日期
                else if ((starttime != "" || starttime != null) && (endtime == "" || endtime == null))
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("select Sum(income) as '收入',Year(CONVERT(datetime,date)) as '年份',MONTH(CONVERT(datetime,date)) as '月份' from income ");
                    strsql.Append("where CONVERT(datetime,date) >= '" + starttime + "'  group by MONTH(CONVERT(datetime,date)),Year(CONVERT(datetime,date)) ");
                    strsql.Append("order by Year(CONVERT(datetime,date)) asc,MONTH(CONVERT(datetime,date)) asc ");
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql.ToString()).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["年份"].ToString() + "/" + item["月份"].ToString());
                        incomelist.Add(Convert.ToInt32(item["收入"]));
                    }
                }

                //有截止日期
                else if ((starttime == "" || starttime == null) && (endtime != "" || endtime != null))
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("select Sum(income) as '收入',Year(CONVERT(datetime,date)) as '年份',MONTH(CONVERT(datetime,date)) as '月份' from income ");
                    strsql.Append("where CONVERT(datetime,date) <= '" + endtime + "'  group by MONTH(CONVERT(datetime,date)),Year(CONVERT(datetime,date)) ");
                    strsql.Append("order by Year(CONVERT(datetime,date)) asc,MONTH(CONVERT(datetime,date)) asc ");
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql.ToString()).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["年份"].ToString() + "/" + item["月份"].ToString());
                        incomelist.Add(Convert.ToInt32(item["收入"]));
                    }
                }

            }

            else if (unit == "year") { 
                //没有起止日期
                if ((starttime == "" || starttime == null) && (endtime == "" || endtime == null)) 
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("select Sum(income) as '收入',Year(CONVERT(datetime,date)) as '年份' from income group by Year(CONVERT(datetime,date))");
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql.ToString()).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["年份"].ToString());
                        incomelist.Add(Convert.ToInt32(item["收入"]));
                    }
                }
                //有起止日期
                else if (starttime != "" && starttime != null && endtime != "" && endtime != null)
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("select Sum(income) as '收入',Year(CONVERT(datetime,date)) as '年份' from income ");
                    strsql.Append("Where CONVERT(datetime,date) between '" + starttime + "' and '" + endtime + "' group by Year(CONVERT(datetime,date)) order by Year(CONVERT(datetime,date)) asc");
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql.ToString()).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["年份"].ToString());
                        incomelist.Add(Convert.ToInt32(item["收入"]));
                    }
                }
                //有开始日期
                else if ((starttime != "" || starttime != null) && (endtime == "" || endtime == null))
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("select Sum(income) as '收入',Year(CONVERT(datetime,date)) as '年份' from income ");
                    strsql.Append("Where CONVERT(datetime,date) >= '" + starttime + "' group by Year(CONVERT(datetime,date)) order by Year(CONVERT(datetime,date)) asc");
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql.ToString()).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["年份"].ToString());
                        incomelist.Add(Convert.ToInt32(item["收入"]));
                    }
                }
                //有结束日期
                else if ((starttime != "" || starttime != null) && (endtime == "" || endtime == null))
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("select Sum(income) as '收入',Year(CONVERT(datetime,date)) as '年份' from income ");
                    strsql.Append("Where CONVERT(datetime,date) <= '" + endtime + "' group by Year(CONVERT(datetime,date)) order by Year(CONVERT(datetime,date)) asc");
                    DataTable dt = DbHelperSQL.ExecuteDataSet(strsql.ToString()).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        datelist.Add(item["年份"].ToString());
                        incomelist.Add(Convert.ToInt32(item["收入"]));
                    }
                }
            }


            string[] datelist1 = datelist.ToArray();
            int[] incomelist1 = incomelist.ToArray();
            returnlist.Add(datelist1);
            returnlist.Add(incomelist1);
            return returnlist;
        }

        
    }

    
}
