# dotnet
learn .NET 

* [C#](#c_sharp)
    * [lambda](#lambda)
    * [using](#using)
    * [LINQ](#linq)
    * [Generic](#generic)
* [Design Pattern](#design_pattern)
    * [Fluent](#fluent)
* [ASP Core](#asp_core)
* [EF Core](#ef_core)
    * [Entity Properties](#entity_properties)
* [Architecture](#architecture)
    * [onion](#onion)
* [API Guide](#api_guide)


<a id="c_sharp"></a>
# C#
<a id="lambda"></a>
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

### Declaring functions
```
Func<int, int, int> Multiply = (x,y) => x*y;
int result = Mutliply(3,5); // 15
```

Simple multiplication function that takes in two numbers and multiplys them to return another int.
Func states that we're going to pass it two ints, and it's going to return an int too.

It follows the following formula Func<in, out> or Func<in,in,in,out> the out is always the last specified type.

For example if we wanted it to take two integers, but return as a string we'd declare like this

```
Func<int,int,string> Multiply = (x,y) => (x*y).ToString();
```

Single line declarations don't require the return keyword, it's done implicitly, however multiline functions would, for example:
```
Func<string, string> GetGreeting = (name) => {
    var timeOfDay = DateTime.Now.Hour < 12 ? "Good Morning" : "Good Afternoon";
    var message = DateTime.Now.Hour < 17 ? "Have a great evening" : "Enjoy the rest of your day";
    return $"{timeOfDay} - {name} -- {message}";
};
Console.WriteLine(GetGreeting("Grant"));   
```

A statement lambda has a block of statements as its body.
Syntax: (input-parameters) => { <sequence-of-statements> }
Example:
```
Action<string> greet = name => {
    Console.WriteLine($"Hello, {name}!");
};
greet("Alice"); // Output: Hello, Alice!
```

<a id="using"></a>
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

<a id="linq"></a>
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

<a id="generic"></a>
## Generic classes and methods
C# Generics allow us to create a single class or method that can be used with different types of data. This helps us to reuse our code.
Here, we will learn to create generics class and method in C#.

### C# generics Class
A generics class is used to create an instance of any data type. To define a generics class, we use angle brackets (<>) as,

```
class Student<T>
{
  // block of code 
}
```

Here, we have created a generics class named Student. T used inside the angle bracket is called the type parameter.
While creating an instance of the class, we specify the data type of the object which replaces the type parameter.

Create an Instance of Generics Class
Let's create two instances of the generics class.
```
// create an instance with data type string
Student<string> studentName = new Student<string>();
```
```
// create an instance with data type int
Student<int> studentId = new Student<int>();
```
Here, we have created two instances named studentName and studentId with data types string and int, respectively.

During the time of compilation, the type parameter T of the Student class is replaced by,

* string - for instance studentName
* int - for instance studentId

Example: C# generics Class
```
using System;
// define a generics class named Student
public class Student<T>
{
    // define a variable of type T 
    public T data;

    // define a constructor of the Student class 
    public Student(T data)
    {
        this.data = data;
        Console.WriteLine("Data passed: " + this.data);
    }
}

class Program
{
    static void Main()
    {
        // create an instance with data type string 
        Student<string> studentName = new Student<string>("Avicii");

        // create an instance with data type int
        Student<int> studentId = new Student<int>(23);
    }
}
```

Output
```
Data passed: Avicii
Data passed: 23
```

C# generics Method
Similar to the generics class, we can also create a method that can be used with any type of data. Such a class is known as the generics Method. For example,
```
public void displayData(T data) {
    Console.WriteLine("Data Passed: " + data);
}
```
Here,

* displayData - name of the generics method
* T - type parameter to specify the function can accept any type of data
* data - function parameter
Now we can use this function to work with any type of data. For example,
```
// calling function with integer data
obj.displayData(34);
```
```
// calling function with string data
obj.displayData("Tim");
```

Example:
```
// C# program to show multiple
// type parameters in Generics
using System;

public class GFG {
	
	// Generics method
	public void Display<TypeOfValue>(string msg, TypeOfValue value)
	{
		Console.WriteLine("{0}:{1}", msg, value);
	}
}

// Driver class
public class Example {
	
	// Main Method
	public static int Main()
	{
		
		// creating object of class GFG
		GFG p = new GFG();
		
		// calling Generics method
		p.Display<int>("Integer", 122);
		p.Display<char>("Character", 'H');
		p.Display<double>("Decimal", 255.67);
		return 0;
	}
}
```

Output :
```
Integer:122
Character:H
Decimal:255.67
```

<a id="design_pattern"></a>
# Design Pattern

<a id="fluent"></a>
## Fluent Interface Design Pattern
The Fluent Interface Design Pattern in C# is a way of implementing object-oriented APIs in a manner that aims to provide more readable and discoverable code. It often involves method chaining, where each method returns the same context object, invoking multiple actions or commands in a single line of code. 

The core idea behind the Fluent Interface pattern is to make code more readable and to make the client code look like a domain-specific language. The Fluent Interface Design Pattern’s main objective is to apply multiple properties (or methods) to an object by connecting them with dots (.) without re-specifying the object name each time

Let us understand How to Implement the Fluent Interface Design Pattern in C# with an Example. Let’s say we have the following Employee class.

![linq](https://dotnettutorials.net/wp-content/uploads/2019/10/c-users-pranaya-pictures-understanding-fluent-int.png?ezimgfmt=ng:webp/ngcb8)

If we want to consume the above Employee class, we generally create an instance of the Employee class and set the respective properties as shown below.

![linq](https://dotnettutorials.net/wp-content/uploads/2019/10/c-users-pranaya-pictures-without-fluent-interface.png?ezimgfmt=ng:webp/ngcb8)

The Fluent interfaces simplify our object consumption code by making our code more simple, readable, and discoverable. Is it not nice to set the object properties as shown below?

![linq](https://dotnettutorials.net/wp-content/uploads/2019/10/c-users-pranaya-pictures-fluent-interface-design.png?ezimgfmt=ng:webp/ngcb8)

Creating such interfaces is like speaking a sentence that would make the class consumption code more simple and readable. Now, the next thing is how to achieve this. To achieve this, we have something called Method Chaining in C#

What is Method Chaining in C#?
Method Chaining in C# is a common technique where each method returns an object, and all these methods can be chained together to form a single statement. To achieve this, first, we need to create a wrapper class around the Employee class, as shown below.

![linq](https://dotnettutorials.net/wp-content/uploads/2019/10/c-users-pranaya-pictures-fluent-interface-design-1.png?ezimgfmt=ng:webp/ngcb8)

As you can see, here we have created methods for each property. Also, notice the return of the method is set to the FluentEmployee. Now, the above fluent interface is going to be consumed by the client. So, with the above FluentEmployee class in place, the client code should look as shown below.

![linq](https://dotnettutorials.net/wp-content/uploads/2019/10/c-users-pranaya-pictures-method-chaining-in-c-pn.png?ezimgfmt=ng:webp/ngcb8)

Example to Implement Fluent Interface Design Pattern using C#
Whatever we have discussed so far, let us implement the same using the Fluent Interface Design Pattern in C#. First, we need to create a class file named Employee.cs and then copy and paste the following code into it. This is a simple class having few properties. We aim to set these property values using Method Chaining
```
using System;
namespace FluentInterfaceDesignPattern
{
    public class Employee
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
    }
}
```
Creating Wrapper:
To set the above Employee class Property Values using Method Chaining, we must create a Wrapper class around the Employee class. So, create a class file named FluentEmployee.cs and copy and paste the following code. This is going to be our Wrapper class. If you notice, here, we have created an instance of the Employee class. For each Employee class property, here we have created a corresponding method, and using those methods, we are setting the Employee object property values. Further, if you notice, the return type of each method (except ShowDetails) is FluentEmployee type, which is important. Because of this return type (which is returning the FluentEmployee instance), we can call the FluentEmployee class methods (except ShowDetails) one after another using the dot operator. The ShowDetails method is not for setting the values; it is used to display them.

```
using System;
namespace FluentInterfaceDesignPattern
{
    public class FluentEmployee
    {
        private Employee employee = new Employee();
        public FluentEmployee NameOfTheEmployee(string FullName)
        {
            employee.FullName = FullName;
            return this;
        }
        public FluentEmployee Born(string DateOfBirth)
        {
            employee.DateOfBirth = Convert.ToDateTime(DateOfBirth);
            return this;
        }
        public FluentEmployee WorkingOn(string Department)
        {
            employee.Department = Department;
            return this;
        }
        public FluentEmployee StaysAt(string Address)
        {
            employee.Address = Address;
            return this;
        }
        public void ShowDetails()
        {
            Console.WriteLine($"Name: {employee.FullName}, \nDateOfBirth: {employee.DateOfBirth}, \nDepartment: {employee.Department}, \nAddress: {employee.Address}");
        }
    }
}
```
```
using System;
namespace FluentInterfaceDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create an Instance of Wrapper class i.e. FluentEmployee
            FluentEmployee obj = new FluentEmployee();

            //Call Methods one by one using dot Operator whose Return Type is FluentEmployee
            obj.NameOfTheEmployee("Anurag Mohanty")
                    .Born("10/10/1992")
                    .WorkingOn("IT")
                    .StaysAt("Mumbai-India");

            //To See the Details call the ShowDetails Method
            obj.ShowDetails();
            Console.Read();
        }
    }
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

## What are projection and selection?
**Projection** means choosing which columns (or expressions) the query shall return.

**Selection** means which rows are to be returned.

<a id="entity_properties"></a>
## Entity Properties
Each entity type in your model has a set of properties, which EF Core will read and write from the database. If you're using a relational database, entity properties map to table columns.

### Included and excluded properties
By convention, all public properties with a getter and a setter will be included in the model.
Specific properties can be excluded as follows:

```
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    [NotMapped]
    public DateTime LoadedFromDatabase { get; set; }
}
```

### Column names
By convention, when using a relational database, entity properties are mapped to table columns having the same name as the property.
If you prefer to configure your columns with different names, you can do so as following code snippet:

```
public class Blog
{
    [Column("blog_id")]
    public int BlogId { get; set; }

    public string Url { get; set; }
}
```

### Column data types
When using a relational database, the database provider selects a data type based on the .NET type of the property. It also takes into account other metadata, such as the configured maximum length, whether the property is part of a primary key, etc.

For example, SQL Server maps DateTime properties to datetime2(7) columns, and string properties to nvarchar(max) columns (or to nvarchar(450) for properties that are used as a key).

You can also configure your columns to specify an exact data type for a column. For example, the following code configures Url as a non-unicode string with maximum length of 200 and Rating as decimal with precision of 5 and scale of 2:

```
public class Blog
{
    public int BlogId { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string Url { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Rating { get; set; }
}
```

### Maximum length
Configuring a maximum length provides a hint to the database provider about the appropriate column data type to choose for a given property. Maximum length only applies to array data types, such as string and byte[].

In the following example, configuring a maximum length of 500 will cause a column of type nvarchar(500) to be created on SQL Server:

```
public class Blog
{
    public int BlogId { get; set; }

    [MaxLength(500)]
    public string Url { get; set; }
}
```

### Indexes
Indexes are a common concept across many data stores. While their implementation in the data store may vary, they are used to make lookups based on a column (or set of columns) more efficient. See the indexes section in the performance documentation for more information on good index usage.

You can specify an index over a column as follows:

```
[Index(nameof(Url))]
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}
```

### Keys
A key serves as a unique identifier for each entity instance. Most entities in EF have a single key, which maps to the concept of a primary key in relational databases (for entities without keys, see Keyless entities). Entities can have additional keys beyond the primary key (see Alternate Keys for more information).

Configuring a primary key
By convention, a property named Id or <type name>Id will be configured as the primary key of an entity.

```
internal class Car
{
    public string Id { get; set; }

    public string Make { get; set; }
    public string Model { get; set; }
}

internal class Truck
{
    public string TruckId { get; set; }

    public string Make { get; set; }
    public string Model { get; set; }
}
```

You can configure a single property to be the primary key of an entity as follows:

```
internal class Car
{
    [Key]
    public string LicensePlate { get; set; }

    public string Make { get; set; }
    public string Model { get; set; }
}
```

You can also configure multiple properties to be the key of an entity - this is known as a composite key. Conventions will only set up a composite key in specific cases - like for an owned type collection.

```
[PrimaryKey(nameof(State), nameof(LicensePlate))]
internal class Car
{
    public string State { get; set; }
    public string LicensePlate { get; set; }

    public string Make { get; set; }
    public string Model { get; set; }
}
```

<a id="architecture"></a>
# Architecture

<a id="onion"></a>
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

<a id="api_guide"></a>
# API Guide

## REST API Design Best Practices
### Use JSON as the Format for Sending and Receiving Data
In the past, accepting and responding to API requests were done mostly in XML and even HTML. But these days, JSON (JavaScript Object Notation) has largely become the de-facto format for sending and receiving API data

To ensure the client interprets JSON data correctly, you should set the Content-Type type in the response header to application/json while making the request.

### Use Nouns Instead of Verbs in Endpoints
When you're designing a REST API, you should not use verbs in the endpoint paths. The endpoints should use nouns, signifying what each of them does.

This is because HTTP methods such as GET, POST, PUT, PATCH, and DELETE are already in verb form for performing basic CRUD (Create, Read, Update, Delete) operations.

GET, POST, PUT, PATCH, and DELETE are the commonest HTTP verbs. There are also others such as COPY, PURGE, LINK, UNLINK, and so on.

So, for example, an endpoint should not look like this:

https://mysite.com/getPosts or https://mysite.com/createPost

Instead, it should be something like this: https://mysite.com/posts

### Name Collections with Plural Nouns
You can think of the data of your API as a collection of different resources from your consumers.

If you have an endpoint like https://mysite.com/post/123, it might be okay for deleting a post with a DELETE request or updating a post with PUT or PATCH request, but it doesn’t tell the user that there could be some other posts in the collection. This is why your collections should use plural nouns.

So, instead of https://mysite.com/post/123, it should be https://mysite.com/posts/123.\

### Use Status Codes in Error Handling
You should always use regular HTTP status codes in responses to requests made to your API. This will help your users to know what is going on – whether the request is successful, or if it fails, or something else.

### Use Nesting on Endpoints to Show Relationships
Oftentimes, different endpoints can be interlinked, so you should nest them so it's easier to understand them.

For example, in the case of a multi-user blogging platform, different posts could be written by different authors, so an endpoint such as https://mysite.com/posts/author would make a valid nesting in this case.

In the same vein, the posts might have their individual comments, so to retrieve the comments, an endpoint like https://mysite.com/posts/postId/comments would make sense.

You should avoid nesting that is more than 3 levels deep as this can make the API less elegant and readable.

### Use Filtering, Sorting, and Pagination to Retrieve the Data Requested
Sometimes, an API's database can get incredibly large. If this happens, retrieving data from such a database could be very slow.

Filtering, sorting, and pagination are all actions that can be performed on the collection of a REST API. This lets it only retrieve, sort, and arrange the necessary data into pages so the server doesn’t get too occupied with requests.

An example of a filtered endpoint is the one below:
https://mysite.com/posts?tags=javascript
This endpoint will fetch any post that has a tag of JavaScript.

### Be Clear with Versioning
REST APIs should have different versions, so you don’t force clients (users) to migrate to new versions. This might even break the application if you're not careful.

One of the commonest versioning systems in web development is semantic versioning.

An example of semantic versioning is 1.0.0, 2.1.2, and 3.3.4. The first number represents the major version, the second number represents the minor version, and the third represents the patch version.

Many RESTful APIs from tech giants and individuals usually comes like this:
https://mysite.com/v1/ for version 1
https://mysite.com/v2 for version 2

### Given Pattern 
```
Title
Path:
Description:
Http method type: GET - PUT - POST - DELETE
Parameters:
    Description
	Example:
    Place: Header - Body
	Type: String - Integer - Guid
	Mandatory: Yes - No
	Validity Check: None needed
Example:
Validity Check: None needed
Response: 200 Ok + value
```