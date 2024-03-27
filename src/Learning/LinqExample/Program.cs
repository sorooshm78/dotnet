using LinqExample;

using (var context = new SchoolContext())
{
    //creates db if not exists 
    context.Database.EnsureCreated();

    //create entity objects
    var std1 = new Student() { FirstName = "Harry", LastName = "Potter", Age = 10 };
    var std2 = new Student() { FirstName = "Harry", LastName = "Gholi", Age = 12 };
    var std3 = new Student() { FirstName = "Tom", LastName = "Ridder", Age = 25 };
    var std4 = new Student() { FirstName = "Joly", LastName = "tommy", Age = 22 };

    //add entitiy to the context
    context.Students.Add(std1);
    context.Students.Add(std2);
    context.Students.Add(std3);
    context.Students.Add(std4);

    //save data to the database tables
    context.SaveChanges();

    // Query is compiled as IEnumerable<Student>
    // or perhaps IQueryable<Student>
    var Query = from student in context.Students select student.LastName;
    Console.WriteLine(Query.GetType().Name); //EntityQueryable

    IEnumerable<String> QueryIEnum = from student in context.Students select student.LastName;
    IQueryable<String> QueryIQuery = from student in context.Students select student.LastName;
    foreach (String item in QueryIEnum)
    {
        Console.WriteLine(item);
    }
}