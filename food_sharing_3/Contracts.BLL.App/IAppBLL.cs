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
        IComponentService Components { get; }
        IComponentPizzaUserService ComponentPizzaUsers { get; }
        IComponentPizzaTemplateService ComponentPizzaTemplates { get; }
        IItemService Items { get; }
        IMealService Meals { get; }
        IPizzaService Pizzas { get; }
        IPizzaUserService PizzaUsers { get; }
        IPizzaTemplateService PizzaTemplates { get; }
        IRestaurantFoodService RestaurantFoods { get; }
        IRestaurantService Restaurants { get; }
        ISharingItemService SharingItems { get; }
        ISharingService Sharings { get; }
        IUserLocationService UserLocations { get; }
    }
}