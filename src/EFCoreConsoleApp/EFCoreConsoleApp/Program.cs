// Install Package 
// dotnet add package <packageName>
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer


using EFCoreConsoleApp;


using (var context = new SchoolDbContext())
{
    //creates db if not exists 
    context.Database.EnsureCreated();

    //create entity objects
    var std1 = new Student() { FirstName = "Soroush", LastName = "Mohammadi" };

    //add entitiy to the context
    context.Students.Add(std1);

    //save data to the database tables
    context.SaveChanges();

    //retrieve all the students from the database
    foreach (var s in context.Students)
    {
        Console.WriteLine($"First Name: {s.FirstName}, Last Name: {s.LastName}");
    }
}
