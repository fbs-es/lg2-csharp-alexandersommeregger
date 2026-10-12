using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Mitarbeiterklassen
{
    public class Aushilfe : Mitarbeiter
    {
        /// <summary>
        /// Zeit in Stunden.
        /// </summary>
        public double Arbeitszeit { get; private set; }
        public DateTime DatumArbeitszeit {  get; private set; }
        public bool IsAbgerechnet {  get; private set; }

        public Aushilfe(string vorname, string nachname, double arbeitszeit, DateTime datumarbeitszeit, bool isabgerechnet, decimal stundengehalt, decimal wochenstunden = 0)
                    : base(vorname, nachname, wochenstunden,  stundengehalt)
        {
            this.Arbeitszeit = arbeitszeit;
            this.DatumArbeitszeit = datumarbeitszeit;
            this.IsAbgerechnet = isabgerechnet;
        }

        /// <summary>
        /// Berechnet den monatsgehalt einer Aushilfe.
        /// </summary>
        /// <param name="stundensatz"></param>
        /// <returns></returns>
        public decimal BerechneMonatsgehalt(decimal stundensatz)
        {
            decimal auszahlungsbetrag = 0;

            if (this.Arbeitszeit > 0.0)
            {
                auszahlungsbetrag = (decimal)this.Arbeitszeit * stundensatz;
                this.IsAbgerechnet = true;
            }

            return auszahlungsbetrag;
        }
    }
}
