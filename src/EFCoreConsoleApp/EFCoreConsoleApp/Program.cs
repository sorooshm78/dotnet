using EFCoreConsoleApp;
using Microsoft.EntityFrameworkCore;
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
    var teacher1 = new Teacher() { FirstName = "TF", LastName = "TL" };
    var std1 = new Student() { FirstName = "SF1", LastName = "SL1", PhoneNumber = "9092", Teacher = teacher1 };
    var std2 = new Student() { FirstName = "SF2", LastName = "SL2", PhoneNumber = "9093", Teacher = teacher1 };

    //add entitiy to the context
    context.Students.Add(std1);
    context.Students.Add(std2);

    //save data to the database tables
    context.SaveChanges();

    //retrieve all the students from the database
    foreach (var s in context.Students.Include(Student => Student.Teacher).ToList())
    {
        Console.WriteLine($"{s} {s.Teacher.FirstName}");
    }

    //retrieve all the Teacher from the database
    foreach (var teacher in context.Teachers.Include(Teacher => Teacher.Students).ToList())
    {
        var students = teacher.Students;;
        foreach(var student in students)
        {
            Console.WriteLine($"{teacher} {student}");
        }
    }
}
