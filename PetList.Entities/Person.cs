using System;
using System.Collections.Generic;
using System.Text;

namespace PetList.Entities
{
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public Pet[] Pets { get; set; }
    }
}
