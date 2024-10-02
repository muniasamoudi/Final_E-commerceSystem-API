using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.DataModel
{
    public class Employee : Person
    {
        public Employee() { }
        public Employee(int id, string name) : base(id, name) { }
    }
}
