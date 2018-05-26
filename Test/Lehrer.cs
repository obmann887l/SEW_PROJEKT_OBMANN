using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Lehrer: Person , IComparable<Lehrer>
    {
        public int LID { get; set; }
        public string Klasse { get; }
        public string Lehrerkürzel { get; set; }

        public Lehrer(int id , string Klasse, string v_name, string n_name, string lehrerkuerzel) : base(v_name, n_name, id )
        {
            LID = id;
            Lehrerkürzel = lehrerkuerzel;
            this.Klasse = Klasse;
        }

        public override string ToString()
        {
            return $"LID: {LID} | Klasse: {Klasse} | Vorname: {Vorname} | Nachname: {Nachname} | Lehrerkürzel: {Lehrerkürzel}";
        }

        public int CompareTo(Lehrer vorherigeLID)
        {
            if (LID < vorherigeLID.LID)
                return -1;
            else if (LID > vorherigeLID.LID)
                return 1;
            else
                throw new Exception("LID kommt doppelt vor!");
        }

        
    }
}
