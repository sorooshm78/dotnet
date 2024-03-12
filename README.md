# dotnet
learn .NET 

* [C#](#c_sharp)
* [ASP Core](#asp_core)
* [EF Core](#ef_core)
* [Architecture](#architecture)

<a id="c_sharp"></a>
# C#
## Lambda operator
In lambda expressions, the lambda operator => separates the input parameters on the left side from the lambda body on the right side.
The following example uses the LINQ feature with method syntax to demonstrate the usage of lambda expressions:

```
string[] words = { "bot", "apple", "apricot" };
int minimalLength = words
  .Where(w => w.StartsWith("a"))
  .Min(w => w.Length);
Console.WriteLine(minimalLength);   // output: 5

int[] numbers = { 4, 7, 10 };
int product = numbers.Aggregate(1, (interim, next) => interim * next);
Console.WriteLine(product);   // output: 280
```

Input parameters of a lambda expression are strongly typed at compile time. When the compiler can infer the types of input parameters, like in the preceding example, you may omit type declarations. If you need to specify the type of input parameters, you must do that for each parameter, as the following example shows:

```
int[] numbers = { 4, 7, 10 };
int product = numbers.Aggregate(1, (int interim, int next) => interim * next);
Console.WriteLine(product);   // output: 280
```

The following example shows how to define a lambda expression without input parameters:

```
Func<string> greet = () => "Hello, World!";
Console.WriteLine(greet());
```

## using 
The using statement ensures the correct use of an IDisposable instance:
```
var numbers = new List<int>();
using (StreamReader reader = File.OpenText("numbers.txt"))
{
    string line;
    while ((line = reader.ReadLine()) is not null)
    {
        if (int.TryParse(line, out int number))
        {
            numbers.Add(number);
        }
    }
}
```
When the control leaves the block of the using statement, an acquired IDisposable instance is disposed. In particular, the using statement ensures that a disposable instance is disposed even if an exception occurs within the block of the using statement. In the preceding example, an opened file is closed after all lines are processed.

The IDisposable interface plays an important role in memory management in .NET C#. It is a part of the System namespace, and it defines the Dispose() method, which can be used to free up unmanaged resources like file handles and database connections.

What is the IDisposable Interface?
The IDisposable interface is a .NET interface that defines a single method - the Dispose () method. This method is used to free up unmanaged resources like file handles, database connections and other resources that are not managed by the garbage collector.

When to Use the IDisposable Interface
The IDisposable interface should be used when you need to free up unmanaged resources like file handles and database connections. It is also useful when you have an object that needs to be disposed of after use, such as an object that holds a reference to a database connection or an object that has been created from a large file.

Using the IDisposable Interface
To use the IDisposable interface you need to implement it in your class. This can be done by simply adding the IDisposable interface to your class definition. For example:

```
public class MyClass : IDisposable 
{ 
    // ... 
}
```

Once you have implemented the interface, you must implement the Dispose() method. The Dispose() method should be used to free up any unmanaged resources the class holds. For example:
```
public void Dispose() 
{ 
    // Free up any unmanaged resources here. 
} 
```

Finally, when you are done with your object, you should call the Dispose() method to free up any unmanaged resources that are being held by it. For example:
```
MyClass myObject = new MyClass(); 
// Use myObject 
myObject.Dispose(); 
```

.NET remote developers can also use the using block instead of manually calling the Dispose() method. The using block ensures that the object is disposed of correctly even if an exception occurs.

```
using (MyClass myObject = new MyClass()) 
{ 
    // Use myObject 
} 
// myObject is automatically disposed of here.
```

## LINQ
LINQ in C# is used to work with data access from sources such as objects, data sets, SQL Server, and XML. LINQ stands for Language Integrated Query. LINQ is a data querying API with SQL like query syntaxes. LINQ provides functions to query cached data from all kinds of data sources. The data source could be a collection of objects, database or XML files. We can easily retrieve data from any object that implements the IEnumerable<T> interface.

A query is an expression that retrieves data from a data source. Different data sources have different native query languages, for example SQL for relational databases and XQuery for XML. Developers must learn a new query language for each type of data source or data format that they must support. LINQ simplifies this situation by offering a consistent C# language model for kinds of data sources and formats. In a LINQ query, you always work with C# objects. You use the same basic coding patterns to query and transform data in XML documents, SQL databases, .NET collections, and any other format when a LINQ provider is available

Three Parts of a Query Operation
All LINQ query operations consist of three distinct actions:

1. Obtain the data source.
2. Create the query.
3. Execute the query.

The following example shows how the three parts of a query operation are expressed in source code. The example uses an integer array as a data source for convenience; however, the same concepts apply to other data sources also. This example is referred to throughout the rest of this article.

```
// The Three Parts of a LINQ Query:
// 1. Data source.
int[] numbers = [ 0, 1, 2, 3, 4, 5, 6 ];

// 2. Query creation.
// numQuery is an IEnumerable<int>
var numQuery =
    from num in numbers
    where (num % 2) == 0
    select num;

// 3. Query execution.
foreach (int num in numQuery)
{
    Console.Write("{0,1} ", num);
}
```
The following illustration shows the complete query operation. In LINQ, the execution of the query is distinct from the query itself. In other words, you don't retrieve any data by creating a query variable.

![linq](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/media/introduction-to-linq-queries/linq-query-complete-operation.png)

### The Data Source
The data source in the preceding example is an array, which supports the generic IEnumerable<T> interface. This fact means it can be queried with LINQ. A query is executed in a foreach statement, and foreach requires IEnumerable or IEnumerable<T>. Types that support IEnumerable<T> or a derived interface such as the generic IQueryable<T> are called queryable types.

A queryable type requires no modification or special treatment to serve as a LINQ data source. If the source data isn't already in memory as a queryable type, the LINQ provider must represent it as such. For example, LINQ to XML loads an XML document into a queryable XElement type:

```
// Create a data source from an XML document.
// using System.Xml.Linq;
XElement contacts = XElement.Load(@"c:\myContactList.xml");
```

With EntityFramework, you create an object-relational mapping between C# classes and your database schema. You write your queries against the objects, and at run-time EntityFramework handles the communication with the database. In the following example, Customers represents a specific table in the database, and the type of the query result, IQueryable<T>, derives from IEnumerable<T>.

```
Northwnd db = new Northwnd(@"c:\northwnd.mdf");

// Query for customers in London.
IQueryable<Customer> custQuery =
    from cust in db.Customers
    where cust.City == "London"
    select cust;
```

For more information about how to create specific types of data sources, see the documentation for the various LINQ providers. However, the basic rule is simple: a LINQ data source is any object that supports the generic IEnumerable<T> interface, or an interface that inherits from it, typically IQueryable<T>.

### The Query
The query specifies what information to retrieve from the data source or sources. Optionally, a query also specifies how that information should be sorted, grouped, and shaped before being returned. A query is stored in a query variable and initialized with a query expression. You use C# query syntax to write queries.

The query in the previous example returns all the even numbers from the integer array. The query expression contains three clauses: from, where and select. (If you're familiar with SQL, you noticed that the ordering of the clauses is reversed from the order in SQL.) The from clause specifies the data source, the where clause applies the filter, and the select clause specifies the type of the returned elements. All the query clauses are discussed in detail in this section. For now, the important point is that in LINQ, the query variable itself takes no action and returns no data. It just stores the information that is required to produce the results when the query is executed at some later point. For more information about how queries are constructed, see Standard Query Operators Overview (C#).

### Query Execution
Deferred Execution
The query variable itself only stores the query commands. The actual execution of the query is deferred until you iterate over the query variable in a foreach statement. This concept is referred to as deferred execution and is demonstrated in the following example:

```
foreach (int num in numQuery)
{
    Console.Write("{0,1} ", num);
}
```

The foreach statement is also where the query results are retrieved. For example, in the previous query, the iteration variable num holds each value (one at a time) in the returned sequence.

Because the query variable itself never holds the query results, you can execute it repeatedly to retrieve updated data. For example, you might have a database that is being updated continually by a separate application. In your application, you could create one query that retrieves the latest data, and you could execute it at intervals to retrieve updated results.

### Forcing Immediate Execution
Queries that perform aggregation functions over a range of source elements must first iterate over those elements. Examples of such queries are Count, Max, Average, and First. These methods execute without an explicit foreach statement because the query itself must use foreach in order to return a result. These queries return a single value, not an IEnumerable collection. The following query returns a count of the even numbers in the source array:

```
var evenNumQuery =
    from num in numbers
    where (num % 2) == 0
    select num;

int evenNumCount = evenNumQuery.Count();
```

To force immediate execution of any query and cache its results, you can call the ToList or ToArray methods.

```
List<int> numQuery2 =
    (from num in numbers
        where (num % 2) == 0
        select num).ToList();

// or like this:
// numQuery3 is still an int[]

var numQuery3 =
    (from num in numbers
        where (num % 2) == 0
        select num).ToArray();
```

You can also force execution by putting the foreach loop immediately after the query expression. However, by calling ToList or ToArray you also cache all the data in a single collection object.

### We can use LINQ queries in two ways
1. Query Syntax:
The LINQ query language syntax starts with from keyword and finishes with the Select or GroupBy keyword. After from keyword, you can utilize various sorts of Standard Query operations like grouping, filtering, and so on, as indicated by your need. In LINQ, 50 unique kinds of Standard Question Administrators are accessible

2. Method Syntax
In LINQ, Method Syntax is utilized to call the expansion method for the Enumerable or Queryable static classes. It is also called Method Extension Syntax or Fluent. Notwithstanding, the compiler generally changes over the query syntax in method syntax structure at compile time. It can summon the standard Query operator like Where, Join, Max, Min, Avg, GroupBy Select, and so forth. You are permitted to call them straightforwardly without utilizing Query syntax.

```
int[] numbers = [ 5, 10, 8, 3, 6, 12 ];

//Query syntax:
IEnumerable<int> numQuery1 =
    from num in numbers
    where num % 2 == 0
    orderby num
    select num;

//Method syntax:
IEnumerable<int> numQuery2 = numbers.Where(num => num % 2 == 0).OrderBy(n => n);

foreach (int i in numQuery1)
{
    Console.Write(i + " ");
}
Console.WriteLine(System.Environment.NewLine);
foreach (int i in numQuery2)
{
    Console.Write(i + " ");
}
```


<a id="asp_core"></a>
# ASP Core

## appsettings.json
What is the ASP.NET Core AppSettings.json File?
The appsettings.json file in an ASP.NET Core application is a JSON formatted file that stores configuration data. In this file, you can keep settings like connection strings, application settings, logging configuration, and anything else you want to change without recompiling your application. The settings in this file can be read at runtime and overridden by environment-specific files like appsettings.Development.json or appsettings.Production.json

The appsettings.json file is the application configuration file used to store configuration settings such as database connection strings, any application scope global variables, etc. If you open the ASP.NET Core appsettings.json file, you see the following code created by Visual Studio by default.

The appsettings.json file typically resides in the root directory of your ASP.NET Core project. You can add multiple appsettings.json files with different names, such as appsettings.development.json, appsettings.production.json, etc., to manage configuration for different environments

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "MyCustomKey": "MyCustomKey Value coming from appsettings.json"
}
```

Here’s what each section typically means:

* Logging: Defines the logging level for different components of the application.
* AllowedHosts: Specifies the hosts that the application will listen to.
* MyCustomSettings: A custom section that you define for your own application settings.

To access the settings in the appsettings.json file, you can use the Configuration object in the Startup class. For example:
```
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}

public IConfiguration Configuration { get; }
```
You can then access the settings using the GetValue method of the Configuration object. For example:

```
string connectionString = Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
```
```
public class TestModel : PageModel
{
    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration Configuration;

    public TestModel(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public ContentResult OnGet()
    {
        var myKeyValue = Configuration["MyKey"];
        var title = Configuration["Position:Title"];
        var name = Configuration["Position:Name"];
        var defaultLogLevel = Configuration["Logging:LogLevel:Default"];


        return Content($"MyKey value: {myKeyValue} \n" +
                       $"Title: {title} \n" +
                       $"Name: {name} \n" +
                       $"Default Log Level: {defaultLogLevel}");
    }
}
```

You can also use the appsettings.json file to store different configuration values for different environments, such as development, staging, and production. To do this, you can use the appsettings.{Environment}.json naming convention, where {Environment} is the name of the environment. For example, you might have separate appsettings.Development.json, appsettings.Staging.json, and appsettings.Production.json files for different environments. The ASP.NET Core runtime will automatically use the appropriate file based on the current environment.

<a id="ef_core"></a>
# EF Core
Entity Framework (EF) Core is a lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology.

EF Core can serve as an object-relational mapper (O/RM), which:

* Enables .NET developers to work with a database using .NET objects.
* Eliminates the need for most of the data-access code that typically needs to be written.

## The model
With EF Core, data access is performed using a model. A model is made up of entity classes and a context object that represents a session with the database. The context object allows querying and saving data

EF supports the following model development approaches:

* Generate a model from an existing database.
* Hand code a model to match the database.
* Once a model is created, use EF Migrations to create a database from the model. Migrations allow evolving the database as the model changes.

```
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Intro;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True");
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    public int Rating { get; set; }
    public List<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
```

## Querying
Instances of your entity classes are retrieved from the database using Language Integrated Query (LINQ). For more information, see Querying Data.
```
using (var db = new BloggingContext())
{
    var blogs = db.Blogs
        .Where(b => b.Rating > 3)
        .OrderBy(b => b.Url)
        .ToList();
}
```

## Saving data
Data is created, deleted, and modified in the database using instances of your entity classes. See Saving Data to learn more.
```
using (var db = new BloggingContext())
{
    var blog = new Blog { Url = "http://sample.com" };
    db.Blogs.Add(blog);
    db.SaveChanges();
}
```

## Install Entity Framework Core
To install EF Core, you install the package for the EF Core database provider(s) you want to target. This tutorial uses SQLite because it runs on all platforms that .NET supports. For a list of available providers

```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

## Create the database
The following steps use migrations to create a database.

Run the following commands:
```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```
This installs dotnet ef and the design package which is required to run the command on a project. The migrations command scaffolds a migration to create the initial set of tables for the model. The database update command creates the database and applies the new migration to it.

## Create, read, update & delete
Open Program.cs and replace the contents with the following code:
```
using System;
using System.Linq;

using var db = new BloggingContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
db.SaveChanges();

// Read
Console.WriteLine("Querying for a blog");
var blog = db.Blogs
    .OrderBy(b => b.BlogId)
    .First();

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
db.SaveChanges();

// Delete
Console.WriteLine("Delete the blog");
db.Remove(blog);
db.SaveChanges();
```

<a id="architecture"></a>
# Architecture
## Onion Architecture
What is the Onion Architecture?
The Onion architecture is a form of layered architecture and we can visualize these layers as concentric circles. Hence the name Onion architecture. The Onion architecture was first introduced by Jeffrey Palermo, to overcome the issues of the traditional N-layered architecture approach.

There are multiple ways that we can split the onion, but we are going to choose the following approach where we are going to split the architecture into 4 layers:

* Domain Layer
* Service Layer
* Infrastructure Layer
* Presentation Layer

Conceptually, we can consider that the Infrastructure and Presentation layers are on the same level of the hierarchy.

![onion](https://code-maze.com/wp-content/uploads/2021/07/onion_architecture.jpeg)

## Flow of Dependencies
The main idea behind the Onion architecture is the flow of dependencies, or rather how the layers interact with each other. The deeper the layer resides inside the Onion, the fewer dependencies it has

The Domain layer does not have any direct dependencies on the outside layers. It is isolated, in a way, from the outside world. The outer layers are all allowed to reference the layers that are directly below them in the hierarchy.

We can use lower layers of the Onion architecture to define contracts or interfaces. The outer layers of the architecture implement these interfaces. This means that in the Domain layer, we are not concerning ourselves with infrastructure details such as the database or external services

Using this approach, we can encapsulate all of the rich business logic in the Domain and Service layers without ever having to know any implementation details. In the Service layer, we are going to depend only on the interfaces that are defined by the layer below, which is the Domain layer.

Key Layers of Onion Architecture:
* Presentation Layer: Handles user interaction and input/output operations. It consists of user interfaces, controllers, and view models.

* Application Layer: Contains application-specific logic and serves as the entry point for the presentation layer. It encapsulates use cases, such as user management, authentication, and business workflows.

* Domain Layer: Represents the core business logic of the application. It consists of entities, aggregates, and domain services. This layer should be independent of any infrastructure or framework concerns.

* Infrastructure Layer: Provides implementations for external services and frameworks. It includes data access, third-party integrations, logging, and caching.

Domain and Application Layer will be at the center of the design. We can refer to these layers as the Core Layers. These layers will not depend on any other layers.

Domain Layer usually contains enterprise logic and entities. Application Layer would have Interfaces and types. The main difference is that The Domain Layer will have the types that are common to the entire enterprise, hence can be shared across other solutions as well. But the Application Layer has Application-specific types and interfaces. Understand?

As mentioned earlier, the Core Layers will never depend on any other layer. Therefore what we do is that we create interfaces in the Application Layer and these interfaces get implemented in the external layers. This is also known as DIP or Dependency Inversion Principle.

For example, If your application want’s to send a mail, We define an IMailService in the Application Layer and Implement it outside the Core Layers. Using DIP, it is easily possible to switch the implementations. This helps build scalable applications.

The presentation layer is where you would Ideally want to put the Project that the User can Access. This can be a WebApi, Mvc Project, etc.

The infrastructure layer is a bit more tricky. It is where you would want to add your Infrastructure. Infrastructure can be anything. Maybe an Entity Framework Core Layer for Accessing the DB, a Layer specifically made to generate JWT Tokens for Authentication or even a Hangfire Layer. You will understand more when we start Implementing Onion Architecture in ASP.NET Core WebApi Project

Under the Blank Solution, add 3 new folders.

![onion](https://www.hosting.work/wp-content/uploads/2021/06/onion-architecture-layers.png?ezimgfmt=rs:744x332/rscb5/ng:webp/ngcb4)

* Core – will contain the Domain and Application layer Projects
* Infrastructure – will include any projects related to the Infrastructure of the ASP.NET Core Web API (Authentication, Persistence, etc)
* Presentation – The Projects that are linked to the UI or API. In our case, this folder will hold the API Project.

Layers
* Domain Layer: This layer does not depend on any other layer. This layer contains entities, enums, specifications etc.
Add repository and unit of work contracts in this layer.

* Application Layer: This layer contains business logic, services, service interfaces, request and response models.
Third party service interfaces are also defined in this layer.
This layer depends on domain layer.

* Infrastructure Layer: This layer contains database related logic (Repositories and DbContext), and third party library implementation (like logger and email service).
This implementation is based on domain and application layer.

* Presentation Layer: This layer contains Webapi or UI.
