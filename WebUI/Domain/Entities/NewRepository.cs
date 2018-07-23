using Domain.Abtract;
using Domain.Concrete;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NewRepository : INewRepository
    {
        private DaPhongThuyEF context = new DaPhongThuyEF();


        public IQueryable<TinTuc> News => context.TinTucs;
        
        public void Delete(TinTuc New)
        {
            var deleteNew = GetNewById(New.NewID);
            if (deleteNew != null)
            {
                deleteNew.TrangThai = false;
            }
            context.SaveChanges();
        }

        public List<TinTuc> GetAllDeletedNews()
        {
            return context.TinTucs.Where(x => x.TrangThai == false).ToList();
        }

        public List<TinTuc> GetAllNews()
        {
            return context.TinTucs.ToList();
        }

        public List<ListNewAdminViewModel> GetListNewAdmin(int newCategoryId=0)
        {
            return context.TinTucs.Where(x=>newCategoryId == 0 && x.TrangThai == true || x.NewCategoryID==newCategoryId && x.TrangThai == true).Select(x => new ListNewAdminViewModel() {
                TrangThai=x.TrangThai,
                NewID=x.NewID,
                Ten =x.Ten,
                ThuTuHienThi=x.ThuTuHienThi,
                HinhAnh=x.HinhAnh,
                NgayTao=x.NgayTao,
                NewCategoryName=x.DanhMucTinTuc.Ten                
            }).ToList();
        }

        public TinTuc GetNewById(long id)
        {
            return context.TinTucs.Where(x => x.NewID == id).FirstOrDefault();
        }

        public void SaveNew(TinTuc New)
        {
                                                
            var saveNew = GetNewById(New.NewID);
            
            if (saveNew == null)
            {
                New.TrangThai = true;
                New.NgayTao = DateTime.Now;
                context.TinTucs.Add(New);
            }
            else
            {
                saveNew.LuotXem = New.LuotXem;
                saveNew.Ten = New.Ten;
                saveNew.NoiDungTinTuc = New.NoiDungTinTuc;
                saveNew.NewCategoryID = New.NewCategoryID;
                saveNew.ThuTuHienThi = New.ThuTuHienThi;
                saveNew.HinhAnh = New.HinhAnh;
            }
            context.SaveChanges();
        }

        public void SetViewCount()
        {
            var model = context.TinTucs.ToList();
            foreach (var item in model)
            {
                item.LuotXem = 0;
            }
            context.SaveChanges();
        }

        List<ListNewAdminViewModel> INewRepository.GetAllNewCategoryIndex(int page, string newOrBlog,long newCategoryId=0 )
        {
            if (newOrBlog == "News")
            {            
            return context.TinTucs
                    .Where(x => newCategoryId == 0 && x.TrangThai == true && x.DanhMucTinTuc.ThuTuHienThi < 1000 || x.NewCategoryID == newCategoryId && x.TrangThai == true && x.DanhMucTinTuc.ThuTuHienThi < 1000)
                    .Select(x => new ListNewAdminViewModel()
                    {
                        NewID = x.NewID,
                        NewCategoryID=x.NewCategoryID,
                        TrangThai =x.TrangThai,
                       
                        Ten = x.Ten,
                        ThuTuHienThi = x.ThuTuHienThi,
                        HinhAnh = x.HinhAnh,
                        NgayTao = x.NgayTao,
                        NewCategoryName = x.DanhMucTinTuc.Ten
                    })
                    .OrderByDescending(x => x.NewID)
                    .Skip((page - 1) * 10)
                    .Take(10)                    
                    .ToList();
            }
            else
            {
                return context.TinTucs
                   .Where(x => newCategoryId == 0 && x.TrangThai == true &&x.DanhMucTinTuc.ThuTuHienThi>1000|| x.NewCategoryID == newCategoryId && x.TrangThai == true && x.DanhMucTinTuc.ThuTuHienThi > 1000)
                   .Select(x => new ListNewAdminViewModel()
                   {
                       NewID = x.NewID,
                       NewCategoryID = x.NewCategoryID,
                       TrangThai = x.TrangThai,

                       Ten = x.Ten,
                       ThuTuHienThi = x.ThuTuHienThi,
                       HinhAnh = x.HinhAnh,
                       NgayTao = x.NgayTao,
                       NewCategoryName = x.DanhMucTinTuc.Ten
                   })
                   .OrderByDescending(x => x.NewID)
                   .Skip((page - 1) * 10)
                   .Take(10)
                   .ToList();
            }
        }
    }
}
