using System;
using Contracts.BLL.App.Services;
using ee.itcollege.kaande.pitsariina.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        ICartMealService CartMeals { get; }
        ICartService Carts { get; }
        ICategoryService Categorys { get; }
        IComponentService Components { get; }
        IComponentPizzaTemplateService ComponentPizzaTemplates { get; }
        IItemService Items { get; }
        IPizzaService Pizzas { get; }
        IPizzaTemplateService PizzaTemplates { get; }
        IRestaurantFoodService RestaurantFoods { get; }
        IRestaurantService Restaurants { get; }
        ISharingItemService SharingItems { get; }
        ISharingService Sharings { get; }
        IUserLocationService UserLocations { get; }
    }
}