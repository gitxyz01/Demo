namespace Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucSanPham")]
    public partial class DanhMucSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMucSanPham()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public long CategoryProductID { get; set; }

        [DisplayName("Tên Danh Mục")]
        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(200)]
        public string MetaTitle { get; set; }

        [DisplayName("Thứ Tự")]
        public int? ThuTuHienThi { get; set; }

        [StringLength(200)]
        public string SeoTitle { get; set; }

        [DisplayName("Ngày Tạo")]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người Tạo")]
        [StringLength(50)]
        public string NguoiTao { get; set; }

        public DateTime? NgayChinhSua { get; set; }

        [StringLength(50)]
        public string NguoiChinhSua { get; set; }

        public bool? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
