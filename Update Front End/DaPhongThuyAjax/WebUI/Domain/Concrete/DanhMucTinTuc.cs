namespace Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucTinTuc")]
    public partial class DanhMucTinTuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMucTinTuc()
        {
            TinTucs = new HashSet<TinTuc>();
        }

        [Key]
        public long NewCategoryID { get; set; }

        [DisplayName("Tên Danh Mục")]
        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(200)]
        public string MetaTitle { get; set; }

        [DisplayName("Thứ Tự Hiển Thị")]
        public int? ThuTuHienThi { get; set; }

        [StringLength(200)]
        public string SeoTitle { get; set; }

        [DisplayName("Ngày Tạo")]
        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public DateTime? NgayChinhSua { get; set; }

        [StringLength(50)]
        public string NguoiChinhSua { get; set; }

        [DisplayName("Trạng Thái")]
        public bool? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
