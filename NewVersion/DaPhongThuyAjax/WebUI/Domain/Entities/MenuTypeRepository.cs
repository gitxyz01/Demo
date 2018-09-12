using Domain.Abtract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MenuTypeRepository : IMenuTypeRepository
    {
        private DaPhongThuyEF context = new DaPhongThuyEF();

        public IQueryable<MenuType> MenuTypes => context.MenuTypes;

        public void Add(MenuType menuType)
        {
            context.MenuTypes.Add(menuType);
        }
   
        public void Delete(MenuType menuType)
        {
            context.MenuTypes.Remove(menuType);
        }

        public void Edit(MenuType menuType)
        {
            context.MenuTypes.Attach(menuType);
            
        }

        public MenuType GetMenuTypeById(long id)
        {
            return context.MenuTypes.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        List<MenuType> IMenuTypeRepository.GetAllMenutype()
        {
            return context.MenuTypes.ToList();
        }
    }
}
