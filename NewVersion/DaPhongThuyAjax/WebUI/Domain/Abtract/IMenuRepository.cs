using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abtract
{
    public interface IMenuRepository
    {
        IQueryable<Menu> Menus { get; }
        Menu GetMenuById(long id);
        List<Menu> GetAllMenu();
        void Add(Menu menu);
        void Delete(Menu menu);
        void Edit(Menu menu);
        void Save();
    }
}
