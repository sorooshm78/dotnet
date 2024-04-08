using AutoMapper;
using SimpleAutoMapper;


internal class Program
{
    public record StudentRecordDTO(String FirstName, String LastName, int Age);
    private static void Main(string[] args)
    {
        var student = new Student()
        {
            FirstName = "Tom",
            LastName = "Tommy",
            Age = 19,
            PhoneNumber = "12345",
            Email = "sm@sm.com"
        };

        // var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentRecordDTO>());
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>());

        var mapper = new Mapper(config);

        // StudentRecordDTO stdDTO = mapper.Map<StudentRecordDTO>(student);
        StudentDTO stdDTO = mapper.Map<StudentDTO>(student);

        Console.WriteLine($"FName : {stdDTO.FirstName} - LName : {stdDTO.LastName} - Age : {stdDTO.Age}");
    }
}

