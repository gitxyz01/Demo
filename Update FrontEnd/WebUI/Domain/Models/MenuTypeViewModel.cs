using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MenuTypeViewModel
    {        
        public long MenuTypeId { get; set; }

        [DisplayName("Tên Menu")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
