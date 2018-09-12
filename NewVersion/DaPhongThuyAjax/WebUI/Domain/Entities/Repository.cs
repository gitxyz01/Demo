using Domain.Abtract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    
    public class Repository: IMainRepository
    {
        private DaPhongThuyEF context = new DaPhongThuyEF();

        public IQueryable<SanPham> SanPhams => context.SanPhams;
        public IQueryable<DanhMucSanPham> DanhMucSanPhams => context.DanhMucSanPhams;

        public void DeleteCategory(DanhMucSanPham danhMuc)
        {
            try
            {             
                var delete = context.DanhMucSanPhams.FirstOrDefault(x => x.CategoryProductID == danhMuc.CategoryProductID);
                if (delete != null)
                {
                    context.DanhMucSanPhams.Remove(delete);

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
                        case 547: throw new Exception("Xóa Thất Bại - Danh Mục Này Đang Được Sủ Dụng Ở Chức Năng Khác !"); //FK exception
                        case 2627:
                        case 2601:
                            throw new Exception("Lỗi Khóa Chính"); //primary key exception

                        default: throw sqlex; //otra excepcion que no controlo.
                    }
                }
                throw ex;
            }
        }

        public void DeleteProduct(SanPham sanPham)
        {
            try
            {
                var delete = context.SanPhams.FirstOrDefault(x => x.ProductID == sanPham.ProductID);
                if (delete != null)
                {
                    context.SanPhams.Remove(delete);

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
                        case 547: throw new Exception("Xóa Thất Bại - Sản Phẩm Này Đang Được Sủ Dụng Ở Chức Năng Khác !"); //FK exception
                        case 2627:
                        case 2601:
                            throw new Exception("Lỗi Khóa Chính"); //primary key exception

                        default: throw sqlex; //otra excepcion que no controlo.
                    }
                }
                throw ex;
            }
        }

        public void SaveCategory(DanhMucSanPham danhMuc)
        {
            var edit = context.DanhMucSanPhams.FirstOrDefault(x => x.CategoryProductID == danhMuc.CategoryProductID);
            if (edit == null)
            {
                danhMuc.TrangThai = true;
                context.DanhMucSanPhams.Add(danhMuc);
            }
            else
            {
                edit.Ten = danhMuc.Ten;
                edit.ThuTuHienThi = danhMuc.ThuTuHienThi;
                edit.TrangThai = danhMuc.TrangThai;
                edit.NgayChinhSua = DateTime.Now;
            }
            context.SaveChanges();
        }

        public bool SaveProduct(SanPham sanPham)
        {
            var edit = context.SanPhams.FirstOrDefault(x => x.ProductID == sanPham.ProductID);
            try
            {           
            if (edit == null)
            {
                    var productByCode = context.SanPhams.Where(x => x.MaSP == sanPham.MaSP).FirstOrDefault();
                    if (productByCode != null)
                    {
                        throw new Exception("Mã Sản Phẩm Đã Tồn Tại Trong Hệ Thống");

                    }
                    sanPham.TrangThai = true;
                    sanPham.NgayTao = DateTime.Now;
                    context.SanPhams.Add(sanPham);
                    context.SaveChanges();
                    return true;
                }
            else
            {
                    var productByCode = context.SanPhams.Where(x => x.MaSP == sanPham.MaSP).FirstOrDefault();
                    if (productByCode != null && productByCode.ProductID != sanPham.ProductID)
                    {
                        throw new Exception("Mã Sản Phẩm Đã Tồn Tại Trong Hệ Thống!");
                    }
                edit.MaSP = sanPham.MaSP;
                edit.Ten = sanPham.Ten;
                edit.SoLuong = sanPham.SoLuong;              
                edit.Gia = sanPham.Gia;
                edit.HinhAnh = sanPham.HinhAnh;
                edit.CategoryProductID = sanPham.CategoryProductID;
                edit.NgayChinhSua = DateTime.Now;
                edit.SPHot = sanPham.SPHot;
                edit.Mota = sanPham.Mota;
                edit.TrangThai = sanPham.TrangThai;
                    context.SaveChanges();
                    return true;
                }                          
            }
            
            catch(Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void RestoreAll()
        {
            var listProducts = context.SanPhams.ToList();
            foreach (var item in listProducts)
            {
                item.TrangThai = true;
            }
            var listCategories = context.DanhMucSanPhams.ToList();
            foreach (var item in listCategories)
            {
                item.TrangThai = true;
            }
            context.SaveChanges();
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void SetAllSalePriceNull()
        {
            foreach (var product in context.SanPhams)
            {
                product.GiaKhuyenMai = null;
            }
            context.SaveChanges();
        }
    }
}
