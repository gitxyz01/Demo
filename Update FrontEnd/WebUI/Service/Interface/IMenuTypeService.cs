using Domain.Concrete;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IMenuTypeService
    {
        List<MenuTypeViewModel> GetListMenuType();
        void Create(MenuTypeViewModel model);
        void Update(MenuTypeViewModel model);
        void Delete(long id);
        bool CheckName(MenuTypeViewModel model, int checkType);
        MenuTypeViewModel GetMenuTypeByID(long id);
    }    
}
