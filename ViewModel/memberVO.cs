using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class memberVO
    {
        [Required(ErrorMessage = "姓名不能为空")]
        //[StringLength(8,MinimumLength=2,ErrorMessage="长度2--8 之间")]
        [RegularExpression("[\u4e00-\u9fa5]{2,4}", ErrorMessage = "2--4个汉字")]
        public string name { get; set; }

        [Required(ErrorMessage = "性别不能为空")]
        public string gender { get; set; }

        [Required(ErrorMessage = "请选择出生日期")]
        public DateTime born { get; set; }

        
        [Required(ErrorMessage="必须填写手机号")]
        [RegularExpression("1\\d{10}",ErrorMessage="11位数字，1开头")]
        public string phonenum{get;set;}

    }
}
