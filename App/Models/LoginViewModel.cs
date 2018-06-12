using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "验证码")]
        public string Codeimg { get; set; }

        public string ErrorMsg { get; set; }
    }

}