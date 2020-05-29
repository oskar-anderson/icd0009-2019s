using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain.Base.App.EF;


namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        public ICartMealService CartMeals  => 
            GetService<ICartMealService>(() => new CartMealService(UOW));
        public ICartService Carts  => 
            GetService<ICartService>(() => new CartService(UOW));
        public ICategoryService Categorys  => 
            GetService<ICategoryService>(() => new CategoryService(UOW));
        public IComponentService Components  => 
            GetService<IComponentService>(() => new ComponentService(UOW));
        public IComponentPizzaUserService ComponentPizzaUsers  => 
            GetService<IComponentPizzaUserService>(() => new ComponentPizzaUserService(UOW));
        public IComponentPizzaTemplateService ComponentPizzaTemplates  => 
            GetService<IComponentPizzaTemplateService>(() => new ComponentPizzaTemplateService(UOW));
        public IItemService Items  => 
            GetService<IItemService>(() => new ItemService(UOW));
        public IPizzaUserService PizzaUsers  => 
            GetService<IPizzaUserService>(() => new PizzaUserService(UOW));
        public IPizzaService Pizzas  => 
            GetService<IPizzaService>(() => new PizzaService(UOW));
        public IPizzaTemplateService PizzaTemplates  => 
            GetService<IPizzaTemplateService>(() => new PizzaTemplateService(UOW));
        public IRestaurantFoodService RestaurantFoods  => 
            GetService<IRestaurantFoodService>(() => new RestaurantFoodService(UOW));
        public IRestaurantService Restaurants  => 
            GetService<IRestaurantService>(() => new RestaurantService(UOW));
        public ISharingItemService SharingItems  => 
            GetService<ISharingItemService>(() => new SharingItemService(UOW));
        public ISharingService Sharings  => 
            GetService<ISharingService>(() => new SharingService(UOW));
        public IUserLocationService UserLocations  => 
            GetService<IUserLocationService>(() => new UserLocationService(UOW));
    }
}