using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        ICartRepository Carts { get; }
        ICartMealRepository CartMeals { get; }
        ICategoryRepository Categorys { get; }
        IComponentRepository Components { get; }
        IComponentPizzaUserRepository ComponentPizzaUsers { get; }
        IComponentPizzaTemplateRepository ComponentPizzaTemplates { get; }
        IItemRepository Items { get; }
        IPizzaRepository Pizzas { get; }
        IPizzaUserRepository PizzaUsers { get; }
        IPizzaTemplateRepository PizzaTemplates { get; }
        IRestaurantRepository Restaurants { get; }
        IRestaurantFoodRepository RestaurantFoods { get; }
        ISharingRepository Sharings { get; }
        ISharingItemRepository SharingItems { get; }
        IUserLocationRepository UserLocations { get; }
    }
}