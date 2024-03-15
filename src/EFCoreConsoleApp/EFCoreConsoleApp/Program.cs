using EFCoreConsoleApp;
using Microsoft.Extensions.Configuration;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

using (var context = new SchoolDbContext(configuration))
{
    //creates db if not exists 
    context.Database.EnsureCreated();

    //create entity objects
    var teacher1 = new Teacher() { FirstName = "T1", LastName = "T1" };
    var std1 = new Student() { FirstName = "S1", LastName = "S1", PhoneNumber = "9092", Teacher = teacher1 };
    var std2 = new Student() { FirstName = "S2", LastName = "S2", PhoneNumber = "9093", Teacher = teacher1 };

    //add entitiy to the context
    context.Students.Add(std1);
    context.Students.Add(std2);

    //save data to the database tables
    context.SaveChanges();

    //retrieve all the students from the database
    foreach (var s in context.Students)
    {
        Console.WriteLine($"First Name: {s.FirstName}, Last Name: {s.LastName}");
    }
}
