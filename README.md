# Reference .NET Core Entity Framework 3.1 with Scaffolding

## Features
* Scaffold DbContext and entities from command line with powershell script
* Customize pluralization using Inflector
* Customize DbContext and entity generation with handlebars templates
* Pattern for including stored procedure calls in DbContext
* Sample unit tests with in-memory database
* Sample stored procedure mocking

## Running E2E Tests & Regenerating the Model

Get the adventure works database from Microsoft. Update the connection strings as necessary.

I generated the database using this script 
https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks-oltp-install-script.zip

Open PowerShell and navigate to project directory and run the following script:

    .\ScaffoldDbContext.ps1

The script runs a command similar to the following, that will generate NettoliciousDbContext and the entity classes:

    dotnet ef dbcontext scaffold "Server=.;Database=AdventureWorks;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer -d -f -o Entities -c NettoliciousDbContext --context-dir "./" -t HumanResources.Department -t HumanResources.Employee -t HumanResources.EmployeeDepartmentHistory

Note that the classes in the Entities folder get written over, but no files get deleted.
If you remove or rename a table, you need to manually delete any entities that are no longer needed.
The DbContext 'NettoliciousDbContext.cs' gets written over.

Note the use of handlebars templates to customize the generation of the DbContext and entities. You need to copy the CodeTemplates
for C# from the NuGet package.

Note the use of design time services to hook up handlebars templates and pluralization in Infrastructure/ScaffoldingDesignTimeServices.

## Stored Procedures 
* If the return type is not a generated Entity
    * Add the entity to the SpResults folder
    * Add the return type as a DbSet in NettoliciousDbContext.SpResults.cs and add an Exec{MySpName} method 
that returns IQueryable<MyCustomType>
* If the return type is an Entity, simply add an Exec{MySpName} method that returns IQueryable<MyEntityType>

## References
* https://github.com/TrackableEntities/EntityFrameworkCore.Scaffolding.Handlebars
* https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-2.0#pluralization-hook-for-dbcontext-scaffolding
* https://github.com/srkirkland/Inflector/
* https://github.com/juanpabloventoso/MockProject/tree/netcore3

## Issues

Entity-less many-to-many relationships are not supported.
https://docs.microsoft.com/en-us/ef/core/modeling/relationships#many-to-many

The way I'm setting the namespace explicitly in the handlebars templates is not great.
