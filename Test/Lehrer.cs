using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Lehrer: Person
    {
        public Lehrer(string v_name, string n_name, int id, int alter, int lid) : base(v_name, n_name, id, alter)
        {
            LID = lid;
        }

        public int LID { get; set; }
    }
}
