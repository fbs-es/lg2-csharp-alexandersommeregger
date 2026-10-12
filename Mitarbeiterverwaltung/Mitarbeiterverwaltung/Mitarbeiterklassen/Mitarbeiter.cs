using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Mitarbeiterklassen
{
    public class Mitarbeiter
    {
        public string Vorname { get; private set; }
        public string Nachname { get; private set; }
        public decimal Wochenstunden { get; private set; }
        public decimal Stundengehalt { get; private set; }

        public Mitarbeiter(string vorname, string nachname, decimal wochenstunden, decimal stundengehalt)
        {
            this.Vorname = vorname;
            this.Nachname = nachname;
            this.Wochenstunden = wochenstunden;
            this.Stundengehalt = stundengehalt;
        }

        /// <summary>
        /// Berechnet den Monatsgehalt.
        /// </summary>
        /// <param name="stundengehalt"></param>
        /// <param name="arbeitsstunden"></param>
        /// <returns></returns>
        public virtual decimal BerechneMonatsgehalt()
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Stundengehalt);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Wochenstunden);

            return this.Stundengehalt * this.Wochenstunden * 4;
        }
    }
}
