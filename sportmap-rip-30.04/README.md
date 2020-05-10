# SportMap ASP.NET front and backend

Project for: https://git.akaver.com/iti0213-2019s/course-materials/-/blob/master/homeworks/HW2.md    
Android location info: https://developer.android.com/reference/android/location/Location  
ERD schema: https://www.lucidchart.com/invitations/accept/9b0354f0-82c5-4cb3-a955-118fb31a43a2  
RESTful Api endpoints are testable and documented in url /swagger/  

## Database
~~~
"SqlServerConnection": "Server=localhost;User Id=SA;Password=xxxxx.yyyyyy;Database=sportmap;MultipleActiveResultSets=true"
~~~

## Install or update tooling
~~~
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool update -g dotnet-aspnet-codegenerator
~~~

## Generate database migration
Run from solution folder.  
~~~
dotnet ef database drop --project DAL.App.EF --startup-project WebApp
dotnet ef migrations --project DAL.App.EF --startup-project WebApp add InitialDbCreation 
dotnet ef migrations --project DAL.App.EF --startup-project WebApp remove
dotnet ef database update --project DAL.App.EF --startup-project WebApp
~~~

## Enable nullable reference types
Add ***Directory.Build.props*** to solution folder for common project properties
~~~xml
<Project>
    <PropertyGroup>
        <LangVersion>latest</LangVersion>
        <Nullable>enable</Nullable>
    </PropertyGroup>
</Project>
~~~


## Generate identity UI
Install Microsoft.VisualStudio.Web.CodeGeneration.Design to WebApp.  
Run from inside the WebApp directory.  
~~~
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f  
~~~


## Generate WebApi controllers
Run from inside the WebApp directory.  
~~~
dotnet aspnet-codegenerator controller -name GpsLocationsController     -m GpsLocation     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name GpsLocationTypesController -m GpsLocationType -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name GpsSessionsController      -m GpsSession      -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

dotnet aspnet-codegenerator controller -name GpsSessionTypesController  -m Domain.App.GpsSessionType  -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name TracksController           -m Domain.App.Track           -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name TrackPointsController      -m Domain.App.TrackPoint      -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

~~~

MVC Web Controllers (disable global warnings as errors - only one controller will be generated otherwise, then compile starts to fail)
~~~
dotnet aspnet-codegenerator controller -name GpsLocationsController        -actions -m  Domain.App.GpsLocation        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name GpsLocationTypesController    -actions -m  Domain.App.GpsLocationType    -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name GpsSessionsController         -actions -m  Domain.App.GpsSession         -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name GpsSessionTypesController     -actions -m  Domain.App.GpsSessionType     -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LangStrsController            -actions -m  Domain.App.LangStr            -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LangStrTranslationsController -actions -m  Domain.App.LangStrTranslation -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TracksController              -actions -m  Domain.App.Track              -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TrackPointsController         -actions -m  Domain.App.TrackPoint         -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

After scaffolding do global search & replace:
~~~
</dd class> => </dd>
asp-validation-summary="ModelOnly" => asp-validation-summary="All"
~~~ 
