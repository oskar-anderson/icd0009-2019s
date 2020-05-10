using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Domain.Base.App.EF.Repositories;
using Domain.Base.EF;

namespace Domain.Base.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        

        public ICartRepository Carts => 
            GetRepository<ICartRepository>(() => new CartRepository(UOWDbContext));
        
        public ICartMealRepository CartMeals => 
            GetRepository<ICartMealRepository>(() => new CartMealRepository(UOWDbContext));

        public ICategoryRepository Categorys => 
            GetRepository<ICategoryRepository>(() => new CategoryRepository(UOWDbContext));

        public IComponentRepository Components => 
            GetRepository<IComponentRepository>(() => new ComponentRepository(UOWDbContext));

        public IComponentPriceRepository ComponentPrices => 
            GetRepository<IComponentPriceRepository>(() => new ComponentPriceRepository(UOWDbContext));

        public IInvoiceRepository Invoices => 
            GetRepository<IInvoiceRepository>(() => new InvoiceRepository(UOWDbContext));

        public IInvoiceLineRepository InvoiceLines => 
            GetRepository<IInvoiceLineRepository>(() => new InvoiceLineRepository(UOWDbContext));

        public IItemRepository Items => 
            GetRepository<IItemRepository>(() => new ItemRepository(UOWDbContext));

        public IMealRepository Meals => 
            GetRepository<IMealRepository>(() => new MealRepository(UOWDbContext));

        public IPaymentMethodRepository PaymentMethods => 
            GetRepository<IPaymentMethodRepository>(() => new PaymentMethodRepository(UOWDbContext));

        public IPersonRepository Persons => 
            GetRepository<IPersonRepository>(() => new PersonRepository(UOWDbContext));
        
        public IPizzaComponentRepository PizzaComponents => 
            GetRepository<IPizzaComponentRepository>(() => new PizzaComponentRepository(UOWDbContext));
        
        public IPizzaRepository Pizzas => 
            GetRepository<IPizzaRepository>(() => new PizzaRepository(UOWDbContext));

        public IPizzaFinalRepository PizzaFinals => 
            GetRepository<IPizzaFinalRepository>(() => new PizzaFinalRepository(UOWDbContext));

        public IPizzaTemplateRepository PizzaTemplates => 
            GetRepository<IPizzaTemplateRepository>(() => new PizzaTemplateRepository(UOWDbContext));

        public IRestaurantRepository Restaurants => 
            GetRepository<IRestaurantRepository>(() => new RestaurantRepository(UOWDbContext));

        public IRestaurantFoodRepository RestaurantFoods => 
            GetRepository<IRestaurantFoodRepository>(() => new RestaurantFoodRepository(UOWDbContext));

        public ISharingRepository Sharings => 
            GetRepository<ISharingRepository>(() => new SharingRepository(UOWDbContext));

        public ISharingItemRepository SharingItems => 
            GetRepository<ISharingItemRepository>(() => new SharingItemRepository(UOWDbContext));

        public ISizeRepository Sizes => 
            GetRepository<ISizeRepository>(() => new SizeRepository(UOWDbContext));

        public IUserLocationRepository UserLocations => 
            GetRepository<IUserLocationRepository>(() => new UserLocationRepository(UOWDbContext));
        
    }
}