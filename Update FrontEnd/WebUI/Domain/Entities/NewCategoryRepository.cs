using Domain.Abtract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NewCategoryRepository : INewCategoryRepository
    {
        private DaPhongThuyEF context = new DaPhongThuyEF();

        public IQueryable<DanhMucTinTuc> NewCategory => context.DanhMucTinTucs;

        public void Delete(DanhMucTinTuc NewCategory)
        {
            var deleteNew = GetById(NewCategory.NewCategoryID);
            if (deleteNew != null)
            {
                deleteNew.TrangThai = false;
            }
          
            context.SaveChanges();
        }

        public List<DanhMucTinTuc> GetAllActive(string newBlog)
        {
            if (newBlog == "News")
            {
                return context.DanhMucTinTucs.Where(x => x.TrangThai == true && x.ThuTuHienThi < 1000)
                    .OrderBy(x => x.ThuTuHienThi)
                    .ToList();
            }
            if(newBlog=="Blogs")
            {
                return context.DanhMucTinTucs.Where(x => x.TrangThai == true && x.ThuTuHienThi >= 1000)
                   .OrderBy(x => x.ThuTuHienThi)
                   .ToList();
            }
            else
            {
                return context.DanhMucTinTucs.Where(x => x.TrangThai == true )
                  .OrderBy(x => x.ThuTuHienThi)
                  .ToList();
            }

        }
     

        public List<DanhMucTinTuc> GetAllDeletedNewCategory()
        {
            return context.DanhMucTinTucs.Where(x => x.TrangThai == false).ToList();
        }

        

        public DanhMucTinTuc GetById(long id)
        {
            return context.DanhMucTinTucs.Find(id);
        }

        public void Save(DanhMucTinTuc NewCategory)
        {
            var saveNewCategory = GetById(NewCategory.NewCategoryID);
                                              
            if(saveNewCategory==null)
            {
                NewCategory.TrangThai = true;
                NewCategory.NgayTao = DateTime.Now;
                context.DanhMucTinTucs.Add(NewCategory);             
            }
            else
            {
                saveNewCategory.Ten = NewCategory.Ten;
                saveNewCategory.ThuTuHienThi = NewCategory.ThuTuHienThi;               
            }
            context.SaveChanges();
        }
    }
}
