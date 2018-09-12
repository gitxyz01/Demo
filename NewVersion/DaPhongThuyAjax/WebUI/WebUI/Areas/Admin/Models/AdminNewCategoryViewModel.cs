using Domain.Concrete;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Areas.Admin.Models
{
    public class AdminCategoryViewModel
    {
        public List<DanhMucTinTuc> ListNewCategory { get; set; }
        public CreateNewCategoryViewModel createNewCategoryView { get; set; }
    }
    public class AdminNewViewModel
    {
        public List<ListNewAdminViewModel> ListNew { get; set; }
    }
}