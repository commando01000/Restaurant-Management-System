using Service_Layer.Services;
using Service_Layer.Services.Menu_Item;
using Service_Layer.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Management_System.Controllers.Orders
{
    public class OrderItemsController : Controller
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IOrderItemService _orderItemService;
        private readonly IOrderService _orderService;

        public OrderItemsController(IMenuItemService menuItemService, IOrderItemService orderItemService, IOrderService orderService)
        {
            _menuItemService = menuItemService;
            _orderItemService = orderItemService;
            _orderService = orderService;
        }
        // GET: OrderItems
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderItems/Create
        public ActionResult Create()
        {
            // prepare the customer

            // prepare the order items

            // prepare the order for reservation


            return View();
        }

        // POST: OrderItems/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderItems/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderItems/Edit/5
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

        // GET: OrderItems/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderItems/Delete/5
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
