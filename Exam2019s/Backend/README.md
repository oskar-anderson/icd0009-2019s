Exam

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
