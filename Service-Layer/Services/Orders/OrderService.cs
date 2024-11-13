using CRM.Repository;
using Repository_Layer.CustomerRepository;
using Repository_Layer.Repositories.Order;
using Repository_Layer.Repositories.OrderItem;
using Service_Layer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Xrm.Sdk;
using Repository_Layer.Repositories.Menu_Item;

namespace Service_Layer.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, IOrderItemRepository orderItemRepository, IMenuItemRepository menuItemRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderItemRepository = orderItemRepository;
            _menuItemRepository = menuItemRepository;
        }
        public void addOrder(OrderDto order)
        {
            // Check if the customer already exists
            // Use reflection to call the method to get the customer
            var method = _customerRepository.GetType().GetMethod("GetCustomerByPhoneNumber");
            var customer = method.Invoke(_customerRepository, new object[] { order.customer.Phone }) as Entity;

            Guid customerId = Guid.Empty;
            if (customer == null || customer.Id == Guid.Empty)
            {
                // Create a new customer if they don't exist
                Entity newCustomer = new Entity("initiumc_customer");
                newCustomer.Attributes["initiumc_name"] = order.customer.Name;
                newCustomer.Attributes["initiumc_phonenumber"] = order.customer.Phone;

                // Save the customer and retrieve the customer ID
                _customerRepository.AddCustomer(newCustomer);
            }
            else
            {
                // Use the existing customer ID
                customerId = customer.Id;
            }

            // Create a new order
            Entity newOrder = new Entity("initiumc_order_ss");

            // Set the order's attributes
            newOrder.Attributes["initiumc_customerid"] = new EntityReference("initiumc_customer", customerId);
            newOrder.Attributes["createdon"] = order.orderDate;
            newOrder.Attributes["discount"] = order.discount;
            newOrder.Attributes["initiumc_orderprice"] = new Money(order.orderPrice);
            newOrder.Attributes["initiumc_orderstatus"] = new OptionSetValue(order.orderStatus.Value);

            // Save the order and retrieve the order ID
            _orderRepository.addOrder(newOrder);
            var orderId = newOrder.Id;

            // Create the order items
            foreach (var orderItem in order.orderItems)
            {
                Entity newOrderItem = new Entity("initiumc_orderitem_ss");

                // Set the order item's attributes
                newOrderItem.Attributes["initiumc_related_order"] = new EntityReference("initiumc_order_ss", orderId);
                newOrderItem.Attributes["initiumc_menuitem"] = new EntityReference("initiumc_menuitem", orderItem.MenuItem.Id);
                newOrderItem.Attributes["initiumc_quantity"] = orderItem.Quantity;
                newOrderItem.Attributes["initiumc_totalprice"] = new Money(orderItem.totalPrice);

                // Save the order item
                _orderItemRepository.AddOrderItem(newOrderItem);
            }
        }

        public void deleteOrder(OrderDto order)
        {
            var Order = _orderRepository.getOrderById(order.Id);
            _orderRepository.deleteOrder(Order);
        }

        public List<OrderDto> getAllOrders()
        {
            var allOrders = _orderRepository.getOrders();
            List<OrderDto> orders = new List<OrderDto>();
            List<OrderItemDto> orderItemsList = new List<OrderItemDto>();

            foreach (var order in allOrders)
            {
                var Customer = XrmExtensions.GetEntityReference(order, "initiumc_customerid");
                var customerData = _customerRepository.GetCustomerById(Customer.Id);

                CustomerDto customer = new CustomerDto
                {
                    Id = Customer.Id,
                    Name = customerData.GetAttributeValue<string>("initiumc_name"),
                    Phone = customerData.GetAttributeValue<string>("initiumc_phonenumber")
                };

                // Use reflection to call the method to get the order items
                var method = _orderItemRepository.GetType().GetMethod("GetAllOrderItemsByOrderId");
                var orderItems = method.Invoke(_orderItemRepository, new object[] { order.Id }) as DataCollection<Entity>;

                foreach (var item in orderItems)
                {
                    var menuItemRef = XrmExtensions.GetEntityReference(item, "initiumc_menuitem");
                    var menuItem = _menuItemRepository.GetMenuItemById(menuItemRef.Id);
                    MenuItemDto menuItemDto = new MenuItemDto
                    {
                        Id = menuItemRef.Id,
                        description = menuItem.GetAttributeValue<string>("initiumc_description"),
                        itemStatus = menuItem.GetOptionSetValue("initiumc_status").Value,
                        price = menuItem.GetAttributeValue<Money>("initiumc_price").Value,
                        itemName = menuItem.GetAttributeValue<string>("initiumc_itemname"),
                    };

                    var Order = new OrderDto
                    {
                        Id = order.Id,
                        orderDate = order.GetAttributeValue<DateTime>("createdon"),
                        discount = order.GetAttributeValue<decimal>("discount"),
                        orderPrice = order.GetAttributeValue<Money>("initiumc_orderprice").Value,
                        orderStatus = order.GetOptionSetValue("initum_c_orderstatus").Value,
                        customer = customer,
                    };

                    orderItemsList.Add(new OrderItemDto
                    {
                        Id = item.Id,
                        Quantity = item.GetAttributeValue<int>("initiumc_quantity"),
                        totalPrice = item.GetAttributeValue<Money>("initiumc_totalprice").Value,
                        MenuItem = menuItemDto,
                        Order = Order
                    });
                    customer.Orders = orders;
                    orders.Add(Order);
                }
            }
            return orders;
        }

        public OrderDto getOrderById(Guid id)
        {
            // Retrieve the order by ID
            var order = _orderRepository.getOrderById(id);
            if (order == null)
            {
                return null; // Return null or handle order not found case
            }

            // Retrieve the customer associated with the order
            var customerRef = XrmExtensions.GetEntityReference(order, "initiumc_customerid");
            var customerData = _customerRepository.GetCustomerById(customerRef.Id);

            CustomerDto customer = new CustomerDto
            {
                Id = customerRef.Id,
                Name = customerData.GetAttributeValue<string>("initiumc_name"),
                Phone = customerData.GetAttributeValue<string>("initiumc_phonenumber")
            };

            // Retrieve the order items associated with this order
            var method = _orderItemRepository.GetType().GetMethod("GetAllOrderItemsByOrderId");
            var orderItems = method.Invoke(_orderItemRepository, new object[] { id }) as DataCollection<Entity>;
            List<OrderItemDto> orderItemsList = new List<OrderItemDto>();

            foreach (var item in orderItems)
            {
                // Get the menu item related to the order item
                var menuItemRef = XrmExtensions.GetEntityReference(item, "initiumc_menuitem");
                var menuItem = _menuItemRepository.GetMenuItemById(menuItemRef.Id);

                MenuItemDto menuItemDto = new MenuItemDto
                {
                    Id = menuItemRef.Id,
                    description = menuItem.GetAttributeValue<string>("initiumc_description"),
                    itemStatus = menuItem.GetOptionSetValue("initiumc_status").Value,
                    price = menuItem.GetAttributeValue<Money>("initiumc_price").Value,
                    itemName = menuItem.GetAttributeValue<string>("initiumc_itemname")
                };

                // Add the order item to the list
                orderItemsList.Add(new OrderItemDto
                {
                    Id = item.Id,
                    Quantity = item.GetAttributeValue<int>("initiumc_quantity"),
                    totalPrice = item.GetAttributeValue<Money>("initiumc_totalprice").Value,
                    MenuItem = menuItemDto
                });
            }

            // Construct and return the OrderDto
            OrderDto orderDto = new OrderDto
            {
                Id = order.Id,
                orderDate = order.GetAttributeValue<DateTime>("createdon"),
                discount = order.GetAttributeValue<decimal>("discount"),
                orderPrice = order.GetAttributeValue<Money>("initiumc_orderprice").Value,
                orderStatus = order.GetOptionSetValue("initum_c_orderstatus").Value,
                customer = customer,
                orderItems = orderItemsList
            };

            return orderDto;
        }


        public void updateOrder(OrderDto order)
        {
            throw new NotImplementedException();
        }
    }
}
