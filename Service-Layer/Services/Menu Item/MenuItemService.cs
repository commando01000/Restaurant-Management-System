using CRM.Repository;
using Microsoft.Xrm.Sdk;
using Repository_Layer.Repositories.Menu_Item;
using Service_Layer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Services.Menu_Item
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }
        public void addMenuItem(MenuItemDto menuItem)
        {
            throw new NotImplementedException();
        }

        public void deleteMenuItem(MenuItemDto menuItem)
        {
            throw new NotImplementedException();
        }

        public List<MenuItemDto> getAllMenuItems()
        {
            var menuItems = _menuItemRepository.GetAllMenuItems();
            List<MenuItemDto> MenuItemsDto = new List<MenuItemDto>();

            foreach (var item in menuItems)
            {
                var menuItemDto = new MenuItemDto
                {
                    Id = item.Id,
                    description = item.GetAttributeValue<string>("initiumc_description"),
                    itemName = item.GetAttributeValue<string>("initiumc_itemname"),
                    itemStatus = item.GetOptionSetValue("initiumc_status").Value,
                    price = item.GetAttributeValue<Money>("initiumc_price").Value,
                };
                MenuItemsDto.Add(menuItemDto);
            }
            return MenuItemsDto;
        }

        public MenuItemDto getMenuItemById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void updateMenuItem(MenuItemDto menuItem)
        {
            throw new NotImplementedException();
        }
    }
}
