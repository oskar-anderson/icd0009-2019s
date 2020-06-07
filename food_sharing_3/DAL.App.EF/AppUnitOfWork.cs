using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using ee.itcollege.kaande.pitsariina.DAL.Base.EF;

namespace DAL.App.EF
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
        
        public IItemRepository Items => 
            GetRepository<IItemRepository>(() => new ItemRepository(UOWDbContext));
        
        public IComponentPizzaTemplateRepository ComponentPizzaTemplates => 
            GetRepository<IComponentPizzaTemplateRepository>(() => new ComponentPizzaTemplateRepository(UOWDbContext));
        
        
        public IPizzaRepository Pizzas => 
            GetRepository<IPizzaRepository>(() => new PizzaRepository(UOWDbContext));


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
        
        public IUserLocationRepository UserLocations => 
            GetRepository<IUserLocationRepository>(() => new UserLocationRepository(UOWDbContext));
        
    }
}