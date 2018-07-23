using Domain.Concrete;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abtract
{
    public interface INewRepository
    {
        IQueryable<TinTuc> News { get; }
        void SaveNew(TinTuc New);
        void Delete(TinTuc New);
        List<TinTuc> GetAllNews();
        TinTuc GetNewById(long id);
        List<TinTuc> GetAllDeletedNews();
        List<ListNewAdminViewModel> GetListNewAdmin(int newCategoryId=0);
        List<ListNewAdminViewModel> GetAllNewCategoryIndex(int page, string newOrBlog, long newCategoryId);
        void SetViewCount();
    }
}
