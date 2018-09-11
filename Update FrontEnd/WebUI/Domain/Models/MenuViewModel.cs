using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MenuViewModel
    {
        public long MenuID { get; set; }

        [StringLength(50)]
        [DisplayName("Tên")]
        public string Text { get; set; }

        [DisplayName("Đường Dẫn")]
        [StringLength(250)]
        public string Link { get; set; }

        [DisplayName("Thứ Tự Hiển Thị")]
        public int? DisplayOrder { get; set; }

        [DisplayName("Trạng Thái")]
        public bool? Status { get; set; }

        public long? MenuTypeId { get; set; }

        [DisplayName("Hình Ảnh")]
        [StringLength(250)]
        public string Image { get; set; }
    }
}
