using KeyExample;

internal class Program
{
    private static void CarsModelTest(KeyContext context)
    {
        //create entity objects
        var c1 = new Car() { Code = "11", Make = "make1", Model = "model1" };
        var c2 = new Car() { Code = "22", Make = "make2", Model = "model2" };
        
        //add entitiy to the context
        context.Cars.Add(c1);
        context.Cars.Add(c2);
    
        //save data to the database tables
        context.SaveChanges();

        foreach (var car in context.Cars)
        {
            Console.WriteLine($"Code {car.Code} | Model {car.Model} | Make {car.Make}");
        }
    }
    private static void StudentsModelTest(KeyContext context)
    {
        //create entity objects
        var s1 = new Student { FirstName = "f1", LastName = "l1", Age = 1 };
        var s2 = new Student { FirstName = "f2", LastName = "l2", Age = 2 };

        //add entitiy to the context
        context.Students.Add(s1);
        context.Students.Add(s2);

        //save data to the database tables
        context.SaveChanges();

        foreach (var std in context.Students)
        {
            Console.WriteLine($"fname {std.FirstName} | lname {std.LastName} | age {std.Age}");
        }
    }
    private static void PersonsModelTest(KeyContext context)
    {
        //create entity objects
        var p1 = new Person { FirstName = "f1", LastName = "l1"};
        var p2 = new Person { FirstName = "f2", LastName = "l2"};

        //add entitiy to the context
        context.Persons.Add(p1);
        context.Persons.Add(p2);

        //save data to the database tables
        context.SaveChanges();

        foreach (var person in context.Persons)
        {
            Console.WriteLine($"code {person.Code} | fname {person.FirstName} | lname {person.LastName}");
        }
    }
    
    private static void ManagersModelTest(KeyContext context)
    {
        //create entity objects
        var m1 = new Manager {Code=Guid.NewGuid(), FirstName = "f1", LastName = "l1"};
        var m2 = new Manager {Code=Guid.NewGuid(), FirstName = "f2", LastName = "l2"};

        //add entitiy to the context
        context.Managers.Add(m1);
        context.Managers.Add(m2);

        //save data to the database tables
        context.SaveChanges();

        foreach (var manager in context.Managers)
        {
            Console.WriteLine($"code {manager.Code} | fname {manager.FirstName} | lname {manager.LastName}");
        }
    }

    private static void Main(string[] args)
    {
        using (var context = new KeyContext())
        {
            //creates db if not exists 
            context.Database.EnsureCreated();
            CarsModelTest(context);
            StudentsModelTest(context);
            PersonsModelTest(context);
            ManagersModelTest(context);
        }
    }
}