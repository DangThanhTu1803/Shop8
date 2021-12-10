using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop8.Models
{
    public class RegisterModel
    {
        [Key]
        public long Id { set; get; }
        [Display(Name="Tên tài khoản")]
        [Required(ErrorMessage ="vui lòng nhập tên đăng nhập")]
        public string UserName { set; get; }
        [Display(Name = "Mật khẩu")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="Độ dài mật khẩu từ 6 đén 20 ký tự")]
        [Required(ErrorMessage = "vui lòng nhập Mật khẩu")]
        public string Password { set; get; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mât khẩu và mật khẩu xác nhận không trùng nhau")]
        
        public string ConfirmPassword { set; get; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "vui lòng nhập Tên")]
        public string Name { set; get; }
        [Display(Name = "Địa chỉ")]
        public string Adress { set; get; }
        [Display(Name = "Email")]
        public string Email { set; get; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "vui lòng nhập số điện thoại")]
        public string Phone { set; get; }

    }
}