using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConsoleApp
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<Student> Students { get; set; }     
    }
}

