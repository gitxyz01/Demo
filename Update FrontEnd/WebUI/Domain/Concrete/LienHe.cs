namespace Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LienHe")]
    public partial class LienHe
    {
        [Key]
        public int ContactID { get; set; }

        [Column(TypeName = "ntext")]
        public string ThongTinLienHe { get; set; }

        [Column(TypeName = "ntext")]
        public string TrangThai { get; set; }
    }
}
