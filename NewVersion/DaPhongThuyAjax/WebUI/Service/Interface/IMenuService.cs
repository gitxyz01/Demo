using Domain.Concrete;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IMenuService
    {
        List<MenuViewModel> GetListMenus(long menuTypeId);
        void Create(MenuViewModel model);
        void Update(MenuViewModel model);
        void Delete(long id);
        bool CheckName(MenuViewModel model, int checkType);
        MenuViewModel GetMenuByID(long id);
        List<MenuViewModel> GetViewBagMenu(long menuTypeId);
    }
}
