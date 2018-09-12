namespace Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        public long ProductID { get; set; }

        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(50)]
        public string MaSP { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string Mota { get; set; }

        [StringLength(200)]
        public string HinhAnh { get; set; }

        [Column(TypeName = "xml")]
        public string MucHinhAnh { get; set; }

        public decimal? Gia { get; set; }

        public decimal? GiaKhuyenMai { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDungSanPham { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public DateTime? NgayChinhSua { get; set; }

        [StringLength(50)]
        public string NguoiChinhSua { get; set; }

        public bool? TrangThai { get; set; }

        public bool? SPHot { get; set; }

        public long? CategoryProductID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual DanhMucSanPham DanhMucSanPham { get; set; }
    }
}
