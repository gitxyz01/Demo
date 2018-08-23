using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ShippingDetailsViewModel
    {
        [Required(ErrorMessage = "Bạn Chưa Nhập Họ Tên")]
        [DisplayName("Tên Khách Hàng:")]
        [StringLength(200)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Bạn Chưa Nhập Email")]
        [DisplayName("Địa Chỉ Email:")]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn Chưa Nhập Địa Chỉ")]
        [DisplayName("Địa Chỉ Khách Hàng:")]
        [StringLength(200)]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Bạn Chưa Nhập Số Điện Thoại")]
        [DisplayName("Số Điện Thoại:")]
        [StringLength(200)]
        public string SDT { get; set; }

        [DisplayName("Lời Nhắn")]
        public string NoiDung { get; set; }
    }
}
