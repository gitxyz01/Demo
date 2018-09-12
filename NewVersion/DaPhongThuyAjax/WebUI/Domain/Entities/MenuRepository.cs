using Domain.Abtract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MenuRepository : IMenuRepository
    {
        private DaPhongThuyEF context = new DaPhongThuyEF();

        public IQueryable<Menu> Menus => context.Menus;

        public void Add(Menu menu)
        {
            context.Menus.Add(menu);
        }

        public void Delete(Menu menu)
        {
            context.Menus.Remove(menu);
        }

        public void Edit(Menu menu)
        {
            context.Menus.Attach(menu);

        }

        public Menu GetMenuById(long id)
        {
            return context.Menus.Find(id);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        List<Menu> IMenuRepository.GetAllMenu()
        {
            return context.Menus.ToList();
        }
    }
}
