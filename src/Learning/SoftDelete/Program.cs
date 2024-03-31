using Microsoft.EntityFrameworkCore;
using SoftDelete;

using (var context = new PersonContext())
{
    //creates db if not exists 
    context.Database.EnsureCreated();

    //create entity objects
    var p1 = new Person() { Name = "p1", Age = 10 };
    var p2 = new Person() { Name = "p2", Age = 10 };
    var p3 = new Person() { Name = "p3", Age = 10 };


    //add entitiy to the context
    context.Persons.Add(p1);
    context.Persons.Add(p2);
    context.Persons.Add(p3);

    //save data to the database tables
    context.SaveChanges();

    //soft delete
    var p = context.Persons.Where(p => p.Name == "p1").First();
    context.Persons.Remove(p);
    context.SaveChanges();  
}