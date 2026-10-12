using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Mitarbeiterklassen
{
    public class Auszubildender : Mitarbeiter
    {
        public string Ausbildungsberuf {  get; private set; }
        public int Lehrjahr {  get; private set; }
        /// <summary>
        /// Urlaubsanspruch in Tagen.
        /// </summary>
        public int Urlaubsanspruch {  get; private set; }  

        public Auszubildender(string vorname, string nachname, decimal wochenstunden, decimal stundengehalt, string ausbildungsberuf, int lehrjahr)
                    : base(vorname, nachname, wochenstunden, stundengehalt)
        {
            this.Ausbildungsberuf = ausbildungsberuf;
            this.Lehrjahr = lehrjahr;
        }
    }
}
