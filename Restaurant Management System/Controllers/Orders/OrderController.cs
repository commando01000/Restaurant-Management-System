using Microsoft.Xrm.Sdk;
using Service_Layer.Dtos;
using Service_Layer.Services;
using Service_Layer.Services.Customer;
using Service_Layer.Services.Menu_Item;
using Service_Layer.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Management_System.Controllers.Orders
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMenuItemService _menuItemService;
        private readonly IOrderItemService _orderItemService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, IMenuItemService menuItemService, IOrderItemService orderItemService, ICustomerService customerService)
        {
            _orderService = orderService;
            _menuItemService = menuItemService;
            _orderItemService = orderItemService;
            _customerService = customerService;
        }
        // GET: Order
        public ActionResult Index()
        {
            var allOrders = _orderService.getAllOrders();
            return View(allOrders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var menuItems = _menuItemService.getAllMenuItems();
            ViewBag.MenuItems = menuItems;

            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // Retrieve discount
                var discount = Convert.ToDecimal(collection["discount"]);

                // Retrieve total price if needed
                var totalPrice = Convert.ToDecimal(collection["totalPrice"]);

                var order = new OrderDto
                {
                    Id = Guid.NewGuid(),
                    orderDate = DateTime.Now,
                    discount = discount,
                    orderPrice = totalPrice,
                    customer = new CustomerDto
                    {
                        Phone = collection["customer.Phone"]
                    },
                    orderStatus = 1
                };

                var selectedOrderItems = new List<OrderItemDto>();
                // Loop through the form collection for quantities
                foreach (var key in collection.AllKeys)
                {
                    if (key.StartsWith("Quantities["))
                    {
                        // Get the menu item ID from the key
                        var menuItemIdStr = key.Substring("Quantities[".Length, key.Length - "Quantities[".Length - 1);
                        if (Guid.TryParse(menuItemIdStr, out Guid menuItemId))
                        {
                            // Get the quantity value
                            var quantity = Convert.ToInt32(collection[key]);
                            if (quantity > 0)
                            {
                                // Retrieve price from the database or ViewBag.MenuItems based on menuItemId
                                var menuItem = _menuItemService.getMenuItemById(menuItemId); // Assuming you have this method
                                if (menuItem != null)
                                {
                                    selectedOrderItems.Add(new OrderItemDto
                                    {
                                        MenuItem = new MenuItemDto
                                        {
                                            Id = menuItemId,
                                            itemName = menuItem.itemName,
                                            price = menuItem.price
                                        },
                                        Quantity = quantity,
                                        totalPrice = menuItem.price * quantity,
                                        OrderId = order.Id
                                    });
                                }
                            }
                        }
                    }
                }
                order.orderItems = selectedOrderItems;

                _orderService.addOrder(order);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
