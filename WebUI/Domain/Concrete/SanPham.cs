namespace Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

        [DisplayName("Tên")]
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

        [Column(TypeName = "xml")]
        public string MucHinhAnh { get; set; }

        [DisplayName("Giá")]
        public decimal? Gia { get; set; }

        [DisplayName("Giá Khuyến Mãi")]
        public decimal? GiaKhuyenMai { get; set; }

        [DisplayName("Số Lượng")]
        public int? SoLuong { get; set; }

        [DisplayName("Thông Tin Chi Tiết")]
        [Column(TypeName = "ntext")]
        public string NoiDungSanPham { get; set; }

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

        [DisplayName("Sản Phẩm Hot")]
        public bool? SPHot { get; set; }

        [DisplayName("Danh Mục")]
        public long? CategoryProductID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual DanhMucSanPham DanhMucSanPham { get; set; }
    }
}
