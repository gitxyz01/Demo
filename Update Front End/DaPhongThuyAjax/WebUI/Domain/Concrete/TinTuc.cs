namespace Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TinTuc()
        {
            Tags = new HashSet<Tag>();
        }

        [Key]
        public long NewID { get; set; }

        [DisplayName("Tên Tin Tức")]
        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [DisplayName("Thứ Tự Hiển Thị")]
        public int? ThuTuHienThi { get; set; }

        [DisplayName("Mô Tả")]
        [StringLength(500)]
        public string Mota { get; set; }

        [DisplayName("Hình Ảnh")]
        [StringLength(200)]
        public string HinhAnh { get; set; }

        [DisplayName("Nội Dung Tin Tức")]
        [Column(TypeName = "ntext")]
        public string NoiDungTinTuc { get; set; }

        [DisplayName("Ngày Tạo")]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người Tạo")]
        [StringLength(50)]
        public string NguoiTao { get; set; }

        [DisplayName("Ngày Chỉnh Sửa")]
        public DateTime? NgayChinhSua { get; set; }

        [DisplayName("Người Chỉnh Sửa")]
        [StringLength(50)]
        public string NguoiChinhSua { get; set; }

        [DisplayName("Trạng Thái")]
        public bool? TrangThai { get; set; }

        public bool? TinHot { get; set; }

        [DisplayName("Danh Mục")]
        public long? NewCategoryID { get; set; }

        [DisplayName("Lượt Xem")]
        public int? LuotXem { get; set; }

        [StringLength(500)]
        public string Tag { get; set; }

        public virtual DanhMucTinTuc DanhMucTinTuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
