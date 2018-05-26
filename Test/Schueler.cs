using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Schueler: Person, IComparable<Schueler>
    {
        public double Noten { get; }
        public int SID { get; }
        public string Klasse { get; }

        public Schueler(int id, string Klasse, string v_name, string n_name,double Note ) : base(v_name, n_name, id)
        {
            SID = id;
            this.Klasse = Klasse;
            Noten = Note;
        }
        //Wie man ein Objekt in ein string umwandelt (der Wert der ausgegeben werden soll)
        public override string ToString()
        {
            return $"SID: {SID} | Klasse: {Klasse} | Vorname: {Vorname} | Nachname: {Nachname} | Note: {Noten}";
        }
        public int CompareTo(Schueler vorherigeSID)
        {
            if (SID < vorherigeSID.SID)
                return -1;
            else if (SID > vorherigeSID.SID)
                return 1;
            else
                throw new Exception("SID kommt doppelt vor!");
        }
    }
}
