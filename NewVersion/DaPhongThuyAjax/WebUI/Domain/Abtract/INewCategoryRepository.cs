using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;

namespace Domain.Abtract
{
    public interface INewCategoryRepository
    {
        IQueryable<TinTuc> News { get; }
        IQueryable<DanhMucTinTuc> NewCategory { get; }
        void Save(DanhMucTinTuc NewCategory);
        void Delete(DanhMucTinTuc NewCategory);
        List<DanhMucTinTuc> GetAllActive(string Newblog);
        DanhMucTinTuc GetById(long id);
        List<DanhMucTinTuc> GetAllDeletedNewCategory();

        List<NewCategoryViewModel> GetNewCategories();
        List<NewCategoryViewModel> GetNewCategoriesActive();
        NewCategoryViewModel LoadDetail(long id);


    }
}
