using Domain.Abtract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
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
            var delete = context.DanhMucSanPhams.FirstOrDefault(x => x.CategoryProductID == danhMuc.CategoryProductID);
            if (delete != null)
            {
                delete.TrangThai = false;
                            
            }
            context.SaveChanges();
        }

        public void DeleteProduct(SanPham sanPham)
        {
            var delete = context.SanPhams.FirstOrDefault(x => x.ProductID == sanPham.ProductID);
            if (delete != null)
            {
                delete.TrangThai = false;              
            }
            context.SaveChanges();
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
                edit.NgayChinhSua = DateTime.Now;
            }
            context.SaveChanges();
        }

        public void SaveProduct(SanPham sanPham)
        {
            
            var edit = context.SanPhams.FirstOrDefault(x => x.ProductID == sanPham.ProductID);
            if (edit == null)
            {
                sanPham.TrangThai = true;
                sanPham.NgayTao = DateTime.Now;
                context.SanPhams.Add(sanPham);                
            }
            else
            {
                edit.MaSP = sanPham.MaSP;
                edit.Ten = sanPham.Ten;
                edit.SoLuong = sanPham.SoLuong;              
                edit.Gia = sanPham.Gia;
                edit.HinhAnh = sanPham.HinhAnh;
                edit.CategoryProductID = sanPham.CategoryProductID;
                edit.NgayTao = sanPham.NgayTao;
                edit.NgayChinhSua = DateTime.Now;
                edit.SPHot = sanPham.SPHot;
                edit.GiaKhuyenMai = sanPham.GiaKhuyenMai;
                edit.Mota = sanPham.Mota;
            }
            context.SaveChanges();           
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
