using Repository_Layer.CustomerRepository;
using Repository_Layer.Repositories.Menu_Item;
using Repository_Layer.Repositories.Order;
using Repository_Layer.Repositories.OrderItem;
using Service_Layer.Services;
using Service_Layer.Services.Customer;
using Service_Layer.Services.Menu_Item;
using Service_Layer.Services.OrderItems;
using Service_Layer.Services.Orders;
using System;

using Unity;

namespace Restaurant_Management_System
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IMenuItemService, MenuItemService>();
            container.RegisterType<IMenuItemRepository, MenuItemRepository>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IOrderItemRepository, OrderItemRepository>();
            container.RegisterType<IOrderItemService, OrderItemService>();
        }
    }
}