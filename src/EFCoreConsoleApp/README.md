# Install Package 
```
dotnet add package <packageName>
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

# appsettings.json file
Another approach is to store the connection string in the appsettings.json file and retrieve it using the configuration API. This allows for easy configuration and flexibility, as the connection string can be changed without modifying the code.

Now, you need to install Microsoft.Extensions.Configuration and Microsoft.Extensions.Configuration.Json NuGet package to your project.

```
dotnet add package Microsoft.Extensions.Configuratio
dotnet add package Microsoft.Extensions.Configuration.Json
```