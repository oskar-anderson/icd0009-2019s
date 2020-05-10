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
        public IComponentPriceService ComponentPrices  => 
            GetService<IComponentPriceService>(() => new ComponentPriceService(UOW));
        public IComponentService Components  => 
            GetService<IComponentService>(() => new ComponentService(UOW));
        public IInvoiceLineService InvoiceLines  => 
            GetService<IInvoiceLineService>(() => new InvoiceLineService(UOW));
        public IInvoiceService Invoices  => 
            GetService<IInvoiceService>(() => new InvoiceService(UOW));
        public IItemService Items  => 
            GetService<IItemService>(() => new ItemService(UOW));
        public IMealService Meals  => 
            GetService<IMealService>(() => new MealService(UOW));
        public IPaymentMethodService PaymentMethods  => 
            GetService<IPaymentMethodService>(() => new PaymentMethodService(UOW));
        public IPersonService Persons  => 
            GetService<IPersonService>(() => new PersonService(UOW));
        public IPizzaComponentService PizzaComponents  => 
            GetService<IPizzaComponentService>(() => new PizzaComponentService(UOW));
        public IPizzaFinalService PizzaFinals  => 
            GetService<IPizzaFinalService>(() => new PizzaFinalService(UOW));
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
        public ISizeService Sizes  => 
            GetService<ISizeService>(() => new SizeService(UOW));
        public IUserLocationService UserLocations  => 
            GetService<IUserLocationService>(() => new UserLocationService(UOW));
    }
}