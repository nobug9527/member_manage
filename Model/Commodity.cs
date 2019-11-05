using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Commodity
    {

        /// <summary>
        /// id
        /// </summary>		
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// typeid
        /// </summary>		
        private int _typeid;
        public int typeid
        {
            get { return _typeid; }
            set { _typeid = value; }
        }
        /// <summary>
        /// commodityname
        /// </summary>
        private string _commodityname;
        public string commodityname
        {
            get { return _commodityname; }
            set { _commodityname = value; }
        }
        /// <summary>
        /// price
        /// </summary>		
        private int _price;
        public int price
        {
            get { return _price; }
            set { _price = value; }
        }
        /// <summary>
        /// allowance
        /// </summary>		
        private int _allowance;
        public int allowance
        {
            get { return _allowance; }
            set { _allowance = value; }
        }

    }
}
