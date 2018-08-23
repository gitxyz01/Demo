using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abtract
{
    public interface IMenuTypeRepository
    {
        IQueryable<MenuType> MenuTypes { get; }
        MenuType GetMenuTypeById(long id);
        List<MenuType> GetAllMenutype();
        void Add(MenuType menuType);
        void Delete(MenuType menuType);
        void Edit(MenuType menuType);
        void Save();
    }
}
