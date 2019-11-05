using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    [Serializable]
    public partial class MemberInfo
    {
        public MemberInfo() { }

        private Int64 _no;
        private string _name;
        private string _phonenum;
        private Decimal _balance;
        private int _islock;
        private int _sycs;
        private string _gender;
        private string _born;
        private string _starttime;
        private string _manageid;
        private string _xuhao;





        /// <summary>
        /// 会员编号
        /// </summary>
        public Int64 no
        {
            get { return _no; }
            set { _no = value; }
        }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 会员手机号码
        /// </summary>
        public string phonenum
        {
            get { return _phonenum; }
            set { _phonenum = value; }
        }
        /// <summary>
        /// 会员余额
        /// </summary>
        public Decimal balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        /// <summary>
        /// 是否注销锁定
        /// </summary>
        public int islock
        {
            get { return _islock; }
            set { _islock = value; }
        }
        /// <summary>
        /// 剩余次数
        /// </summary>
        public int sycs
        {
            get { return _sycs; }
            set { _sycs = value; }
        }
        /// <summary>
        /// 会员性别
        /// </summary>
        public string gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        /// <summary>
        /// 会员出生日期
        /// </summary>
        public string born
        {
            get { return _born; }
            set { _born = value; }
        }

        /// <summary>
        /// 会员消费开始时间
        /// </summary>
        public string starttime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }

        /// <summary>
        /// 所属管理者ID
        /// </summary>
        public string manageid
        {
            get { return _manageid; }
            set { _manageid = value; }
        }

        /// <summary>
        /// 生成的分页和显示用的序号
        /// </summary>
        public string xuhao
        {
            get { return _xuhao; }
            set { _xuhao = value; }
        }

    }
}
