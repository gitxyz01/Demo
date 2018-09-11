using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderViewModel
    {
        public long OrderId { get; set; }

        public long? CustomerId { get; set; }

        [DisplayName("Tên Khách Hàng")]
        [StringLength(50)]
        public string ShipName { get; set; }

        [DisplayName("Số Điện Thoại")]
        [StringLength(50)]
        public string ShipMobile { get; set; }

        [DisplayName("Địa Chỉ")]
        [StringLength(50)]
        public string ShipAdress { get; set; }

        [DisplayName("Địa Chỉ Email")]
        [StringLength(50)]
        public string ShipEmail { get; set; }

        [DisplayName("Tình Trạng Đơn Hàng")]
        public string Status { get; set; }

        [DisplayName("Ghi Chú")]
        public string Note { get; set; }

        [DisplayName("Ngày Tạo")]
        public DateTime? CreateDate { get; set; }

        [DisplayName("Người Tạo")]
        [StringLength(50)]
        public string CreateBy { get; set; }

        [DisplayName("Ngày Chỉnh Sửa")]
        public DateTime? ModifiedDate { get; set; }

        [DisplayName("Người Chỉnh Sửa")]
        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [DisplayName("Chi Tiết Đơn Hàng")]
        public List<ChiTietDonHang> ListOrderDetails { get; set; }

        public string TotalOrder { get; set; }

        public int? IntStatus { get; set; }
    }
}
