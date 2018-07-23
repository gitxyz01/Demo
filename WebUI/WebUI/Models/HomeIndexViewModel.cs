using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class HomeIndexViewModel
    {
        public int CurentSortBy { get; set; }
        public List<SanPham> Sanphams { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int CurentCategory { get; set; }
        public string TenDanhMuc { get; set; }
        public decimal CurentMinPrice { get; set; }
        public decimal CurentMaxPrice { get; set; }

    }
}