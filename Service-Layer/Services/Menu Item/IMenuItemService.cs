using Microsoft.Xrm.Sdk;
using Service_Layer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Services.Menu_Item
{
    public interface IMenuItemService
    {
        void addMenuItem(MenuItemDto menuItem);
        void updateMenuItem(MenuItemDto menuItem);
        void deleteMenuItem(MenuItemDto menuItem);
        List<MenuItemDto> getAllMenuItems();
        MenuItemDto getMenuItemById(Guid id);
    }
}
