using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Areas.Admin.Models
{
    public class NewCategoryViewModel
    {
        public long NewCategoryID { get; set; }

        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(200)]
        public string MetaTitle { get; set; }

        public int? ThuTuHienThi { get; set; }

        [StringLength(200)]
        public string SeoTitle { get; set; }

        public string NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public string NgayChinhSua { get; set; }

        [StringLength(50)]
        public string NguoiChinhSua { get; set; }

        public bool? TrangThai { get; set; }

        public long? ParentID { get; set; }

    }
    public class CreateNewCategoryViewModel
    {
        [DisplayName("Tên Danh Mục")]
        [StringLength(200)]
        public string Ten { get; set; }

        [DisplayName("Thứ Tự")]
        public int? ThuTuHienThi { get; set; }
      
        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public bool? TrangThai { get; set; }
    }
}