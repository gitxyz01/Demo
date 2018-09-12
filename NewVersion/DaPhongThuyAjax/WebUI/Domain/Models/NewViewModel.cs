using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Models
{
    public class ListNewAdminViewModel
    {
        public long NewID { get; set; }

        public long? NewCategoryID { get; set; }

        [DisplayName("Tên Tin Tức")]
        public string Ten { get; set; }

        [DisplayName("Thứ Tự")]
        public int? ThuTuHienThi { get; set; }

        [DisplayName("Hình Minh Họa")]
        public string HinhAnh { get; set; }

        [DisplayName("Ngày Tạo")]
        public string NgayTao { get; set; }

        public string NgayChinhSua { get; set; }

        [DisplayName("Danh Mục")]
        public string NewCategoryName { get; set; }
        public string NoiDungTinTuc { get; set; }


        [StringLength(500)]
        public string Tag { get; set; }
        
        public bool? TrangThai { get; set; }
    }

    public class CreateNewViewModel
    {
        public CreateNewViewModel()
        {
            Tags = new HashSet<Tag>();
        }
        [DisplayName("Tên Tin Tức")]
        [StringLength(200)]
        public string Ten { get; set; }

        [DisplayName("Thứ Tự")]
        public int? ThuTuHienThi { get; set; }

        [DisplayName("Hình Bìa")]
        [StringLength(200)]
        public string HinhAnh { get; set; }

        [DisplayName("Nội Dung")]
        [Column(TypeName = "ntext")]
        public string NoiDungTinTuc { get; set; }

        [DisplayName("Danh Mục")]
        public long? NewCategoryID { get; set; }

        [StringLength(500)]
        public string Tag { get; set; }

        public virtual DanhMucTinTuc DanhMucTinTuc { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }

  
}