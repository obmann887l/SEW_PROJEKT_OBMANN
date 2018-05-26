using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int ID { get; set; }
        

        public Person(string v_name, string n_name, int id)
        {
            Vorname = v_name;
            Nachname = n_name;
            ID = id;
            
        }
    }
}
