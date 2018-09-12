using Domain.Abtract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;

namespace Domain.Entities
{
    public class NewCategoryRepository : INewCategoryRepository
    {
        private DaPhongThuyEF context = new DaPhongThuyEF();

        public IQueryable<DanhMucTinTuc> NewCategory => context.DanhMucTinTucs;

        public IQueryable<TinTuc> News => context.TinTucs;


        public void Delete(DanhMucTinTuc NewCategory)
        {
            try
            {
                var delete = context.DanhMucTinTucs.FirstOrDefault(x => x.NewCategoryID == NewCategory.NewCategoryID);
                if (delete != null)
                {
                    var listParentId = context.DanhMucTinTucs.Select(x => x.ParentID).ToList();
                    var check = true;
                    if(NewCategory.NewCategoryID==10014|| NewCategory.NewCategoryID == 10015|| NewCategory.NewCategoryID == 8)
                    {
                        check = false;
                    }
                    foreach (var parentId in listParentId)
                    {
                        if (parentId == NewCategory.NewCategoryID)
                        {
                            check = false;
                        }
                    }
                    if (check == false)
                    {
                        throw new Exception("Bạn Không Thể Xóa Danh Mục Cha");
                    }
                    context.DanhMucTinTucs.Remove(delete);
                }
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlex = ex.InnerException.InnerException as SqlException;

                if (sqlex != null)
                {
                    switch (sqlex.Number)
                    {
                        case 547: throw new Exception("Xóa Thất Bại - Danh Mục Đang Được Sử Dụng Ở Chức Năng Khác !"); //FK exception
                        case 2627:
                        case 2601:
                            throw new Exception("Lỗi Khóa Chính"); //primary key exception

                        default: throw sqlex; //otra excepcion que no controlo.
                    }
                }
                throw ex;
            }
        }

        public List<DanhMucTinTuc> GetAllActive(string newBlog)
        {
            if (newBlog == "News")
            {
                return context.DanhMucTinTucs.Where(x => x.TrangThai == true &&x.ParentID==10014)
                    .OrderBy(x => x.ThuTuHienThi)
                    .ToList();
            }
            if(newBlog=="Blogs")
            {
                return context.DanhMucTinTucs.Where(x => x.TrangThai == true && x.ThuTuHienThi >= 10015)
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

        public List<NewCategoryViewModel> GetNewCategories()
        {
            //var test = context.DanhMucTinTucs.ToList();
            //foreach (var item in test)
            //{
            //    item.ParentID = 0;
            //}
            context.SaveChanges();
            var model = context.DanhMucTinTucs.Select(x => new NewCategoryViewModel(){
                NewCategoryID = x.NewCategoryID,
                Ten=x.Ten,
                ThuTuHienThi=x.ThuTuHienThi,
                TrangThai=x.TrangThai,
                ParentID=x.ParentID,
                NgayTao=x.NgayTao.ToString(),
                NgayChinhSua=x.NgayChinhSua.ToString(),
                NguoiTao=x.NguoiTao,
                NguoiChinhSua=x.NguoiChinhSua,

            }).ToList();
            return model;


        }

        public List<NewCategoryViewModel> GetNewCategoriesActive()
        {
            var model = context.DanhMucTinTucs.Where(x => x.TrangThai == true).Select(x => new NewCategoryViewModel()
            {
                NewCategoryID = x.NewCategoryID,
                Ten = x.Ten,
                ThuTuHienThi = x.ThuTuHienThi,
                TrangThai = x.TrangThai,
                ParentID = x.ParentID,
                NgayTao = x.NgayTao.ToString(),
                NgayChinhSua = x.NgayChinhSua.ToString(),
                NguoiTao = x.NguoiTao,
                NguoiChinhSua = x.NguoiChinhSua
            }).ToList();
            return model;
        }

        public NewCategoryViewModel LoadDetail(long id)
        {
            var model = context.DanhMucTinTucs.Where(x => x.NewCategoryID == id).Select(x=> new NewCategoryViewModel() {
                NewCategoryID = x.NewCategoryID,
                Ten = x.Ten,
                ThuTuHienThi = x.ThuTuHienThi,
                TrangThai = x.TrangThai,
                ParentID = x.ParentID,
                NgayTao = x.NgayTao.ToString(),
                NgayChinhSua = x.NgayChinhSua.ToString(),
                NguoiTao = x.NguoiTao,
                NguoiChinhSua = x.NguoiChinhSua
            }).FirstOrDefault();
            return model;
        }

        public void Save(DanhMucTinTuc NewCategory)
        {
            var saveNewCategory = GetById(NewCategory.NewCategoryID);

            try {
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
                saveNewCategory.NgayChinhSua = DateTime.Now;
                saveNewCategory.NguoiChinhSua = NewCategory.NguoiChinhSua;
                saveNewCategory.TrangThai = NewCategory.TrangThai;
                saveNewCategory.ParentID = NewCategory.ParentID;             
                var model = context.TinTucs.Where(x => x.NewCategoryID == saveNewCategory.NewCategoryID).ToList();
                foreach (var item in model)
                {
                        item.TrangThai = NewCategory.TrangThai;
                }
                }
            context.SaveChanges();

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
