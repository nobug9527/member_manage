using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paper
{
    public class Paper
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public Dictionary<string, Object> condition { get; set; }

        public Paper(int pageindex,int pagesize,int recordcount) {
            this.PageIndex = pageindex;
            this.PageSize = pagesize;
            this.RecordCount = recordcount;
            condition = new Dictionary<string, object>();
        }

        /// <summary>
        /// 页数总计
        /// </summary>
        public int PageCount
        {
            get
            { 
                return (int)Math.Ceiling(1.0*RecordCount/PageSize);
            }
        }

        /// <summary>
        /// 下方超链接起始页数
        /// </summary>
        public int LinkStart 
        {
            get 
            {
                int p = PageIndex - 4;
                if (p + 8 > PageCount)
                    p=PageCount-8;
                if (p < 0)
                    p = 1;
                return p;
            }
        }
        /// <summary>
        /// 下方超链接结束页数
        /// </summary>
        public int LinkEnd 
        {
            get 
            {
                int p = LinkStart + 8;
                if (p > PageCount)
                    p = PageCount;
                return p;
            }
        }
        /// <summary>
        /// 上一页
        /// </summary>
        public int Prev 
        {
            get 
            {
                if (PageIndex > 1)
                    return PageIndex - 1;
                else
                    return 1;
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        public int Next 
        {
            get 
            {
                if (PageIndex < PageCount)
                    return PageIndex + 1;
                else
                    return PageCount;
            }
        }

    }
}
