using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Mitarbeiterklassen
{
    public class Angestellter : Mitarbeiter
    {
        public DateTime Einstellungsdatum { get; private set; }
        /// <summary>
        /// Anspruch in Tagen.
        /// </summary>
        public int Urlaubsanspruch { get; private set; }

        public Angestellter(string vorname, string nachname, decimal wochenstunden, decimal stundengehalt, DateTime einstellungsdatum, int urlaubsanspruch)
                            :base(vorname, nachname, wochenstunden, stundengehalt)
        {
            this.Einstellungsdatum = einstellungsdatum;
            this.Urlaubsanspruch = urlaubsanspruch;
        }
    }
}