namespace Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioiThieu")]
    public partial class GioiThieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AboutID { get; set; }

        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

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
    }
}
