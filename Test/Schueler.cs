using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Schueler: Person
    {
        public int Note { get; set; }
        public int SID { get; set; }
        public Schueler(string v_name, string n_name, int id, int alter, int sid) : base(v_name, n_name, id, alter)
        {
            SID = sid;
        }
    }
}
