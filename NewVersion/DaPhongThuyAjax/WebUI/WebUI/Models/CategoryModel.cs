using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class CategoryModel
    {
        public long CategoryId {get;set;}


        [DisplayName("Tên")]
        [StringLength(200)]
        public string Ten { get; set; }

        [DisplayName("Thứ Tự Hiển Thị")]
        public int? ThuTuHienThi { get; set; }

        public string NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public string NgayChinhSua { get; set; }

        [StringLength(50)]
        public string NguoiChinhSua { get; set; }

        [DisplayName("Trạng Thái")]
        public bool? TrangThai { get; set; }

        public int SoLuongSanPham { get; set; }
    }
}