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

dotnet aspnet-codegenerator controller -name ThingsController              -actions -m  Thing            -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f


After scaffolding do global search & replace:
~~~
</dd class> => </dd>
asp-validation-summary="ModelOnly" => asp-validation-summary="All"
~~~ 


## LangStr and CRUD
LangStr with translations only exist at the Domain level. When entity is mapped into DAL.DTO, they are converted into regular strings (if the property type in DAL.DTO is string).  
Using current UICulture. Conversion happens automatically, thanks to this line in LangStr:  
~~~
// "foo" + new LangStr("bar") => "foobar"
public static implicit operator string(LangStr? l) => l?.ToString() ?? "null";
~~~
  
So this Domain->DAL->BLL conversion is nice and automatic.  
  
We will hit some problems on reverse - when mapping from DAL->Domain.  
Create is ok, thank to this line in LangStr:  
~~~
// langStrProperty = "foobar"
public static implicit operator LangStr(string s) => new LangStr(s);
~~~
This takes the string value from DTO and creates new LangStr. LangStr constructor takes care of setting the translation.  
  
Problems arise in Update. Mapping produces this:  
~~~~
entity: {
    NameId: '123', // this value depends ofcourse on that: id-s for langstr are included as hidden fields in forms. or restored from db. 
    Name: {
        Id: '0'    // so this is new LangStr, and mapper has no idea how to set correct Id here
    }
}
~~~~
So EF will just insert new LangStr and LangStrTranslation into db - not updating the already existing LangStrTranslation.  
  
So we need to do this with every LangStr in entity to be updated:  
~~~
domainEntity.Name = await RepoDbContext.LangStrs.Include(t => t.Translations).FirstAsync(s => s.Id == domainEntity.NameId);
domainEntity.Name.SetTranslation(entity.Name);

domainEntity.Description = await RepoDbContext.LangStrs.Include(t => t.Translations).FirstAsync(s => s.Id == domainEntity.DescriptionId);
domainEntity.Description.SetTranslation(entity.Description);
~~~

So we attach back the original LangStr from DB, including LangStrTranslations.  
And then we update the translation - either existing one is updated or new one is added.  
