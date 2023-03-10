Databases to use:  

"MySqlConnection": "server=alpha.akaver.com;database=student2018_akaver_demo2019s;user=student2018;password=student2018"  
http://alpha.akaver.com/phpMyAdmin/index.php  

"MSSQLConnection": "Server=alpha.akaver.com,1533;User Id=SA;Password=Admin.TalTech.1;Database=<your_uni-id_somerandomname>;MultipleActiveResultSets=true"  
For managing data:  
https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio  

~~~
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp
~~~



run in WebApp folder
~~~
dotnet aspnet-codegenerator controller -name AuthorsController          -actions -m Author          -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoriesController       -actions -m Category        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AuthorPicturesController   -actions -m AuthorPicture   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PostsController            -actions -m Post            -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PostCategoriesController   -actions -m PostCategory    -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

Generate Identity UI
~~~
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext  -f  
~~~


~~~
dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions  -f
~~~
