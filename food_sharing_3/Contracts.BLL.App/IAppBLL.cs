using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        ICartMealService CartMeals { get; }
        ICartService Carts { get; }
        ICategoryService Categorys { get; }
        IComponentPriceService ComponentPrices { get; }
        IComponentService Components { get; }
        IInvoiceLineService InvoiceLines { get; }
        IInvoiceService Invoices { get; }
        IItemService Items { get; }
        IMealService Meals { get; }
        IPaymentMethodService PaymentMethods { get; }
        IPersonService Persons { get; }
        IPizzaComponentService PizzaComponents { get; }
        IPizzaFinalService PizzaFinals { get; }
        IPizzaService Pizzas { get; }
        IPizzaTemplateService PizzaTemplates { get; }
        IRestaurantFoodService RestaurantFoods { get; }
        IRestaurantService Restaurants { get; }
        ISharingItemService SharingItems { get; }
        ISharingService Sharings { get; }
        ISizeService Sizes { get; }
        IUserLocationService UserLocations { get; }
    }
}