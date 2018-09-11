using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CustomerViewModel
    {
        
        public long CustomerID { get; set; }

        [DisplayName("Tên Khách Hàng")]
        [StringLength(200)]
        public string Ten { get; set; }

        [DisplayName("Địa Chỉ Email")]
        [StringLength(200)]
        public string Email { get; set; }

        [DisplayName("Địa Chỉ")]
        [StringLength(200)]
        public string DiaChi { get; set; }

        [DisplayName("Số Điện Thoại")]
        [StringLength(200)]
        public string SDT { get; set; }

        [DisplayName("Danh Sách Đơn Hàng")]
        public List<OrderViewModel> ListOrder { get; set; }
    }
}
