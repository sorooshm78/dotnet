using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConsoleApp
{
    public class Student
    {
        public int StudentId {get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }
        [NotMapped]
        public string? TempField { get; set;}

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
