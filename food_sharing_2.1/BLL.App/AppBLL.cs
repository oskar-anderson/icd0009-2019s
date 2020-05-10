using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.EF;


namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ICartMealService CartMeals  => 
            GetService<ICartMealService>(() => new CartMealService(UnitOfWork));
        public ICartService Carts  => 
            GetService<ICartService>(() => new CartService(UnitOfWork));
        public ICategoryService Categorys  => 
            GetService<ICategoryService>(() => new CategoryService(UnitOfWork));
        public IComponentPriceService ComponentPrices  => 
            GetService<IComponentPriceService>(() => new ComponentPriceService(UnitOfWork));
        public IComponentService Components  => 
            GetService<IComponentService>(() => new ComponentService(UnitOfWork));
        public IInvoiceLineService InvoiceLines  => 
            GetService<IInvoiceLineService>(() => new InvoiceLineService(UnitOfWork));
        public IInvoiceService Invoices  => 
            GetService<IInvoiceService>(() => new InvoiceService(UnitOfWork));
        public IItemService Items  => 
            GetService<IItemService>(() => new ItemService(UnitOfWork));
        public IMealService Meals  => 
            GetService<IMealService>(() => new MealService(UnitOfWork));
        public IPaymentMethodService PaymentMethods  => 
            GetService<IPaymentMethodService>(() => new PaymentMethodService(UnitOfWork));
        public IPersonService Persons  => 
            GetService<IPersonService>(() => new PersonService(UnitOfWork));
        public IPizzaComponentService PizzaComponents  => 
            GetService<IPizzaComponentService>(() => new PizzaComponentService(UnitOfWork));
        public IPizzaFinalService PizzaFinals  => 
            GetService<IPizzaFinalService>(() => new PizzaFinalService(UnitOfWork));
        public IPizzaService Pizzas  => 
            GetService<IPizzaService>(() => new PizzaService(UnitOfWork));
        public IPizzaTemplateService PizzaTemplates  => 
            GetService<IPizzaTemplateService>(() => new PizzaTemplateService(UnitOfWork));
        public IRestaurantFoodService RestaurantFoods  => 
            GetService<IRestaurantFoodService>(() => new RestaurantFoodService(UnitOfWork));
        public IRestaurantService Restaurants  => 
            GetService<IRestaurantService>(() => new RestaurantService(UnitOfWork));
        public ISharingItemService SharingItems  => 
            GetService<ISharingItemService>(() => new SharingItemService(UnitOfWork));
        public ISharingService Sharings  => 
            GetService<ISharingService>(() => new SharingService(UnitOfWork));
        public ISizeService Sizes  => 
            GetService<ISizeService>(() => new SizeService(UnitOfWork));
        public IUserLocationService UserLocations  => 
            GetService<IUserLocationService>(() => new UserLocationService(UnitOfWork));
    }
}