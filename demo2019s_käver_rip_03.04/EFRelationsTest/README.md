
~~~
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp
~~~



run in WebApp folder
~~~
dotnet aspnet-codegenerator controller -name PrimaryOnesController   -actions -m PrimaryOne    -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ChildOnesController     -actions -m ChildOne      -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name PrimaryTwosController   -actions -m PrimaryTwo    -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ChildTwosController     -actions -m ChildTwo      -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name PrimaryThreesController -actions -m PrimaryThree    -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ChildThreesController   -actions -m ChildThree      -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~
