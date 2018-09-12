using Domain.Abtract;
using Domain.Concrete;
using Domain.Entities;
using Domain.Models;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class MenuService:IMenuService
    {
        private IMenuRepository menuRepository = new MenuRepository();

        public bool CheckName(MenuViewModel model, int checkType)
        {
            var checkName = menuRepository.Menus.Where(x => x.Text == model.Text).FirstOrDefault();
            if(checkType==1)
            if (checkName != null || string.IsNullOrWhiteSpace(model.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
           else
                if (string.IsNullOrWhiteSpace(model.Text))
                {
                    return false;
                }
                else
                {
                    return true;
                }
        }
        public void Create(MenuViewModel model)
        {
            Menu create = new Menu()
            {
                Text = model.Text,
                Link = model.Link,
                DisplayOrder = model.DisplayOrder,
                Status = true,
                MenuTypeId=model.MenuTypeId,
                Image=model.Image
            };
            menuRepository.Add(create);
            menuRepository.Save();
        }

        public void Delete(long id)
        {
            var model = menuRepository.GetMenuById(id);
            if (model != null)
            {
                model.Status = false;
                menuRepository.Save();
            }
        }

        public List<MenuViewModel> GetListMenus(long menuTypeId)
        {
            var model = menuRepository.Menus.Where(x=>x.MenuTypeId== menuTypeId && x.Status==true).Select(x => new MenuViewModel()
            {
                MenuID=x.MenuID,
                Text = x.Text,
                Link = x.Link,
                DisplayOrder = x.DisplayOrder,
                Status = x.Status,
                MenuTypeId = x.MenuTypeId,
                Image = x.Image
            }).ToList();
            return model;
        }

        public MenuViewModel GetMenuByID(long id)
        {
            var x = menuRepository.GetMenuById(id);
            MenuViewModel model = new MenuViewModel()
            {
                MenuID = x.MenuID,
                Text = x.Text,
                Link = x.Link,
                DisplayOrder = x.DisplayOrder,
                Status = x.Status,
                MenuTypeId = x.MenuTypeId,
                Image = x.Image
            };
            return model;
        }

        public List<MenuViewModel> GetViewBagMenu(long menuTypeId)
        {
            var model = menuRepository.Menus.Where(x => x.MenuTypeId == menuTypeId && x.Status==true).Select(x => new MenuViewModel() {
                MenuID = x.MenuID,
                Text = x.Text,
                Link = x.Link,
                DisplayOrder = x.DisplayOrder,
                Status = x.Status,
                MenuTypeId = x.MenuTypeId,
                Image = x.Image
            }).ToList();
            return model;
        }      

        public void Update(MenuViewModel model)
        {
            var x = menuRepository.GetMenuById(model.MenuID);
            if (x != null)
            {
                x.Text = model.Text;
                x.Link = model.Link;
                x.DisplayOrder = model.DisplayOrder;
                x.MenuTypeId = model.MenuTypeId;
                x.Image = model.Image;
                menuRepository.Save();
            }
        }
    }
}
