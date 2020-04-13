using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        ICartRepository Carts { get; }
        ICartMealRepository CartMeals { get; }
        ICategoryRepository Categorys { get; }
        IComponentRepository Components { get; }
        IComponentPriceRepository ComponentPrices { get; }
        IInvoiceRepository Invoices { get; }
        IInvoiceLineRepository InvoiceLines { get; }
        IItemRepository Items { get; }
        IMealRepository Meals { get; }
        IPaymentMethodRepository PaymentMethods { get; }
        IPersonRepository Persons { get; }
        IPizzaComponentRepository PizzaComponents { get; }
        IPizzaRepository Pizzas { get; }
        IPizzaFinalRepository PizzaFinals { get; }
        IPizzaTemplateRepository PizzaTemplates { get; }
        IRestaurantRepository Restaurants { get; }
        IRestaurantFoodRepository RestaurantFoods { get; }
        ISharingRepository Sharings { get; }
        ISharingItemRepository SharingItems { get; }
        ISizeRepository Sizes { get; }
        IUserLocationRepository UserLocations { get; }
    }
}