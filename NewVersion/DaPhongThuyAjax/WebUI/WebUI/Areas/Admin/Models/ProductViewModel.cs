using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public long ProductID { get; set; }

        [DisplayName("Tên Sản Phẩm")]
        [StringLength(200)]
        public string Ten { get; set; }

        [DisplayName("Mã Sản Phẩm")]
        [StringLength(50)]
        public string MaSP { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [DisplayName("Mô Tả")]
        [StringLength(500)]
        public string Mota { get; set; }

        [DisplayName("Hình Ảnh")]
        [StringLength(200)]
        public string HinhAnh { get; set; }

        [DisplayName("Giá")]
        public decimal? Gia { get; set; }

        [DisplayName("Giá Khuyến Mãi")]
        public decimal? GiaKhuyenMai { get; set; }

        [DisplayName("Số Lượng Nhập")]
        public int? SoLuongNhap { get; set; }

        [DisplayName("Số Lượng Nhập")]
        public int? SoLuongTon { get; set; }

        [DisplayName("Nội Dung Chi Tiết")]
        public string NoiDungSanPham { get; set; }

        [DisplayName("Ngày Tạo")]
        public string NgayTao { get; set; }

        [DisplayName("Người Tạo")]
        [StringLength(50)]
        public string NguoiTao { get; set; }

        [DisplayName("Ngày Chỉnh Sửa")]
        public string NgayChinhSua { get; set; }

        [StringLength(50)]
        public string NguoiChinhSua { get; set; }

        [DisplayName("Trạng Thái")]
        public bool? TrangThai { get; set; }

        [DisplayName("Sản Phẩm Hot")]
        public bool? SPHot { get; set; }

        [DisplayName("Tên Danh Mục")]
        public string CategoryProduct { get; set; }

        public long? CategoryId { get; set; }
    }
}