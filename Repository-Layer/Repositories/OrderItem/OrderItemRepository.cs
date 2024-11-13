using CRM.Repository;
using CRM.Repository.Helpers;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Repositories.OrderItem
{
    public class OrderItemRepository : BasePlugin, IOrderItemRepository
    {
        public OrderItemRepository()
        {
            _Service = OrganizationServiceFactory.GetCrmService();
        }

        public void AddOrderItem(Entity orderItem)
        {
            _Service.Create(orderItem);
        }

        public void DeleteOrderItem(Entity orderItem)
        {
            _Service.Delete(orderItem.LogicalName, orderItem.Id);
        }

        public DataCollection<Entity> GetAllOrderItems()
        {
            string fetchXml = $@"<fetch top=""50"">
                              <entity name=""initiumc_orderitem_ss"">
                                <attribute name=""initiumc_menuitem"" />
                                <attribute name=""initiumc_quantity"" />
                                <attribute name=""initiumc_name"" />
                                <attribute name=""createdon"" />
                                <attribute name=""initiumc_totalprice"" />
                                <attribute name=""initiumc_related_order"" />
                                <attribute name=""initiumc_orderitem_ssid"" />
                              </entity>
                            </fetch>";

            return XrmExtensions.FetchMultiple(_Service, fetchXml);
        }

        public DataCollection<Entity> GetAllOrderItemsByOrderId(Guid orderId)
        {
            string fetchXml = $@"<fetch>
                              <entity name=""initiumc_orderitem_ss"">
                                <attribute name=""initiumc_menuitem"" />
                                <attribute name=""initiumc_quantity"" />
                                <attribute name=""initiumc_name"" />
                                <attribute name=""createdon"" />
                                <attribute name=""initiumc_totalprice"" />
                                <attribute name=""initiumc_related_order"" />
                                <attribute name=""initiumc_orderitem_ssid"" />
                                <filter>
                                      <condition attribute=""initiumc_related_order"" operator=""eq"" value=""{orderId}"" />
                                </filter>
                              </entity>
                            </fetch>";

            return XrmExtensions.FetchMultiple(_Service, fetchXml);
        }

        public Entity GetOrderItemById(int id)
        {
            string fetchXml = $@"<fetch>
                              <entity name=""initiumc_orderitem_ss"">
                                <attribute name=""initiumc_menuitem"" />
                                <attribute name=""initiumc_quantity"" />
                                <attribute name=""initiumc_name"" />
                                <attribute name=""createdon"" />
                                <attribute name=""initiumc_totalprice"" />
                                <attribute name=""initiumc_related_order"" />
                                <attribute name=""initiumc_orderitem_ssid"" />
                                <filter>
                                      <condition attribute=""initiumc_orderitem_ssid"" operator=""eq"" value=""{id}"" />
                                </filter>
                              </entity>
                            </fetch>";

            return XrmExtensions.FetchSingle(_Service, fetchXml);
        }

        public void UpdateOrderItem(Entity orderItem)
        {
            _Service.Update(orderItem);
        }
    }
}
