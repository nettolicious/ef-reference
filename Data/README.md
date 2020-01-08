To regenerate model:

Open PowerShell and navigate to project directory and run the following script:

.\ScaffoldDbContext.ps1

The script runs a command similar to the following, that will generate JfccDbContext and the entity classes:

dotnet ef dbcontext scaffold "Data Source=MiSqlJfcc2;Initial Catalog=JFCC;user id=devadmin;password=special" 
  Microsoft.EntityFrameworkCore.SqlServer -d -f -o Entities -c JfccDbContext --context-dir "./" -t Bin -t BinAlias -t BinAliasFamily 
  -t BinHierarchy -t BinType -t BinItemRef -t BinItem

** IMPORTANT
The classes in the Entities folder get written over, but no files get deleted
If you remove or rename a table, you need to manually delete any entities that are no longer needed
The DbContext 'JfccDbContext.cs' gets written over

## Classes returned from stored procedures should be added to SpResults folder 
* If the return is not an Entity, add the return type as a DbQuery in JfccDbContext.Custom.cs and add an Exec{MySpName} method 
that returns IQueryable<MyCustomType>
* If the return type is an Entity, simply add an Exec{MySpName} method that returns IQueryable<MyEntityType>


Note the use of handlebars templates to customize the generation of the DbContext and entities. You need to copy the CodeTemplates
for C# from the NuGet package.

We are using design time services to hook up handlebars templates and pluralization in Infrastructure/ScaffoldingDesignTimeServices

References:
https://github.com/TrackableEntities/EntityFrameworkCore.Scaffolding.Handlebars
https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-2.0#pluralization-hook-for-dbcontext-scaffolding
https://github.com/srkirkland/Inflector/

Notes:

Entity-less many-to-many relationships are not supported.
https://docs.microsoft.com/en-us/ef/core/modeling/relationships#many-to-many

Issues:
The way I'm setting the namespace explicitly in the handlebars templates is not great.