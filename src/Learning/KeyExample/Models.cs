using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyExample
{
    public class Car
    {
        [Key]
        public String Code { get; set; }

        public string? Make { get; set; }
        public string? Model { get; set; }
    }

    [PrimaryKey(nameof(FirstName), nameof(LastName))]
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
    }


    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }   
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
    }


    public class Manager
    {
        [Key]
        public Guid Code { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
