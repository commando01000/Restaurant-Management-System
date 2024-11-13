using CRM.Repository;
using CRM.Repository.Helpers;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace Repository_Layer.Repositories.Menu_Item
{
    public class MenuItemRepository : BasePlugin, IMenuItemRepository
    {
        public MenuItemRepository()
        {
            _Service = OrganizationServiceFactory.GetCrmService();
        }

        public void addMenuItem(Entity menuItem)
        {
            _Service.Create(menuItem);
        }

        public void deleteMenuItem(Entity menuItem)
        {
            _Service.Delete(menuItem.LogicalName, menuItem.Id);
        }

        public DataCollection<Entity> GetAllMenuItems()
        {
            string fetchXml = @"
                <fetch>
                    <entity name='initiumc_menuitem'>
                    <attribute name='initiumc_description' />
                    <attribute name='initiumc_menuitemid' />
                    <attribute name='initiumc_itemname' />
                    <attribute name='initiumc_name' />
                    <attribute name='initiumc_status' />
                    <attribute name='initiumc_price' />
                    </entity>
                </fetch>";
            return XrmExtensions.FetchMultiple(_Service, fetchXml);
        }

        public Entity GetMenuItemById(Guid id)
        {
            string fetchXml = $@"
                <fetch>
                    <entity name='initiumc_menuitem'>
                    <attribute name='initiumc_description' />
                    <attribute name='initiumc_menuitemid' />
                    <attribute name='initiumc_itemname' />
                    <attribute name='initiumc_name' /> 
                    <attribute name='initiumc_status' />
                    <attribute name='initiumc_price' />
                    <filter>
                          <condition attribute= 'initiumc_menuitemid' operator= 'eq' value= '{id}' />
                    </filter>
                    </entity>
                </fetch>";
            return XrmExtensions.FetchSingle(_Service, fetchXml);
        }

        public void updateMenuItem(Entity menuItem)
        {
            _Service.Update(menuItem);
        }
    }
}
