# dotnet
learn .NET 

* [C#](#c_sharp)
    * [lambda](#lambda)
    * [using](#using)
    * [LINQ](#linq)
    * [Generic](#generic)
    * [Anonymous Type](#anonymous_type)
    * [Delegates](#delegates)
    * [Func](#func)
    * [Action](#action)
    * [Predicate](#predicate)
    * [Records](#records)
    * [DTO](#dto)
    * [AutoMapper](#auto_mapper)
    * [Autofac](#autofac)
* [Design Pattern](#design_pattern)
    * [Fluent](#fluent)
    * [Dependency Injection](#dependency_injection)
* [ASP Core](#asp_core)
* [EF Core](#ef_core)
    * [Entity Properties](#entity_properties)
    * [Migration](#migration)
    * [Saving Data](#saving_data)
    * [Query](#query)
    * [Difference between Primary Key and Foreign Key](#pk_fk)
    * [Relationships](#relationships)
    * [Soft Delete](#soft_delete)
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
more:
* [type relationships in linq query](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/type-relationships-in-linq-query-operations)

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

<a id="anonymous_type"></a>
## Anonymous Type
In C#, an anonymous type is a type (class) without any name that can contain public read-only properties only. It cannot contain other members, such as fields, methods, events, etc.

You create an anonymous type using the new operator with an object initializer syntax. The implicitly typed variable- var is used to hold the reference of anonymous types.

The following example demonstrates creating an anonymous type variable student that contains three properties named Id, FirstName, and LastName.

In C#, you are allowed to create an anonymous type object with a new keyword without its class definition and var is used to hold the reference of the anonymous types

```
var student = new { Id = 1, FirstName = "James", LastName = "Bond" };
```

**Because an anonymous type has no name, it is not possible to declare a local variable as explicitly being of an anonymous type. Rather, the local variable's type is replaced with var. However, by no means does this indicate that implicitly typed variables are untyped.**


<a id="delegates"></a>
## Delegates

What if we want to pass a function as a parameter? How does C# handles the callback functions or event handler? The answer is - delegate.

The delegate is a reference type data type that defines the method signature. You can define variables of delegate, just like other data type, that can refer to any method with the same signature as the delegate.

There are three steps involved while working with delegates:

* Declare a delegate
* Create an instance and reference a method
* Invoke a delegate
A delegate can be declared using the delegate keyword followed by a function signature, as shown below.

Delegate Syntax
```
[access modifier] delegate [return type] [delegate name]([parameters])
```
The following declares a delegate named MyDelegate.

Example: Declare a Delegate
```
public delegate void MyDelegate(string msg);
```
Above, we have declared a delegate MyDelegate with a void return type and a string parameter. A delegate can be declared outside of the class or inside the class. Practically, it should be declared out of the class.

After declaring a delegate, we need to set the target method or a lambda expression. We can do it by creating an object of the delegate using the new keyword and passing a method whose signature matches the delegate signature.

Example: Set Delegate Target
```
public delegate void MyDelegate(string msg); // declare a delegate

// set target method
MyDelegate del = new MyDelegate(MethodA);
// or 
MyDelegate del = MethodA; 
// or set lambda expression 
MyDelegate del = (string msg) =>  Console.WriteLine(msg);

// target method
static void MethodA(string message)
{
    Console.WriteLine(message);
}
```
You can set the target method by assigning a method directly without creating an object of delegate e.g., MyDelegate del = MethodA.

After setting a target method, a delegate can be invoked using the Invoke() method or using the () operator.

Example: Invoke a Delegate
```
del.Invoke("Hello World!");
// or 
del("Hello World!");
```
The following is a full example of a delegate.

Example: Delegate
```
public delegate void MyDelegate(string msg); //declaring a delegate

class Program
{
    static void Main(string[] args)
    {
        MyDelegate del = ClassA.MethodA;
        del("Hello World");

        del = ClassB.MethodB;
        del("Hello World");

        del = (string msg) => Console.WriteLine("Called lambda expression: " + msg);
        del("Hello World");
    }
}

class ClassA
{
    static void MethodA(string message)
    {
        Console.WriteLine("Called ClassA.MethodA() with parameter: " + message);
    }
}

class ClassB
{
    static void MethodB(string message)
    {
        Console.WriteLine("Called ClassB.MethodB() with parameter: " + message);
    }
}
```

The following image illustrates the delegate.

![delegates](https://www.tutorialsteacher.com/Content/images/csharp/delegate-mapping.png)

### Passing Delegate as a Parameter
A method can have a parameter of the delegate type, as shown below.

Example: Delegate
```
public delegate void MyDelegate(string msg); //declaring a delegate

class Program
{
    static void Main(string[] args)
    {
        MyDelegate del = ClassA.MethodA;
        InvokeDelegate(del);

        del = ClassB.MethodB;
        InvokeDelegate(del);

        del = (string msg) => Console.WriteLine("Called lambda expression: " + msg);
        InvokeDelegate(del);
    }

    static void InvokeDelegate(MyDelegate del) // MyDelegate type parameter
    {
        del("Hello World");
    }
}

class ClassA
{
    static void MethodA(string message)
    {
        Console.WriteLine("Called ClassA.MethodA() with parameter: " + message);
    }
}

class ClassB
{
    static void MethodB(string message)
    {
        Console.WriteLine("Called ClassB.MethodB() with parameter: " + message);
    }
}
```

<a id="func"></a>
## Func
C# includes built-in generic delegate types Func and Action, so that you don't need to define custom delegates manually in most cases.

Func is a generic delegate included in the System namespace. It has zero or more input parameters and one out parameter. The last parameter is considered as an out parameter.

The Func delegate that takes one input parameter and one out parameter is defined in the System namespace, as shown below:

Signature: Func
```
namespace System
{    
    public delegate TResult Func<in T, out TResult>(T arg);
}
```
The last parameter in the angle brackets <> is considered the return type, and the remaining parameters are considered input parameter types, as shown in the following figure.

![](https://www.tutorialsteacher.com/Content/images/csharp/func-delegate.png)

A Func delegate with two input parameters and one out parameters will be represented as shown below

![](https://www.tutorialsteacher.com/Content/images/csharp/func-delegate2.png)

The following Func delegate takes two input parameters of int type and returns a value of int type:
```
Func<int, int, int> sum;
```
You can assign any method to the above func delegate that takes two int parameters and returns an int value.

Example: Func
```
class Program
{
    static int Sum(int x, int y)
    {
        return x + y;
    }

    static void Main(string[] args)
    {
        Func<int,int, int> add = Sum;

        int result = add(10, 10);

        Console.WriteLine(result); 
    }
}
```

Output:
```
20
```

A Func delegate type can include 0 to 16 input parameters of different types. However, it must include an out parameter for the result. For example, the following Func delegate doesn't have any input parameter, and it includes only an out parameter.

Example: Func with Zero Input Parameter
```
Func<int> getRandomNumber;
```

### C# Func with an Anonymous Method
You can assign an anonymous method to the Func delegate by using the delegate keyword.

Example: Func with Anonymous Method
```
Func<int> getRandomNumber = delegate()
                            {
                                Random rnd = new Random();
                                return rnd.Next(1, 100);
                            };
```

### Func with Lambda Expression
A Func delegate can also be used with a lambda expression, as shown below:

Example: Func with lambda expression
```
Func<int> getRandomNumber = () => new Random().Next(1, 100);
//Or 
Func<int, int, int>  Sum  = (x, y) => x + y;
```


<a id="action"></a>
## Action
Action is a delegate type defined in the System namespace. An Action type delegate is the same as Func delegate except that the Action delegate doesn't return a value. In other words, an Action delegate can be used with a method that has a void return type.

For example, the following delegate prints an int value.

Example: C# Delegate
```
public delegate void Print(int val);

static void ConsolePrint(int i)
{
    Console.WriteLine(i);
}

static void Main(string[] args)
{           
    Print prnt = ConsolePrint;
    prnt(10);
}
```

Output:
```
10
```

You can use an Action delegate instead of defining the above Print delegate, for example:

Example: Action delegate
```
static void ConsolePrint(int i)
{
    Console.WriteLine(i);
}

static void Main(string[] args)
{
    Action<int> printActionDel = ConsolePrint;
    printActionDel(10);
}
```

You can initialize an Action delegate using the new keyword or by directly assigning a method:
```
Action<int> printActionDel = ConsolePrint;

//Or

Action<int> printActionDel = new Action<int>(ConsolePrint);
```

An Action delegate can take up to 16 input parameters of different types.

An Anonymous method can also be assigned to an Action delegate, for example:

Example: Anonymous method with Action delegate
```
static void Main(string[] args)
{
    Action<int> printActionDel = delegate(int i)
                                {
                                    Console.WriteLine(i);
                                };

    printActionDel(10);
}
```

Output:
```
10
```

A Lambda expression also can be used with an Action delegate:

Example: Lambda expression with Action delegate
```
static void Main(string[] args)
{

    Action<int> printActionDel = i => Console.WriteLine(i);
       
    printActionDel(10);
}
```
Thus, you can use any method that doesn't return a value with Action delegate types.

Advantages of Action and Func Delegates
* Easy and quick to define delegates.
* Makes code short.
* Compatible type throughout the application.


<a id="predicate"></a>
## Predicate 
Predicate is the delegate like Func and Action delegates. It represents a method containing a set of criteria and checks whether the passed parameter meets those criteria. A predicate delegate methods must take one input parameter and return a boolean - true or false.

The Predicate delegate is defined in the System namespace, as shown below:

```
Predicate signature: public delegate bool Predicate<in T>(T obj);
```

Same as other delegate types, Predicate can also be used with any method, anonymous method, or lambda expression.

Example: Predicate delegate
```
static bool IsUpperCase(string str)
{
    return str.Equals(str.ToUpper());
}

static void Main(string[] args)
{
    Predicate<string> isUpper = IsUpperCase;

    bool result = isUpper("hello world!!");

    Console.WriteLine(result);
}
```

Output:
```
false
```

An anonymous method can also be assigned to a Predicate delegate type as shown below.

Example: Predicate delegate with anonymous method
```
static void Main(string[] args)
{
    Predicate<string> isUpper = delegate(string s) { return s.Equals(s.ToUpper());};
    bool result = isUpper("hello world!!");
}
```
A lambda expression can also be assigned to a Predicate delegate type as shown below.

Example: Predicate delegate with lambda expression
```
static void Main(string[] args)
{
    Predicate<string> isUpper = s => s.Equals(s.ToUpper());
    bool result = isUpper("hello world!!");
}
```

Let's explore **records** in C# and understand how they simplify working with data models. Records provide special syntax and behavior for creating objects that store data. Here are the key points about records:


<a id="records"></a>
## Records

1. **What Are Records?**
   - A **record** in C# is either a **class** or a **struct** that focuses on efficiently storing data.
   - It provides built-in functionality for common scenarios related to data storage.

2. **Syntax and Behavior:**
   - The `record` modifier instructs the compiler to synthesize useful members for data-centric types.
   - These synthesized members include:
     - An overload of `ToString()` for better string representation.
     - Members that support **value equality** (comparing instances based on content).

3. **Records vs. Classes and Structs:**
   - Records are **immutable** by default, making them thread-safe.
   - They behave like values but are actually **reference types**.
   - Records are primarily intended for **immutable data models**.

4. **Positional Parameters:**
   - When you declare a primary constructor on a record, the compiler generates public properties for the primary constructor parameters.
   - These parameters are referred to as **positional parameters**.
   - The compiler creates positional properties that mirror the primary constructor.

5. **Examples:**

   - **Record Class (Reference Type):**
     ```csharp
     public record Person(string FirstName, string LastName);
     ```

     Or with explicit property definitions:
     ```csharp
     public record Person
     {
         public string FirstName { get; init; }
         public string LastName { get; init; }
     }
     ```

   - **Record Struct (Value Type):**
     ```csharp
     public readonly record struct Point(double X, double Y, double Z);
     ```

     Or with explicit property definitions:
     ```csharp
     public record struct Point
     {
         public double X { get; init; }
         public double Y { get; init; }
         public double Z { get; init; }
     }
     ```

6. **Mutability:**
   - While records can be mutable, they are primarily designed for immutability.
   - Records with mutable properties and fields are possible but less common.

Remember, records offer concise syntax, immutability, and value equality, making them a powerful tool for managing data in your C# applications! 🚀📊

For more details, you can explore the [official Microsoft Learn documentation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record) and other resources ¹²³.

Source: Conversation with Bing, 4/7/2024
(1) Records - C# reference - C# | Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record.
(2) Working with Record Types in C# | CodeGuru.com. https://www.codeguru.com/csharp/record-types-c-sharp/.
(3) Records in C# - C# | Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/records.
(4) Creating and Working with Records in C#: Tutorial (2024). https://www.bytehide.com/blog/records-csharp.
(5) Use record types - C# tutorial - C# | Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/records.


### Differences between records and record structs
Certainly! Let's delve into the differences between **records** and **record structs** in C#.

1. **Records**:
   - A **record** is a type that provides special syntax and behavior for working with data models.
   - It is primarily used for representing data with minimal boilerplate code.
   - Records are reference types (similar to classes).
   - They are designed for **immutability** and are ideal for scenarios where you want to create objects with read-only properties.
   - Records automatically generate useful methods such as equality comparison, hash code calculation, and string representation.
   - Example of a record:

     ```csharp
     public record Person(string FirstName, string LastName);
     var person = new Person("John", "Doe");
     ```

   - In the above example, `Person` is a record with two positional parameters (`FirstName` and `LastName`). These parameters are automatically treated as read-only properties.

2. **Record Structs**:
   - A **record struct** is a value type (similar to structs).
   - Unlike records, record structs are mutable by default.
   - When using positional parameters in a record struct, those parameters are entirely mutable.
   - Example of a record struct:

     ```csharp
     public record struct Point(int X, int Y);
     var point = new Point(10, 20);
     point.X = 15; // Valid, even though it's a record struct
     ```

   - In the above example, `Point` is a record struct with two positional parameters (`X` and `Y`). You can modify their values directly.

3. **When to Use Each**:
   - **Records**: Use records when you need a simple data structure with immutability and automatic generation of useful methods (e.g., equality checks).
   - **Record Structs**: Use record structs when you want a lightweight value type that encapsulates related variables but allows mutation.

Remember that both records and record structs provide concise syntax for defining types, but their behavior and use cases differ. Choose the one that best fits your specific requirements! 🚀

Source: Conversation with Bing, 4/7/2024
(1) Classes, structs, and records in C# - C# | Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/.
(2) What's the Difference Anyway?! Class, Struct, Record, oh my!. https://www.codingwithcalvin.net/whats-the-difference-anyway-class-struct-record-oh-my/.
(3) Class, Struct, Record, Record Struct | by GM Fuster - Medium. https://medium.com/nerd-for-tech/c-class-struct-record-record-struct-d3b21c57d9bb.
(4) When to use record vs class vs struct in C#? - C# Corner. https://www.c-sharpcorner.com/article/when-to-use-record-vs-class-vs-struct-in-c-sharp/.
(5) c# - When to use record vs class vs struct - Stack Overflow. https://stackoverflow.com/questions/64816714/when-to-use-record-vs-class-vs-struct.


<a id="dto"></a>
## DTO

Imagine that you want to write a letter. What information would you include in it? You carefully share the necessary information with the recipient, not all the information you have.
The same applies when sharing data from your database with clients from other layers. You don’t want to share everything. Just like a letter, you only pack the necessary information into a plain object (collection of fields) called a Data Transfer Object (DTO).

Only sending the necessary data helps to improve performance and reduce the risk of data corruption. Your DTO acts as a little package that carries the information from one layer to another, allowing the data to be transferred in a serializable format and passed along a communication channel, such as over a network connection. So, think about what data is important for the recipient and include only that in your DTO, just like you would in a letter.

### Data Transfer Objects in Web
DTOs can be used to transfer data between the client and the server. You can use DTOs to transmit only representations of entity data that are required for performing specific operations between client and server.

In web applications, DTOs help to isolate the client-side code from the underlying data structures on the server side. In this article, we will examine the use of DTOs in C# web APIs, with practical examples to illustrate the benefits and usage of DTOs in API development.
In C# web APIs, DTOs can serve as an intermediary between the client and the server and provide an abstraction level that helps isolate the underlying data structure from the client-side code.

### Data Transfer Objects (DTOs) in C# Example
Here is an example of how DTOs. Suppose you have a web API that shares user information with clients. Users’ information is stored in a database and is represented by the following class:

```
public class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    // And more.
}
```

However, you don’t want to return all this information to the client. The server wants to return the user’s first name, last name, and email address. To accomplish this, you can create a DTO class that contains only the necessary information:

```
public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
```

Since the UserDto class is a DTO; it doesn’t have any methods or behavior, as you see.
The DTO class can then be used in your API controller to return the necessary information:

```
    [HttpGet("api/users/{id}")]
    public IActionResult GetUser(int id)
    {
        var user = database.Users.SingleOrDefault(user => user.Id == id);
        if (user == null)
        {
            return NotFound();
        }
 
        var userDto = new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
 
        return Ok(userDto);
    }
```
 
In this example, the DTO class provides a simplified representation of the user’s information. The API doesn’t share the user’s birth date and phone number in this example. You can ensure that the client only receives the necessary information, which can improve your API’s performance.

The server transferred an UserDto object back to the client over the network. The DTO is formatted as JSON. Your Web API can serialize the DTO as any other format like XML instead.

Serialization is the process of converting an object or data structure into a format (such as binary, XML, JSON) that can be stored, transmitted, or reconstructed later.

The serializability nature of DTOs enables them to be converted into a series of bytes transmitted over the network and reconstructed on the other end. DTOs can be passed between systems, processes, or like our example, between the layers of an application in a format that both the sender and the recipient can understand.

UserDto is only a subset of UserEntity fields reducing the amount of data transmitted over a network, thus conserving network bandwidth and enhancing network performance. DTOs can also make transmitting data over the network easier, even if it needs to traverse multiple systems or networks.

They also help to separate the data from the system’s behavior. As like everything else that’s constantly changing in this world, the UserEntity class might require to be changed later on. But you don’t want that change to leak into the client side.

In other words, you don’t want to update all clients (like the website, the mobile app, and other services) by each update to UserEntity, do you? UserDto helps you to isolate the changes to one server system from changes to its clients.

By the way, did you know that we offer a unique online course that boosts your C# career? Check it out here!

### Reasons to Use DTOs
DTOs help you with the following:
* **Data Abstraction:** DTOs provide a level of abstraction between the client-side code and the underlying data structure, which makes it easier to modify the data structure without affecting the client-side code.
* **Improved Performance:** By only transferring the necessary information, DTOs can significantly improve the performance of web APIs. This is because they reduce the amount of data that needs to be transferred, which can reduce network traffic and improve the overall response time of the API.
* **Increased Security:** By only returning the necessary information, DTOs can help to reduce the risk of exposing sensitive information to the client.
* **Interoperability:** DTOs make sharing data between different processes, threads, services, apps, and application layers easier.
* **Better maintainability:** DTOs make it easier to maintain the code because they provide a clear separation of concerns between the client and the server.

### Reasons to Not Use DTOs
There are a few cases where using DTOs may not be necessary:
* **Simple APIs:** If your API is very simple and only requires returning a small amount of data, using DTOs may add unnecessary complexity to the code.
* **Performance Concerns:** If the data transfer is slow due to the size of the data being transferred, using DTOs may not improve the performance of the API, as the DTOs themselves add some overhead.
* **Development Time:** Implementing DTOs can take more time during the development process and may not be necessary if the data structure is not likely to change in the future.
It’s important to note that these are just general guidelines, and the decision to use DTOs will depend on your API’s specific requirements. When in doubt, it’s a good idea to use DTOs.

### Data Transfer Objects in C# Summary
A Data Transfer Object (DTO) is a design pattern in computer software architecture used to transfer data between systems or between the layers of a software application. It is an object that carries data between processes in a serializable format, allowing the data to be passed along a communication channel, such as over a network connection. A DTO typically contains data members and transfers the data between a software application’s presentation layer and the business layer. The goal is to keep the data separate from the system’s behavior and reduce the amount of data that needs to be transferred, which can improve performance and reduce the risk of data corruption.
DTOs provide a layer of abstraction that helps to isolate the client-side code from the underlying data structure, enabling you to easily change the data structure without affecting the client-side code.


<a id="auto_mapper"></a>
## Auto Mapper

**AutoMapper** is a powerful library in C# that simplifies the process of mapping data between objects. It's particularly useful when you need to transfer data from one object (source) to another (destination) with similar properties. Let's dive into it with examples.

### Why Do We Need AutoMapper in C#?
Consider two classes: `Employee` and `EmployeeDTO`. Both have identical properties: `Name`, `Salary`, `Address`, and `Department`. Our goal is to copy data from an `Employee` object to an `EmployeeDTO` object.

#### Traditional Approach (Without AutoMapper):
In the traditional approach, we manually create and populate the `Employee` object and then copy the data to the `EmployeeDTO` object. Here's how it looks:

```csharp
using System;

namespace AutoMapperDemo
{
    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }

    public class EmployeeDTO
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee
            {
                Name = "James",
                Salary = 20000,
                Address = "London",
                Department = "IT"
            };

            EmployeeDTO empDTO = new EmployeeDTO
            {
                Name = emp.Name,
                Salary = emp.Salary,
                Address = emp.Address,
                Department = emp.Department
            };

            Console.WriteLine($"Name: {empDTO.Name}, Salary: {empDTO.Salary}, Address: {empDTO.Address}, Department: {empDTO.Department}");
            Console.ReadLine();
        }
    }
}
```

#### Using AutoMapper:
Now, let's see how AutoMapper simplifies this process. First, install the AutoMapper NuGet package. Then, initialize AutoMapper and create a mapping configuration:

```csharp
using AutoMapper;

namespace AutoMapperDemo
{
    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }

    public class EmployeeDTO
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Initialize AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDTO>());

            Employee emp = new Employee
            {
                Name = "James",
                Salary = 20000,
                Address = "London",
                Department = "IT"
            };

            EmployeeDTO empDTO = Mapper.Map<EmployeeDTO>(emp);

            Console.WriteLine($"Name: {empDTO.Name}, Salary: {empDTO.Salary}, Address: {empDTO.Address}, Department: {empDTO.Department}");
            Console.ReadLine();
        }
    }
}
```

In this example, AutoMapper automatically maps properties from the `Employee` object to the `EmployeeDTO` object, saving us manual coding effort.

### Advantages of AutoMapper:
- Reduces boilerplate code.
- Simplifies object-to-object mapping.
- Improves maintainability and readability.

Remember, AutoMapper is a powerful tool for handling object mapping in C#. 🚀

Source: Conversation with Bing, 4/7/2024
(1) AutoMapper in C# with Examples - Dot Net Tutorials. https://dotnettutorials.net/lesson/automapper-in-c-sharp/.
(2) What is Automapper in C# and How to Use It | Simplilearn. https://www.simplilearn.com/tutorials/asp-dot-net-tutorial/automapper-in-c-sharp.
(3) c# - Simple Automapper Example - Stack Overflow. https://stackoverflow.com/questions/20635534/simple-automapper-example.
(4) Getting Started Guide — AutoMapper documentation. https://docs.automapper.org/en/stable/Getting-started.html.
(5) github.com. https://github.com/Nohovich/.NET/tree/4e7b6115d25b3232f90103c8115f050382956e3a/Framework-AutoMapperExample-master%2FAutoMapperExample%2FEmployee.cs.
(6) github.com. https://github.com/manojsdeveloper/my-dev.to/tree/ced23fb0d54d12dc81bc68fe5d6032770f97759f/blog-posts%2Fc%23%2Fautomapper.md.
(7) github.com. https://github.com/HongLienLe/AutoMapperDemo/tree/ca8b0da2f26c48389683cd4d2d7cbddb28d571c8/AutoMapperDemo%2FModel%2FEmployee.cs.


### How do Map Properties when the Property Names are Different using AutoMapper?
If the Property names differ in Source and Destination types, then you can map such properties in AutoMapper using the ForMember option. So, to Map the Name property of the Source Object with the FullName property of the Destination Object and the Department property of the Source Object with the Dept property of the Destination Object, we need to provide mapping configuration for these two properties in the mapping configuration. So, modify the MapperConfig class file to provide such property mapping configurations. In our upcoming articles, we will discuss the ForMember and MapForm options in detail.

```
namespace AutoMapperDemo
{
    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }
}
```

```
namespace AutoMapperDemo
{
    public class EmployeeDTO
    {
        public string FullName { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public string Dept { get; set; }
    }
}
```

```
using AutoMapper;
namespace AutoMapperDemo
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<Employee, EmployeeDTO>()
                //Provide Mapping Configuration of FullName and Name Property
                .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name))
                
                //Provide Mapping Dept of FullName and Department Property
                .ForMember(dest => dest.Dept, act => act.MapFrom(src => src.Department));
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
```

With the above changes in place, now run the application, and you should see the output as expected.


<a id="autofac"></a>
## Autofac

Certainly! Let's break it down into simpler terms:

1. **What Is Autofac?**
   - Imagine you're building a house. You need different materials like bricks, wood, and nails. Autofac is like a magical toolbox that helps you manage these materials (dependencies) in your software.
   - It keeps track of what you need and provides it when you ask. So, instead of manually fetching each brick or nail, Autofac does it for you.

2. **Why Use Autofac?**
   - **Dependency Injection**: Autofac helps you inject (provide) the right materials (dependencies) into your classes automatically.
   - **Modularity**: It encourages breaking your code into smaller pieces (modules) that fit together neatly.
   - **Testing**: With Autofac, you can easily swap real materials with fake ones during testing.

3. **Examples**:
   - **Without Autofac (Manual Way)**:
     - Imagine you're building a `UserService` that needs an `ILogger` (a tool to log messages).
     - Manually, you'd create an `ILogger` and give it to `UserService`.
   - **With Autofac**:
     - You tell Autofac, "Hey, I need an `ILogger`!" It magically provides one.
     - Your `UserService` doesn't worry about how to get an `ILogger`.
     - Autofac handles it behind the scenes.

4. **Benefits**:
   - **Cleaner Code**: Autofac simplifies your code. You focus on building, not fetching materials.
   - **Flexibility**: Swap materials easily (change implementations) without breaking things.
   - **Less Boilerplate**: No more manual dependency management.

Remember, Autofac is like your software's helpful toolbox. It ensures everything fits together smoothly! 🛠️

Certainly! Let's explore both the **manual way** (without Autofac) and the **Autofac way** (using Autofac) for managing dependencies in C# applications.

### **Manual Dependency Management (Without Autofac)**

In this approach, you manually create instances of your dependencies and manage them yourself. Let's consider an example where we have an `EmployeeService` that depends on an `ILogger`:

```csharp
using System;

public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

public class EmployeeService
{
    private readonly ILogger _logger;

    public EmployeeService(ILogger logger)
    {
        _logger = logger;
    }

    public void PrintEmployeeDetails(string employeeName)
    {
        _logger.Log($"Employee details: {employeeName}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var logger = new ConsoleLogger(); // Manually create an instance of ILogger
        var employeeService = new EmployeeService(logger); // Manually inject the ILogger dependency

        var employee = new Employee
        {
            EmployeeId = 1,
            FirstName = "Peter",
            LastName = "Parker",
            Designation = "Photographer"
        };

        employeeService.PrintEmployeeDetails($"{employee.FirstName} {employee.LastName}");
    }
}
```

### **Autofac (Dependency Injection Made Easy)**

Now let's see how we can achieve the same using Autofac:

1. **Install Autofac via NuGet**:
   - Add the Autofac NuGet package to your project.

2. **Register Dependencies with Autofac**:
   - Use the `ContainerBuilder` to register your dependencies.
   - Autofac will automatically resolve dependencies when needed.

```csharp
using System;
using Autofac;

public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

public class EmployeeService
{
    private readonly ILogger _logger;

    public EmployeeService(ILogger logger)
    {
        _logger = logger;
    }

    public void PrintEmployeeDetails(string employeeName)
    {
        _logger.Log($"Employee details: {employeeName}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<ConsoleLogger>().As<ILogger>(); // Register ILogger
        builder.RegisterType<EmployeeService>(); // Register EmployeeService

        var container = builder.Build();
        var employeeService = container.Resolve<EmployeeService>(); // Autofac resolves dependencies

        var employee = new Employee
        {
            EmployeeId = 1,
            FirstName = "Peter",
            LastName = "Parker",
            Designation = "Photographer"
        };

        employeeService.PrintEmployeeDetails($"{employee.FirstName} {employee.LastName}");
    }
}
```

With Autofac, you don't need to manually create instances or worry about injecting dependencies. It handles everything behind the scenes, making your code cleaner and more maintainable! 🚀

For more examples and detailed documentation, check out the [official Autofac documentation](https://docs.autofac.org/en/latest/).⁷⁴


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

<a id="dependency_injection"></a>
## Dependency Injection

Dependency Injection (DI) is a design pattern used to implement IoC. It allows the creation of dependent objects outside of a class and provides those objects to a class through different ways. Using DI, we move the creation and binding of the dependent objects outside of the class that depends on them.

The Dependency Injection pattern involves 3 types of classes.

1. Client Class: The client class (dependent class) is a class which depends on the service class
2. Service Class: The service class (dependency) is a class that provides service to the client class.
3. Injector Class: The injector class injects the service class object into the client class.

The following figure illustrates the relationship between these classes:

![](https://www.tutorialsteacher.com/Content/images/ioc/DI.png)

As you can see, the injector class creates an object of the service class, and injects that object to a client object. In this way, the DI pattern separates the responsibility of creating an object of the service class out of the client class.

### Types of Dependency Injection
As you have seen above, the injector class injects the service (dependency) to the client (dependent). The injector class injects dependencies broadly in three ways: through a constructor, through a property, or through a method.

* Constructor Injection: In the constructor injection, the injector supplies the service (dependency) through the client class constructor.

* Property Injection: In the property injection (aka the Setter Injection), the injector supplies the dependency through a public property of the client class.

* Method Injection: In this type of injection, the client class implements an interface which declares the method(s) to supply the dependency and the injector uses this interface to supply the dependency to the client class.

Let's take an example from the previous chapter to maintain the continuity. In the previous section of DIP, we used Factory class inside the CustomerBusinessLogic class to get an object of the CustomerDataAccess object, as shown below.

```
public interface ICustomerDataAccess
{
    string GetCustomerName(int id);
}

public class CustomerDataAccess: ICustomerDataAccess
{
    public CustomerDataAccess() {
    }

    public string GetCustomerName(int id) {
        return "Dummy Customer Name";        
    }
}

public class DataAccessFactory
{
    public static ICustomerDataAccess GetCustomerDataAccessObj() 
    {
        return new CustomerDataAccess();
    }
}

public class CustomerBusinessLogic
{
    ICustomerDataAccess _custDataAccess;

    public CustomerBusinessLogic()
    {
        _custDataAccess = DataAccessFactory.GetCustomerDataAccessObj();
    }

    public string GetCustomerName(int id)
    {
        return _custDataAccess.GetCustomerName(id);
    }
}
```

The problem with the above example is that we used DataAccessFactory inside the CustomerBusinessLogic class. So, suppose there is another implementation of ICustomerDataAccess and we want to use that new class inside CustomerBusinessLogic. Then, we need to change the source code of the CustomerBusinessLogic class as well. The Dependency injection pattern solves this problem by injecting dependent objects via a constructor, a property, or an interface.

The following figure illustrates the DI pattern implementation for the above example.

![](https://www.tutorialsteacher.com/Content/images/ioc/DI-example.png)

As you see, the CustomerService class becomes the injector class, which sets an object of the service class (CustomerDataAccess) to the client class (CustomerBusinessLogic) either through a constructor, a property, or a method to achieve loose coupling. Let's explore each of these options.

### Constructor Injection
As mentioned before, when we provide the dependency through the constructor, this is called a constructor injection.

Consider the following example where we have implemented DI using the constructor.

```Example: Constructor Injection 
public class CustomerBusinessLogic
{
    ICustomerDataAccess _dataAccess;

    public CustomerBusinessLogic(ICustomerDataAccess custDataAccess)
    {
        _dataAccess = custDataAccess;
    }

    public CustomerBusinessLogic()
    {
        _dataAccess = new CustomerDataAccess();
    }

    public string ProcessCustomerData(int id)
    {
        return _dataAccess.GetCustomerName(id);
    }
}

public interface ICustomerDataAccess
{
    string GetCustomerName(int id);
}

public class CustomerDataAccess: ICustomerDataAccess
{
    public CustomerDataAccess()
    {
    }

    public string GetCustomerName(int id) 
    {
        //get the customer name from the db in real application        
        return "Dummy Customer Name"; 
    }
}
```

In the above example, CustomerBusinessLogic includes the constructor with one parameter of type ICustomerDataAccess. Now, the calling class must inject an object of ICustomerDataAccess.

```Example: Inject Dependency
public class CustomerService
{
    CustomerBusinessLogic _customerBL;

    public CustomerService()
    {
        _customerBL = new CustomerBusinessLogic(new CustomerDataAccess());
    }

    public string GetCustomerName(int id) {
        return _customerBL.ProcessCustomerData(id);
    }
}
```

As you can see in the above example, the CustomerService class creates and injects the CustomerDataAccess object into the CustomerBusinessLogic class. Thus, the CustomerBusinessLogic class doesn't need to create an object of CustomerDataAccess using the new keyword or using factory class. The calling class (CustomerService) creates and sets the appropriate DataAccess class to the CustomerBusinessLogic class. In this way, the CustomerBusinessLogic and CustomerDataAccess classes become "more" loosely coupled classes.

### Property Injection
In the property injection, the dependency is provided through a public property. Consider the following example.

```Example: Property Injection
public class CustomerBusinessLogic
{
    public CustomerBusinessLogic()
    {
    }

    public string GetCustomerName(int id)
    {
        return DataAccess.GetCustomerName(id);
    }

    public ICustomerDataAccess DataAccess { get; set; }
}

public class CustomerService
{
    CustomerBusinessLogic _customerBL;

    public CustomerService()
    {
        _customerBL = new CustomerBusinessLogic();
        _customerBL.DataAccess = new CustomerDataAccess();
    }

    public string GetCustomerName(int id) {
        return _customerBL.GetCustomerName(id);
    }
}
```

As you can see above, the CustomerBusinessLogic class includes the public property named DataAccess, where you can set an instance of a class that implements ICustomerDataAccess. So, CustomerService class creates and sets CustomerDataAccess class using this public property.

### Method Injection
In the method injection, dependencies are provided through methods. This method can be a class method or an interface method.

The following example demonstrates the method injection using an interface based method.

```Example: Interface Injection
interface IDataAccessDependency
{
    void SetDependency(ICustomerDataAccess customerDataAccess);
}

public class CustomerBusinessLogic : IDataAccessDependency
{
    ICustomerDataAccess _dataAccess;

    public CustomerBusinessLogic()
    {
    }

    public string GetCustomerName(int id)
    {
        return _dataAccess.GetCustomerName(id);
    }
        
    public void SetDependency(ICustomerDataAccess customerDataAccess)
    {
        _dataAccess = customerDataAccess;
    }
}

public class CustomerService
{
    CustomerBusinessLogic _customerBL;

    public CustomerService()
    {
        _customerBL = new CustomerBusinessLogic();
        ((IDataAccessDependency)_customerBL).SetDependency(new CustomerDataAccess());
    }

    public string GetCustomerName(int id) {
        return _customerBL.GetCustomerName(id);
    }
}
```

In the above example, the CustomerBusinessLogic class implements the IDataAccessDependency interface, which includes the SetDependency() method. So, the injector class CustomerService will now use this method to inject the dependent class (CustomerDataAccess) to the client class.


<a id="asp_core"></a>
# ASP Core

Show list of template
```
dotnet new list
```

For Create Solution File
```
dotnet new sln
```

For Create Webapi
```
dotnet new webapi -n NameOfProject
```

dotnet sln -h:

Commands:
* add <PROJECT_PATH>     Add one or more projects to a solution file.
* list                   List all projects in a solution file.
* remove <PROJECT_PATH>  Remove one or more projects from a solution file.

dotnet add project to solution
```
dotnet sln add <PROJECT_PATH>     // Add one or more projects to a solution file.
```

for run dotnet project
```
dotnet run
```

for run dotnet project with hotreload
```
dotnet watch
```


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

<a id="migration"></a>
## Migration
In the Working with DbContext chapter, we used the context.Database.EnsureCreated() method to create the database and schema for the first time. Note that it creates a database first time only. It cannot change the DB schema after that. For the devlopement projects, we must use EF Core Migrations API.

To use EF Core Migrations API, we need to install the NuGet package Microsoft.EntityFrameworkCore.Tools. We use EF Core 7.0.11, so install the same version of package.

EF Core provides migrations commands to create, update, or remove tables and other DB objects based on the entities and configurations. At this point, there is no SchoolDB database. So, we need to create the database from the model (entities and configurations) by adding a migration.

If you use .NET Core CLI, then enter the following command.

```
dotnet ef migrations add InitialSchoolDB
```

This will create a new folder named Migrations in the project and create the ModelSnapshot files, as shown below.

![ef](https://www.entityframeworktutorial.net/images/efcore/migration2.png)

The Add-Migration command does not create the database. It just creates the two snapshot files in the Migrations folder.

1. \<timestamp>_\<Migration Name>.cs: The main migration file which includes migration operations in the Up() and Down() methods. The Up() method includes the code for creating DB objects and the Down() method includes code for removing DB objects.
2. \<contextclassname>ModelSnapshot.cs: A snapshot of your current model. This is used to determine what changed when creating the next migration.

Use the following command in .NET Core CLI to create a database.

```
dotnet ef database update
```

The following executes the update-database command and creates the database, as shown below. The -verbose option shows the logs while creating the database. It creates a database with the name and location specified in the connection string in the UseSqlServer() method. It creates a table for each entity, Student and Grade). It also creates _EFMigrationHistory table that stores history of migrations applied overtime.

### Apply Migrations for Modified Entities/Configurations
Suppose we add some new entities or modify an existing entities or changed any configuration, then we again need to execute the **add-migration** and **update-migration** commands to apply changes to the database schema

### Reverting Migration
For some reason, if you want to revert the database to any of the previous state then you can do it by using the **update-database \<migration-name>** command.

For example, we modified the Student entity and added some more properties. But now we want to revert it back to same as "InitialSchoolDB" migration. We can do it by using the following command:

Package Manager Console/PowerShell
```
Update-database "InitialSchoolDB"
```

.NET Core CLI
```
dotnet ef database update "InitialSchoolDB".
```

### List All Migrations
Use the following migration command to get the list of all migrations.
```
Get-Migration
```

.NET Core CLI
```
dotnet ef migrations list
```

### Removing a Migration
Above, we have reverted the second migration named "ModifiedStudentEntity". We can remove the last migration if it is not applied to the database. Let's remove the Use the "ModifiedStudentEntity" file using the following remove commands.

Package Manager Console/PowerShell
```
remove-migration
```

.NET Core CLI
```
dotnet ef migrations remove
```

The above commands will remove the last migration and revert the model snapshot to the previous migration, as shown below. Please note that if a migration is already applied to the database, then it will throw the exception.

<a id="saving_data"></a>
## Saving Data

Entity Framework Core provides different ways to add, update, or delete data in the underlying database. An entity contains data in its scalar property will be either inserted or updated or deleted based on its EntityState.

There are two scenarios to save an entity data: connected and disconnected. In the connected scenario, the same instance of DbContext is used in retrieving and saving entities, whereas this is different in the disconnected scenario. In this chapter, you will learn about saving data in the connected scenario.

The following figure illustrates the CUD (Create, Update, Delete) operation in the connected scenario.

![Database](https://www.entityframeworktutorial.net/images/efcore/save-data-in-connected-scenario.png)

As per the above figure, Entity Framework builds and executes INSERT, UPDATE, or DELETE statements for the entities whose EntityState is Added, Modified, or Deleted when the DbContext.SaveChanges() method is called. In the connected scenario, an instance of DbContext keeps track of all the entities and so it automatically sets an appropriate EntityState of each entity whenever an entity is created, modified, or deleted.

### Insert Data
The DbSet.Add and DbContext.Add methods add a new entity to a context (instance of DbContext) which will insert a new record in the database when you call the SaveChanges() method.

```
using (var context = new SchoolContext())
{
    var std = new Student()
    {
        FirstName = "Bill",
        LastName = "Gates"
    };
    context.Students.Add(std);

    // or
    // context.Add<Student>(std);

    context.SaveChanges();
}
```

In the above example, context.Students.Add(std) adds a newly created instance of the Student entity to a context with Added EntityState. EF Core introduced the new DbContext.Add method, which does the same thing as the DbSet.Add method. After this, the SaveChanges() method builds and executes the following INSERT statement to the database.

```
exec sp_executesql N'SET NOCOUNT ON;
INSERT INTO [Students] ( [FirstName], [LastName])
VALUES (@p0, @p1);
SELECT [StudentId]
FROM [Students]
WHERE @@ROWCOUNT = 1 AND [StudentId] = scope_identity();',N
'@p0 nvarchar(4000), @p1 nvarchar(4000) ',@p0=N'Bill',@p1=N'Gates'
go
```

### Updating Data
In the connected scenario, EF Core API keeps track of all the entities retrieved using a context. Therefore, when you edit entity data, EF automatically marks EntityState to Modified, which results in an updated statement in the database when you call the SaveChanges() method.

```
using (var context = new SchoolContext())
{
    var std = context.Students.First<Student>(); 
    std.FirstName = "Steve";
    context.SaveChanges();
}
```

In the above example, we retrieve the first student from the database using context.Students.First<student>(). As soon as we modify the FirstName, the context sets its EntityState to Modified because of the modification performed in the scope of the DbContext instance (context). So, when we call the SaveChanges() method, it builds and executes the following Update statement in the database.

```
exec sp_executesql N'SET NOCOUNT ON;
UPDATE [Students] SET [FirstName] = @p0
WHERE [StudentId] = @p1;
SELECT @@ROWCOUNT;
',N'@p1 int,@p0 nvarchar(4000)',@p1=1,@p0=N'Steve'
Go
```

### Deleting Data
Use the DbSet.Remove() or DbContext.Remove methods to delete a record in the database table.

```
using (var context = new SchoolContext())
{
    var std = context.Students.First<Student>();
    context.Students.Remove(std);

    // or
    // context.Remove<Student>(std);

    context.SaveChanges();
}
```

In the above example, context.Students.Remove(std) or context.Remove<Students>(std) marks the std entity object as Deleted. Therefore, EF Core will build and execute the following DELETE statement in the database.

```
exec sp_executesql N'SET NOCOUNT ON;
DELETE FROM [Students]
WHERE [StudentId] = @p0;
SELECT @@ROWCOUNT;
',N'@p0 int',@p0=1
Go
```

Thus, it is very easy to add, update, or delete data in Entity Framework Core in the connected scenario.

In an update statement, EF Core API includes the properties with modified values, the rest being ignored. In the above example, only the FirstName property was edited, so an update statement includes only the FirstName column.

<a id="query"></a>
## Query
EF Core has a new feature in LINQ-to-Entities where we can include C# or VB.NET functions in the query. This was not possible in EF 6.

```
private static void Main(string[] args)
{
    var context = new SchoolContext();
    var studentsWithSameName = context.Students
                                      .Where(s => s.FirstName == GetName())
                                      .ToList();
}

public static string GetName() {
    return "Bill";
}
```

In the above L2E query, we have included the GetName() C# function in the Where clause. This will execute the following query in the database:

```
exec sp_executesql N'SELECT [s].[StudentId], [s].[DoB], [s].[FirstName], 
    [s].[GradeId], [s].[LastName], [s].[MiddleName]
FROM [Students] AS [s]
WHERE [s].[FirstName] = @__GetName_0',N'@__GetName_0 nvarchar(4000)',
    @__GetName_0=N'Bill'
Go
```

### Eager Loading
Entity Framework Core supports eager loading of related entities, same as EF 6, using the Include() extension method and projection query. In addition to this, it also provides the ThenInclude() extension method to load multiple levels of related entities. (EF 6 does not support the ThenInclude() method.)

#### Include
Unlike EF 6, we can specify a lambda expression as a parameter in the Include() method to specify a navigation property as shown below.

```
var context = new SchoolContext();

var studentWithGrade = context.Students
                           .Where(s => s.FirstName == "Bill")
                           .Include(s => s.Grade)
                           .FirstOrDefault();
```

In the above example, .Include(s => s.Grade) passes the lambda expression s => s.Grade to specify a reference property to be loaded with Student entity data from the database in a single SQL query. The above query executes the following SQL query in the database.

```
SELECT TOP(1) [s].[StudentId], [s].[DoB], [s].[FirstName], [s].[GradeId],[s].[LastName], 
        [s].[MiddleName], [s.Grade].[GradeId], [s.Grade].[GradeName], [s.Grade].[Section]
FROM [Students] AS [s]
LEFT JOIN [Grades] AS [s.Grade] ON [s].[GradeId] = [s.Grade].[GradeId]
WHERE [s].[FirstName] = N'Bill'
```

We can also specify property name as a string in the Include() method, same as in EF 6.

```
var context = new SchoolContext();

var studentWithGrade = context.Students
                        .Where(s => s.FirstName == "Bill")
                        .Include("Grade")
                        .FirstOrDefault();

```

The example above is not recommended because it will throw a runtime exception if a property name is misspelled or does not exist. Always use the Include() method with a lambda expression, so that the error can be detected during compile time.

The Include() extension method can also be used after the FromSql() method, as shown below.

```
var context = new SchoolContext();

var studentWithGrade = context.Students
                        .FromSql("Select * from Students where FirstName ='Bill'")
                        .Include(s => s.Grade)
                        .FirstOrDefault();            
```

Note: The Include() extension method cannot be used after the DbSet.Find() method. E.g. context.Students.Find(1).Include() is not possible in EF Core 2.0. This may be possible in future versions.

### Multiple Include
Use the Include() method multiple times to load multiple navigation properties of the same entity. For example, the following code loads Grade and StudentCourses related entities of Student.

```
var context = new SchoolContext();

var studentWithGrade = context.Students.Where(s => s.FirstName == "Bill")
                        .Include(s => s.Grade)
                        .Include(s => s.StudentCourses)
                        .FirstOrDefault();
```

The above query will execute two SQL queries in a single database round trip.

```
SELECT TOP(1) [s].[StudentId], [s].[DoB], [s].[FirstName], [s].[GradeId], [s].[LastName], 
        [s].[MiddleName], [s.Grade].[GradeId], [s.Grade].[GradeName], [s.Grade].[Section]
FROM [Students] AS [s]
LEFT JOIN [Grades] AS [s.Grade] ON [s].[GradeId] = [s.Grade].[GradeId]
WHERE [s].[FirstName] = N'Bill'
ORDER BY [s].[StudentId]
Go

SELECT [s.StudentCourses].[StudentId], [s.StudentCourses].[CourseId]
FROM [StudentCourses] AS [s.StudentCourses]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT TOP(1) [s0].[StudentId]
        FROM [Students] AS [s0]
        LEFT JOIN [Grades] AS [s.Grade0] ON [s0].[GradeId] = [s.Grade0].[GradeId]
        WHERE [s0].[FirstName] = N'Bill'
        ORDER BY [s0].[StudentId]
    ) AS [t]
) AS [t0] ON [s.StudentCourses].[StudentId] = [t0].[StudentId]
ORDER BY [t0].[StudentId]
Go
```

### ThenInclude
EF Core introduced the new ThenInclude() extension method to load multiple levels of related entities. Consider the following example:

```
var context = new SchoolContext();

var student = context.Students.Where(s => s.FirstName == "Bill")
                        .Include(s => s.Grade)
                            .ThenInclude(g => g.Teachers)
                        .FirstOrDefault();
```

In the above example, .Include(s => s.Grade) will load the Grade reference navigation property of the Student entity. .ThenInclude(g => g.Teachers) will load the Teacher collection property of the Grade entity. The ThenInclude method must be called after the Include method. The above will execute the following SQL queries in the database.

```
SELECT TOP(1) [s].[StudentId], [s].[DoB], [s].[FirstName], [s].[GradeId], [s].[LastName],
         [s].[MiddleName], [s.Grade].[GradeId], [s.Grade].[GradeName], [s.Grade].[Section]
FROM [Students] AS [s]
LEFT JOIN [Grades] AS [s.Grade] ON [s].[GradeId] = [s.Grade].[GradeId]
WHERE [s].[FirstName] = N'Bill'
ORDER BY [s.Grade].[GradeId]
Go

SELECT [s.Grade.Teachers].[TeacherId], [s.Grade.Teachers].[GradeId], [s.Grade.Teachers].[Name]
FROM [Teachers] AS [s.Grade.Teachers]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT TOP(1) [s.Grade0].[GradeId]
        FROM [Students] AS [s0]
        LEFT JOIN [Grades] AS [s.Grade0] ON [s0].[GradeId] = [s.Grade0].[GradeId]
        WHERE [s0].[FirstName] = N'Bill'
        ORDER BY [s.Grade0].[GradeId]
    ) AS [t]
) AS [t0] ON [s.Grade.Teachers].[GradeId] = [t0].[GradeId]
ORDER BY [t0].[GradeId]
go
```

### Projection Query
We can also load multiple related entities by using the projection query instead of Include() or ThenInclude() methods. The following example demonstrates the projection query to load the Student, Grade, and Teacher entities.

```
var context = new SchoolContext();

var stud = context.Students.Where(s => s.FirstName == "Bill")
                        .Select(s => new
                        {
                            Student = s,
                            Grade = s.Grade,
                            GradeTeachers = s.Grade.Teachers
                        })
                        .FirstOrDefault();
```

In the above example, the .Select extension method is used to include the Student, Grade and Teacher entities in the result. This will execute the same SQL query as the above ThenInclude() method.

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

### Composite Primary Key
By using the .HasKey() method, a set of properties can be explicitly configured as the composite primary key of the entity.

```
using System.Data.Entity;    
// ..

public class PersonContext : DbContext
{
    // ..

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // ..

        modelBuilder.Entity<Person>().HasKey(p => new { p.FirstName, p.LastName });
    }
}
```

Another Example
```
public class Category
{
    public int CategoryId1 { get; set; }
    public int CategoryId2 { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int CategoryId1 { get; set; }
    public int CategoryId2 { get; set; }

    public virtual Category Category { get; set; }
}

public class Context : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasKey(c => new {c.CategoryId1, c.CategoryId2});

        modelBuilder.Entity<Product>()
            .HasRequired(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => new {p.CategoryId1, p.CategoryId2});

    }
}
```

Another Example

Student:
```
public class Student
{
    public int StudentId {get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public Teacher Teacher { get; set; }   

    public override string ToString()
    {
        return $"Student : {FirstName} {LastName}";
    }
}
```

Teacher:
```
[PrimaryKey(nameof(FirstName), nameof(LastName))]
public class Teacher
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public List<Student> Students { get; set; }
    public override string ToString()
    {
        return $"Teacher : {FirstName} {LastName}";
    }
}
```

Main:
```
using (var context = new SchoolDbContext(configuration))
{
    //creates db if not exists 
    context.Database.EnsureCreated();

    //create entity objects
    var tech = new Teacher() { FirstName = "Tom", LastName = "Rider" };
    var std1 = new Student() { FirstName = "Harry", LastName = "Potter", Teacher = tech };
    var std2 = new Student() { FirstName = "Gholi", LastName = "Gholipor", Teacher = tech };

    context.Students.Add(std1); 
    context.Students.Add(std2); 

    //save data to the database tables
    context.SaveChanges();

    //retrieve all the students from the database
    foreach (var student in context.Students.Include(s => s.Teacher).ToList())
    {
        Console.WriteLine($"{student.Teacher} {student} ");
    }

    //retrieve all the teachers from the database
    foreach (var teacher in context.Teachers.Include(t => t.Students).ToList())
    {
        var students = teacher.Students; 
        foreach (var student in students)
        {
            Console.WriteLine($"{teacher} {student}");
        }
    }
}
```

OutPut:
```
Teacher : Tom Rider Student : Harry Potter
Teacher : Tom Rider Student : Gholi Gholipor
```

#### Student Table
| StudentId  | FirstName | LastName | TeacherFirstName | TeacherLastName |
| ------------- | ------------- | ------------- | ------------- | ------------- | 
| 1  | Harry | Potter | Tom | Rider |
| 2  | Gholi | Gholipor | Tom | Rider |


#### Teacher Table
| FirstName  | LastName |
| ------------- | ------------- |
| Tom | Rider |


### Explicitly configuring value generation
We saw above that EF Core automatically sets up value generation for primary keys - but we may want to do the same for non-key properties. You can configure any property to have its value generated for inserted entities as follows:

```
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime Inserted { get; set; }
}
```

Similarly, a property can be configured to have its value generated on add or update:

```
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LastUpdated { get; set; }
}
```

I have Id and No properties in my entity. Id is primary key but No isn't. I need auto-increment for No too.

```
[Key]
public int Id {get; set;}

[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int No {get; set;
```

Annotate the property like below
```
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int ID { get; set; }
```

### GUID as Key
GUID is handled by Identity framework and not by EF, so to use GUID you need to do as the following

```
[Key]
public Guid Id { get; set; }
```

and then prepare the model before insert into table
```
new User() { Id = Guid.NewGuid(), Email = "Test@gmail.com", Name = "Test", Password = "123123" },
```

#### Generating GUIDs Automatically with Entity Framework
Entity Framework allows us to automatically generate GUIDs for specific properties using the DatabaseGenerated attribute combined with the Guid data type.

```
public class MyEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
 
    // Other properties...
}
```

In the example above, the Id property serves as the primary key and will be manually assigned a GUID, while the AutoGeneratedGuid property will be automatically assigned a new GUID by the database when inserting a new record.


## Install Package in DotNet
```
dotnet add package <packageName>
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

Install EF Core Using .NET CLI
To install EF Core using the .NET CLI (Command Line Interface), open your terminal or command prompt and navigate to your project directory. This should be the directory that contains your project's .csproj file.

Install the EF Core package for SQL Server using the following command.
```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.11
```

## Adding Package Reference in .csproj
You can simply add the package reference in your .csproj file. Double-click on your project name in Visual Studio to open .csproj file.

Now, add an ItemGroup element after PropertyGroup. In the ItemGroup element, add a PackageReference element and specify the PackageId as "Microsoft.EntityFrameworkCore.SqlServer" and specify the Version attribute to the desired version, in this case "7.0.11", as shown below.

```
<ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.11" />
</ItemGroup>
```

## Database Connection String in Entity Framework Core
Here you will learn the formats of connection strings and the ways to use them in the Entity Framework Core 6/7 application.

## Database Connection String Formats
The most common format of a connection string in EF Core is:

```
Server={server_address};Database={database_name};UserId={username};Password={password};
```

Replace {server_address}, {database_name}, {username}, and {password} with your specific database credentials. For example, the following connection string is for the local database "SchoolDB":

```
"Server=(localdb)\\mssqllocaldb;Database=SchoolDb;UserId=myuserid;Passwprd=mypwd;"
```

you need to install Microsoft.Extensions.Configuration and Microsoft.Extensions.Configuration.Json NuGet package to your project.

After installing the package, you need to build the configuration by adding appsettings.json file, as shown below

```
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
```

You also need to add a constructor which accepts the IConfiguration object, as shown below.

```
public class SchoolContext : DbContext
{       
     IConfiguration appConfig;

     public SchoolDbContext(IConfiguration config)
     {
         appConfig = config;
     }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(appConfig.GetConnectionString("SchoolDBLocalConnection");
    }
} 
```
you can pass the configuration when you create an object of DbContext, as shown below:

```
using (var context = new SchoolDbContext(configuration))
{

}
```


Teacher Model
```
public class Teacher
{
    public int TeacherId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public List<Student> Students { get; set; }
    public override string ToString()
    {
        return $"Teacher : {FirstName} {LastName}";
    }
}
```

Student Model
```
public class Student
{
    public int StudentId {get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public override string ToString()
    {
        return $"Student : {FirstName} {LastName}";
    }
}
```

retrieve all the Student from the database
```
foreach (var student in context.Students.Include(Student => Student.Teacher).ToList())
{
    Console.WriteLine($"{student} {student.Teacher}");
}
```


retrieve all the Teacher from the database
```
foreach (var teacher in context.Teachers.Include(Teacher => Teacher.Students).ToList())
{
    var students = teacher.Students;;
    foreach(var student in students)
    {
        Console.WriteLine($"{teacher} {student}");
    }
}
```

<a id="pk_fk"></a>
## Difference between Primary Key and Foreign Key

### What is a Primary Key?
A primary key is a column (or set of columns) in a table that uniquely identifies each row in the table. It cannot contain null values and must be unique across all rows in the table. Only one primary key is allowed in a table.

A primary key is basically a combination of the 'UNIQUE' and 'Not Null' constraints. Thus, it cannot be a NULL value. Another important point to be noted about primary key is that its value cannot be deleted from the parent table.

### What is a Foreign Key?
A Foreign Key is a Key that refers to the Unique Key or Primary Key of another table or A foreign key is a column (or set of columns) in a table that refers to the primary key in another table. It is used to establish a link between the two tables and is used to enforce referential integrity in the database. Foreign key is basically the field/column in a table that is analogous to the primary key of other table.

Unlike a primary key, a table can have more than one foreign key. Also, the foreign key can contain duplicate and null values in a relational database. The value of a foreign key can be deleted from the child table.
Primary key (PK) - value which uniquely identifies every row in the table.
Foreign keys (FK) - values match a primary or alternate key inherited from some other table

Example: 
* Primary Key : 
    STUD_NO, as well as STUD_PHONE both, are candidate keys for relation STUDENT but STUD_NO can be chosen as the primary key (only one out of many candidate keys).

* Foreign keys : 
    STUD_NO in STUDENT_COURSE is a foreign key to STUD_NO in STUDENT relation


<a id="relationships"></a>
## Relationships

One-to-many relationships are used when a single entity is associated with any number of other entities. For example, a Blog can have many associated Posts, but each Post is associated with only one Blog.

Required one-to-many
```
// Principal (parent)
public class Blog
{
    public int Id { get; set; }
    public ICollection<Post> Posts { get; } = new List<Post>(); // Collection navigation containing dependents
}

// Dependent (child)
public class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; } // Required foreign key property
    public Blog Blog { get; set; } = null!; // Required reference navigation to principal
}
```

A one-to-many relationship is made up from:

* One or more primary or alternate key properties on the principal entity; that is the "one" end of the relationship. For example, Blog.Id.
* One or more foreign key properties on the dependent entity; that is the "many" end of the relationship. For example, Post.BlogId.
* Optionally, a collection navigation on the principal entity referencing the dependent entities. For example, Blog.Posts.
* Optionally, a reference navigation on the dependent entity referencing the principal entity. For example, Post.Blog.

### One-to-One Relationship
A one-to-one relationship exists when a single entity is related to exactly one other entity. For instance, in our blogging application, an author might have a single bio. To define a one-to-one relationship, we can modify the Author class:

```
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int BioId { get; set; }
    public Bio Bio { get; set; }
}

public class Bio
{
    public int Id { get; set; }
    public string Content { get; set; }

    public Author Author { get; set; }
}
```

Here, the Author class now has a BioId property that serves as a foreign key to the Bio entity. The Bio entity, in turn, has a reference to its corresponding Author. This establishes a one-to-one relationship between authors and their bios.

### One-to-Many Relationship
A one-to-many relationship exists when a single entity is related to multiple instances of another entity. In our blogging application, an author can have multiple posts. Let’s update the classes to reflect this relationship:

```
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Post> Posts { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }
}
```

In this case, the Author class includes a collection of Post entities, forming a one-to-many relationship between authors and their posts.

### Many-to-Many Relationship
A many-to-many relationship arises when multiple instances of one entity are associated with multiple instances of another entity. In our blogging application, authors can have many tags associated with their posts, and tags can be associated with multiple posts. To represent this relationship, we’ll introduce a Tag entity:

```
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Post> Posts { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Tag> Tags { get; set; }
}

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Post> Posts { get; set; }
}
```

In this scenario, both the Author and Tag entities have many-to-many relationships with the Post entity through their respective collections of posts.


Let us first understand what is Principal Entity and Dependent Entity from the relational database point of view.

* Principal Entity: The Entity containing the Primary or Unique Key properties is called Principal Entity.
* Dependent Entity: The Entity which contains the Foreign key properties is called Dependent Entity.

let us first understand a few important terms used in database and model classes.

* Primary Key: The Primary key is a column (columns in the case of composite primary key) in the database Table that Uniquely identifies each row.
* Foreign Key: The Foreign key is a column in a table that makes a relationship with another table. The Foreign key Column should point to the Principal Entity’s Primary Key or Unique Key column.
* Navigation Properties: In .NET, the Navigation properties define the type of relationships between the Entities. Based on the requirements, these properties are defined either in the Principal Entity or in the Dependent Entity.

### One-to-Many Relationship Conventions
Let's look at the different conventions which automatically configure a one-to-many relationship between the following Student and Grade entities.

```
public class Student
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
}
       
public class Grade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public string Section { get; set; }
}
```

After applying the conventions for one-to-many relationship in the entities above, the database tables for Student and Grade entities will look like below, where the Students table includes a foreign key GradeId.

#### Convention 1
We want to establish a one-to-many relationship where many students are associated with one grade. This can be achieved by including a reference navigation property in the dependent entity as shown below. (here, the Student entity is the dependent entity and the Grade entity is the principal entity).

```
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
   
    public Grade Grade { get; set; }
}

public class Grade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public string Section { get; set; }
}
```

In the example above, the Student entity class includes a reference navigation property of Grade type. This allows us to link the same Grade to many different Student entities, which creates a one-to-many relationship between them. This will produce a one-to-many relationship between the Students and Grades tables in the database, where Students table includes a nullable foreign key GradeId, as shown below. EF Core will create a shadow property for the foreign key named GradeId in the conceptual model, which will be mapped to the GradeId foreign key column in the Students table.

![linq](https://www.entityframeworktutorial.net/images/efcore/onetomany-conventions1.png)

Note: The reference property Grade is nullable, so it creates a nullable ForeignKey GradeId in the Students table. You can configure NotNull foreign keys using fluent API.

#### Convention 2
Another convention is to include a collection navigation property in the principal entity as shown below.
```
public class Student
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
}

public class Grade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public string Section { get; set; }

    public ICollection<Student> Students { get; set; } 
}
```

In the example above, the Grade entity includes a collection navigation property of type ICollection<student>. This will allow us to add multiple Student entities to a Grade entity, which results in a one-to-many relationship between Students and Grades tables in the database, same as in convention 1.

#### Convention 3
Another EF convention for the one-to-many relationship is to include navigation property at both ends, which will also result in a one-to-many relationship (convention 1 + convention 2).

```
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public Grade Grade { get; set; }
}

public class Grade
{
    public int GradeID { get; set; }
    public string GradeName { get; set; }
    
    public ICollection<Student> Students { get; set; }
}
```

In the example above, the Student entity includes a reference navigation property of Grade type and the Grade entity class includes a collection navigation property ICollection<Student>, which results in a one-to-many relationship between corresponding database tables Students and Grades, same as in convention 1.

#### Convention 4
Defining the relationship fully at both ends with the foreign key property in the dependent entity creates a one-to-many relationship.

```
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int GradeId { get; set; }
    public Grade Grade { get; set; }
}

public class Grade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }

    public ICollection<Student> Students { get; set; }
}
```

In the above example, the Student entity includes a foreign key property GradeId of type int and its reference navigation property Grade. At the other end, the Grade entity also includes a collection navigation property ICollection<Student>. This will create a one-to-many relationship with the NotNull foreign key column in the Students table, as shown below.


![linq](https://www.entityframeworktutorial.net/images/efcore/onetomany-conventions2.png)

If you want to make the foreign key GradeId as nullable, then use nullable int data type (Nullable<int> or int?), as shown below.

```
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int? GradeId { get; set; } 
    public Grade Grade { get; set; }
}
```

Therefore, these are the conventions which automatically create a one-to-many relationship in the corresponding database tables. If entities do not follow the above conventions, then you can use Fluent API to configure the one-to-many relationship.

<a id="soft_delete"></a>
## Soft Delete

### What is a soft delete?
A soft delete marks a record as no longer active or valid without actually deleting it from the database. Soft deletes can improve performance, and can allow “deleted” data to be recovered.

Soft delete is a technique used in databases to mark records as deleted without physically removing them from the database. Instead of permanently deleting data, a flag or a separate column is used to indicate that a record is "deleted" or no longer active. This approach allows for the possibility of later recovering or restoring the deleted data if need

### Why Soft Deletes?
#### Pros
The main reason to implement soft deletes is to prevent data loss. Your application isn't physically deleting data, just marking it deleted. Thus, with some work you can recover any soft delete.

#### Cons
The main downside of soft deletes is complexity. Your database is now more error prone. For instance, wherever you are querying the data you now must remember to filter the data by something like where is_deleted=1. This may be fine in the application context but it's often overlooked by data analysts working in the analytical context.

Soft deletes also cause issues with foreign keys. If you rely on foreign keys to ensure referential integrity in your database, some rules are invalid with soft deletes.

Lastly, you need more storage. If you don't delete any data your database will be bigger.

### Set IsDeleted  
Instead of physically removing a record, a soft delete marks it as deleted, usually by setting a flag like IsDeleted to true. The record remains in the database, but it's effectively hidden from regular application queries.

### OnModelCreating
This method is called when the model for a derived context has been initialized, but before the model has been locked down and used to initialize the context. The default implementation of this method does nothing, but it can be overridden in a derived class such that the model can be further configured before it is locked down.

You can override the OnModelCreating method in your derived context and use the fluent API to configure your model. This is the most powerful method of configuration and allows configuration to be specified without modifying your entity classes. Fluent API configuration has the highest precedence and will override conventions and data annotations. The configuration is applied in the order the methods are called and if there are any conflicts the latest call will override previously specified configuration.

```
using Microsoft.EntityFrameworkCore;

namespace EFModeling.EntityProperties.FluentAPI.Required;

internal class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    #region Required
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property(b => b.Url)
            .IsRequired();
    }
    #endregion
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}
```


### Implement
implement **soft delete** for a `Student` model using **Entity Framework Core (EF Core)`:

1. **Create the `Student` Model**:
   - Assuming you have a `Student` class with properties like `Id`, `Name`, and `IsDeleted` (a boolean flag indicating whether the student is deleted or not):
     ```csharp
     public class Student
     {
         public int Id { get; set; }
         public string Name { get; set; }
         public bool IsDeleted { get; set; } // Soft delete flag
         // Other properties...
     }
     ```

2. **Add the `ISoftDeletable` Interface**:
   - Create an interface called `ISoftDeletable` with properties for soft deletion:
     ```csharp
     public interface ISoftDeletable
     {
         bool IsDeleted { get; set; }
         DateTime? DeletedAt { get; set; }
     }
     ```

3. **Configure the `DbContext`**:
   - In your `DbContext`, configure the soft delete filter for the `Student` entity:
     ```csharp
     public class SchoolDbContext : DbContext
     {
         // DbSet for Student
         public DbSet<Student> Students { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);

             // Configure soft delete filter for Student
             modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
         }

         // Other configurations...
     }
     ```

4. **Override `SaveChanges`**:
   - Override the `SaveChanges` method to handle soft deletion:
     ```csharp
     public class SchoolDbContext : DbContext
     {
         // ...

         public override int SaveChanges()
         {
             foreach (var entry in ChangeTracker.Entries<ISoftDeletable>())
             {
                 if (entry.State == EntityState.Deleted)
                 {
                     // Override removal to set soft delete properties
                     entry.State = EntityState.Modified;
                     entry.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
                     entry.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = DateTime.UtcNow;
                 }
             }
             return base.SaveChanges();
         }
     }
     ```

5. **Usage**:
   - When deleting a student, set the `IsDeleted` flag to true:
     ```csharp
     var studentToDelete = dbContext.Students.Find(studentId);
     if (studentToDelete != null)
     {
         studentToDelete.IsDeleted = true;
         dbContext.SaveChanges();
     }
     ```

6. **Querying**:
   - When querying students, the soft-deleted ones won't be included by default.
   - To include soft-deleted students, use `.IgnoreQueryFilters()`:
     ```csharp
     var activeStudents = dbContext.Students.ToList(); // Excludes soft-deleted students
     var allStudents = dbContext.Students.IgnoreQueryFilters().ToList(); // Includes soft-deleted students
     ```

That's it! You've implemented soft delete for the `Student` model. Adjust the code according to your actual implementation and business logic. 🌟

For more details, you can refer to the [official documentation](https://blog.jetbrains.com/dotnet/2023/06/14/how-to-implement-a-soft-delete-strategy-with-entity-framework-core/) and explore other community resources as well .


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

### Understanding API Pagination
API pagination refers to a technique used in API design and development to retrieve large data sets in a structured and manageable manner. When an API endpoint returns a large amount of data, pagination allows the data to be divided into smaller, more manageable chunks or pages. Each page contains a limited number of records or entries. The API consumer or client can then request subsequent pages to retrieve additional data until the entire dataset has been retrieved.

Pagination typically involves the use of parameters, such as offset and limit or cursor-based tokens, to control the size and position of the data subset to be retrieved. These parameters determine the starting point and the number of records to include on each page.

### Pagination
Most endpoints that returns a list of entities will need to have some sort of pagination.

Without pagination, a simple search could return millions or even billions of hits causing extraneous network traffic.

Paging requires an implied ordering. By default this may be the item’s unique identifier, but can be other ordered fields such as a created date.

### Common API Pagination Techniques
There are several common API pagination techniques that developers employ to implement efficient data retrieval. Here are a few commonly used techniques:

1. Offset and Limit Pagination
This technique involves using two parameters: offset and limit. The "offset" parameter determines the starting point or position in the dataset, while the "limit" parameter specifies the maximum number of records to include on each page.

For example, an API request could include parameters like "offset=0" and "limit=10" to retrieve the first 10 records.
```
GET /api/posts?offset=0&limit=10
```

### Offset Pagination
This is the simplest form of paging. Limit/Offset became popular with apps using SQL databases which already have LIMIT and OFFSET as part of the SQL SELECT Syntax. Very little business logic is required to implement Limit/Offset paging.

Limit/Offset Paging would look like GET /items?limit=20&offset=100. This query would return the 20 rows starting with the 100th row.

2. Cursor-Based Pagination
Instead of relying on numeric offsets, cursor-based pagination uses a unique identifier or token to mark the position in the dataset. The API consumer includes the cursor value in subsequent requests to fetch the next page of data.

This approach ensures stability when new data is added or existing data is modified. The cursor can be based on various criteria, such as a timestamp, a primary key, or an encoded representation of the record.

For example -
```
GET /api/posts?cursor=eyJpZCI6MX0
```
In the above API request, the cursor value eyJpZCI6MX0 represents the identifier of the last fetched record. This request retrieves the next page of posts after that specific cursor.

3. Page-Based Pagination
Page-based pagination involves using a "page" parameter to specify the desired page number. The API consumer requests a specific page of data, and the API responds with the corresponding page, typically along with metadata such as the total number of pages or total record count.

This technique simplifies navigation and is often combined with other parameters like "limit" to determine the number of records per page.

For example -
```
GET /api/posts?page=2&limit=20
```
In this API request, we are requesting the second page, where each page contains 20 posts.

4. Time-Based Pagination
In scenarios where data has a temporal aspect, time-based pagination can be useful. It involves using time-related parameters, such as "start_time" and "end_time," to specify a time range for retrieving data.

This technique enables fetching data in chronological or reverse-chronological order, allowing for efficient retrieval of recent or historical data.

For example -
```
GET /api/events?start_time=2023-01-01T00:00:00Z&end_time=2023-01-31T23:59:59Z
```

Here, this request fetches events that occurred between January 1, 2023, and January 31, 2023, based on their timestamp.