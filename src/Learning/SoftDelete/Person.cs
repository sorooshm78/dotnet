using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftDelete
{
    public class Person
    {
        public int Id { get; set; }
        public String? Name { get; set; }
        public int Age { get; set; }

        public bool IsDeleted { get; set; }
    }
}
