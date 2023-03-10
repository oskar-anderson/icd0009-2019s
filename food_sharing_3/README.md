Databases to use:  

~~~
"MySqlConnection": "server=alpha.akaver.com;database=student2018_kaande_food_sharing;user=student2018;password=student2018"
http://alpha.akaver.com/phpMyAdmin/index.php  

"MSSQLConnection": "Server=alpha.akaver.com,1533;User Id=SA;Password=Admin.TalTech.1;Database=<your_uni-id_somerandomname>;MultipleActiveResultSets=true"  
For managing data:  
https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio  
~~~

~~~
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator
~~~

Run in project folder:
~~~
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp
~~~

Turn off migrations cascade delete:
~~~
onDelete: ReferentialAction.NoAction
~~~

Run in WebApp folder

Generate Controllers
~~~
dotnet aspnet-codegenerator controller -name CartController             -actions -m Cart -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CartMealController         -actions -m CartMeal -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoryController         -actions -m Category -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ComponentController        -actions -m Component -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ComponentPriceController   -actions -m ComponentPrice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InvoiceController          -actions -m Invoice -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InvoiceLineController      -actions -m InvoiceLine -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemController             -actions -m Item -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MealController             -actions -m Meal -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PaymentMethodController    -actions -m PaymentMethod -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PersonController           -actions -m Person -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PizzaController            -actions -m Pizza -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PizzaComponentController   -actions -m PizzaComponent -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PizzaFinalController       -actions -m PizzaFinal -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PizzaTemplateController    -actions -m PizzaTemplate -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantController       -actions -m Restaurant -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RestaurantFoodController   -actions -m RestaurantFood -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SharingController          -actions -m Sharing -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SharingItemController      -actions -m SharingItem -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SizeController             -actions -m Size -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserLocationController     -actions -m UserLocation -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~
Run in WebApp folder

Generate Identity UI
~~~
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext  -f  
~~~
Run in WebApp folder

API Controllers
~~~
dotnet aspnet-codegenerator controller -name CartController             -actions -m Cart -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CartMealController         -actions -m CartMeal -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CategoryController         -actions -m Category -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ComponentController        -actions -m Component -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ComponentPriceController   -actions -m ComponentPrice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name InvoiceController          -actions -m Invoice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name InvoiceLineController      -actions -m InvoiceLine -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemController             -actions -m Item -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name MealController             -actions -m Meal -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PaymentMethodController    -actions -m PaymentMethod -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PersonController           -actions -m Person -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PizzaController            -actions -m Pizza -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PizzaComponentController   -actions -m PizzaComponent -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PizzaFinalController       -actions -m PizzaFinal -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PizzaTemplateController    -actions -m PizzaTemplate -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RestaurantController       -actions -m Restaurant -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RestaurantFoodController   -actions -m RestaurantFood -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name SharingController          -actions -m Sharing -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemController             -actions -m Item -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name SharingItemController      -actions -m SharingItem -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name SizeController             -actions -m Size -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name UserLocationController     -actions -m UserLocation -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
~~~

Replace in path views
~~~
asp-validation-summary="ModelOnly"
~~~
with
~~~
asp-validation-summary="All"
~~~


## Solution structure by the projects
 
We want to write our solutions with as much shared code as possible - 
to avoid writing the same code again in the next solution. 
So we need to separate our code into 2 major parts - current app specific and common shared base.

What can we share? PK definitions, definitions for base repository - those would repeat 
from one solution to the next. And we can share both - interfaces (contracts) and their implementations.

 
### So:  
#### Shared, common codebase  
Contracts.DAL.Base - specs for domain metadata and PK in entities. Specs for common base repository.  
DAL.Base - abstract implementations of interfaces for domain.  
DAL.Base.EF - implementation of common base repository done in EF.  

#### App specific codebase for our solution  
Domain - Domain objects - what is our business about  
Contracts.DAL.App - specs for repositories  
DAL.App.EF - implementation of repositories  


~~~
WebApp/Views/Shared/Layout

<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
</li>

<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Cart" asp-action="Index">Cart</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="CartMeal" asp-action="Index">CartMeal</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Category" asp-action="Index">Category</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Component" asp-action="Index">Component</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="ComponentPrice" asp-action="Index">ComponentPrice</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Invoice" asp-action="Index">Invoice</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="InvoiceLine" asp-action="Index">InvoiceLine</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Item" asp-action="Index">Item</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Meal" asp-action="Index">Meal</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="PaymentMethod" asp-action="Index">PaymentMethod</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Person" asp-action="Index">Person</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Pizza" asp-action="Index">Pizza</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="PizzaComponent" asp-action="Index">PizzaComponent</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="PizzaFinal" asp-action="Index">PizzaFinal</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="PizzaTemplate" asp-action="Index">PizzaTemplate</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Restaurant" asp-action="Index">Restaurant</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="RestaurantFood" asp-action="Index">RestaurantFood</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Sharing" asp-action="Index">Sharing</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="SharingItem" asp-action="Index">SharingItem</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Size" asp-action="Index">Size</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="UserLocation" asp-action="Index">UserLocation</a>
</li>

~~~
                        

    