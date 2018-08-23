using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abtract
{   
    public interface IMainRepository
    {
        IQueryable<SanPham> SanPhams { get; }
        IQueryable<DanhMucSanPham> DanhMucSanPhams { get; }

        void DeleteProduct(SanPham sanPham);
        void SaveProduct(SanPham sanPham);

        void DeleteCategory(DanhMucSanPham danhMuc);
        void SaveCategory(DanhMucSanPham danhMuc);
        void RestoreAll();
        void save();
        void SetAllSalePriceNull();
    }
}
