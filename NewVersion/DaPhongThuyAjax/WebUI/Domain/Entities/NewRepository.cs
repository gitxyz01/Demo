using Domain.Abtract;
using Domain.Concrete;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
            try
            {
                var delete = context.TinTucs.FirstOrDefault(x => x.NewID == New.NewID);
                if (delete != null)
                {
                    context.TinTucs.Remove(delete);
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

        public TinTuc GetAboutNew()
        {
            return context.TinTucs.Where(x => x.TrangThai == true && x.DanhMucTinTuc.ParentID==8).FirstOrDefault();
        }

        public List<TinTuc> GetAllDeletedNews()
        {
            return context.TinTucs.Where(x => x.TrangThai == false).ToList();
        }

        public List<TinTuc> GetAllNews()
        {
            return context.TinTucs.ToList();
        }

        public List<ListNewAdminViewModel> GetListNewAdmin(long newCategoryId=0)
        {
            return context.TinTucs.Where(x => newCategoryId == 0 || x.NewCategoryID == newCategoryId).Select(x => new ListNewAdminViewModel() {
                TrangThai = x.TrangThai,
                NewID = x.NewID,
                Ten = x.Ten,
                ThuTuHienThi = x.ThuTuHienThi,
                HinhAnh = x.HinhAnh,
                NgayTao = x.NgayTao.ToString(),
                NgayChinhSua=x.NgayChinhSua.ToString(),
                NewCategoryName = x.DanhMucTinTuc.Ten,
                NewCategoryID = x.NewCategoryID
            }).ToList();
        }


        public TinTuc GetNewById(long id)
        {
            return context.TinTucs.Where(x => x.NewID == id).FirstOrDefault();
        }

        public ListNewAdminViewModel GetNewByIds(long id)
        {
            return context.TinTucs.Where(x => x.NewID == id).Select(x => new ListNewAdminViewModel()
            {
                TrangThai = x.TrangThai,
                NewID = x.NewID,
                Ten = x.Ten,
                ThuTuHienThi = x.ThuTuHienThi,
                HinhAnh = x.HinhAnh,
                NgayTao = x.NgayTao.ToString(),
                NgayChinhSua = x.NgayChinhSua.ToString(),
                NewCategoryName = x.DanhMucTinTuc.Ten,
                NewCategoryID = x.NewCategoryID,
                NoiDungTinTuc = x.NoiDungTinTuc
            }).FirstOrDefault();
        }

        public void SaveNew(TinTuc New)
        {
                 
            var saveNew = GetNewById(New.NewID);
            try { 
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
                saveNew.TrangThai = New.TrangThai;
                saveNew.NoiDungTinTuc = New.NoiDungTinTuc;
                saveNew.NgayChinhSua = DateTime.Now;
            }
            context.SaveChanges();
            }
            catch(Exception es)
            {
                throw new Exception(es.Message);
            }
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
                    .Where(x => newCategoryId == 0 && x.TrangThai == true  && x.DanhMucTinTuc.ParentID==10014 || x.NewCategoryID == newCategoryId && x.TrangThai == true && x.DanhMucTinTuc.ParentID==10014)
                    .Select(x => new ListNewAdminViewModel()
                    {
                        NewID = x.NewID,
                        NewCategoryID=x.NewCategoryID,
                        TrangThai =x.TrangThai,
                       
                        Ten = x.Ten,
                        ThuTuHienThi = x.ThuTuHienThi,
                        HinhAnh = x.HinhAnh,
                        NgayTao = x.NgayTao.ToString(),
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
                   .Where(x => newCategoryId == 0 && x.TrangThai == true &&x.DanhMucTinTuc.ParentID==10015 || x.NewCategoryID == newCategoryId && x.TrangThai == true && x.DanhMucTinTuc.ParentID == 10015)
                   .Select(x => new ListNewAdminViewModel()
                   {
                       NewID = x.NewID,
                       NewCategoryID = x.NewCategoryID,
                       TrangThai = x.TrangThai,
                       Ten = x.Ten,
                       ThuTuHienThi = x.ThuTuHienThi,
                       HinhAnh = x.HinhAnh,
                       NgayTao = x.NgayTao.ToString(),
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
