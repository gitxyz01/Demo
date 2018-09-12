namespace Domain.Concrete
{
    using System;
    using System.Collections.Generic;
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

        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public int? ThuTuHienThi { get; set; }

        [StringLength(500)]
        public string Mota { get; set; }

        [StringLength(200)]
        public string HinhAnh { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDungTinTuc { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public DateTime? NgayChinhSua { get; set; }

        [StringLength(50)]
        public string NguoiChinhSua { get; set; }

        public bool? TrangThai { get; set; }

        public bool? TinHot { get; set; }

        public long? NewCategoryID { get; set; }

        public int? LuotXem { get; set; }

        [StringLength(500)]
        public string Tag { get; set; }

        public virtual DanhMucTinTuc DanhMucTinTuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
