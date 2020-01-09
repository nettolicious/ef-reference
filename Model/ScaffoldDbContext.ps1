[string]$dbContextName = "NettoliciousDbContext"
[string]$entitiesFolder = "Entities"
[string]$connString = "Server=.;Database=AdventureWorks;Integrated Security=true;"
[string[]]$tables = "HumanResources.Department",
  "HumanResources.Employee",
  "HumanResources.EmployeeDepartmentHistory",
  "HumanResources.EmployeePayHistory",
  "HumanResources.JobCandidate",
  "HumanResources.Shift"

[string]$dbContext = ""
[string]$modelFolder = ""
[string]$scaffoldCommand = ""
[string]$table = ""

$dbContext = Join-Path -Path $PSScriptRoot -ChildPath "$($dbContextName).cs"
$modelFolder = Join-Path -Path $PSScriptRoot -ChildPath $entitiesFolder

Write-Host "Begin scaffolding..."
$scaffoldCommand = "dotnet ef dbcontext scaffold ""$($connString)"" Microsoft.EntityFrameworkCore.SqlServer -d -f -o ""$($entitiesFolder)"" -c ""$($dbContextName)"" --context-dir ""./"" --verbose --json"
foreach ($table in $tables) {
  $scaffoldCommand += " -t $($table)"
}
Invoke-Expression -Command $scaffoldCommand | Write-Host

Write-Host "Complete"