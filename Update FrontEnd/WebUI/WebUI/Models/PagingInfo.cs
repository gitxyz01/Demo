using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int TotalItem { get; set; }
        public int ItemPerPage { get; set; }
        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItem / ItemPerPage);
            }
        }

    }
}