using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Repositories.Menu_Item
{
    public interface IMenuItemRepository
    {
        DataCollection<Entity> GetAllMenuItems();
        Entity GetMenuItemById(Guid id);
        void addMenuItem(Entity menuItem);
        void updateMenuItem(Entity menuItem);
        void deleteMenuItem(Entity menuItem);
    }
}
