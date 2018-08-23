using Domain.Abtract;
using Domain.Concrete;
using Domain.Define;
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
    public class MenuTypeService : IMenuTypeService
    {
        private IMenuTypeRepository menuTypeRepository = new MenuTypeRepository();

        public bool CheckName(MenuTypeViewModel model, int checkType)
        {
            
                var checkName = menuTypeRepository.MenuTypes.Where(x => x.Name == model.Name).FirstOrDefault();
            if (checkType == (int)Define.CheckName.Create)
            {
                if (checkName != null || string.IsNullOrWhiteSpace(model.Name))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public void Create(MenuTypeViewModel model)
        {
            MenuType create = new MenuType()
            {
                Name = model.Name
            };
            menuTypeRepository.Add(create);
            menuTypeRepository.Save();           
        }

        public void Delete(long id)
        {
            var model = menuTypeRepository.GetMenuTypeById(id);
            if(model!=null)
            {
                menuTypeRepository.Delete(model);
                menuTypeRepository.Save();
            }
        }

        public List<MenuTypeViewModel> GetListMenuType()
        {
            var model = menuTypeRepository.MenuTypes.Select(x => new MenuTypeViewModel()
            {
                MenuTypeId = x.MenuTypeId,
                Name=x.Name              
            }).ToList();
            return model;
        }

        public MenuTypeViewModel GetMenuTypeByID(long id)
        {
            var x = menuTypeRepository.GetMenuTypeById(id);
            MenuTypeViewModel model = new MenuTypeViewModel() {
                MenuTypeId=x.MenuTypeId,
                Name=x.Name              
            };
            return model;

        }

        public void Update(MenuTypeViewModel model)
        {
            var x = menuTypeRepository.GetMenuTypeById(model.MenuTypeId);
            if (x != null)
            {
                x.Name = model.Name;
                menuTypeRepository.Save();
            }

        }      
    }
}
