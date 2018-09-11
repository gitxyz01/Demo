using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class AdminProductListModel
    {
        public List<SanPham> SanPhams { get; set; }
        public SanPham SanPham { get; set; }
        public List<DanhMucSanPham> DanhMucSanPhams { get; set; }       
    }
}