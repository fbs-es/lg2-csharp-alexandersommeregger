using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BewertungAufgabe
{
    public class Kunde
    {
        public long Kundennummer {  get; private set; }
        public string KundenName { get; private set; }
        private List<Bewertung> BewertungsList { get; set; } = new();
        public List<Bewertung> Bewertungen => BewertungsList;

        public Kunde(long kundennummer, string kundenName)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(kundennummer);
            ArgumentException.ThrowIfNullOrWhiteSpace(kundenName);

            this.Kundennummer = kundennummer;
            this.KundenName = kundenName;
        }

        /// <summary>
        /// Fügt die Bewertung zur Liste des Kunden hinzu.
        /// Wird nur vom Konstruktor der Bewertung aufgerufen.
        /// </summary>
        /// <param name="bewertung"></param>
        internal void AddBewertung(Bewertung bewertung)
        {
            ArgumentNullException.ThrowIfNull(bewertung);

            if (BewertungsList.Contains(bewertung))
            {
                throw new InvalidOperationException("Bewertung ist bereits beim Kunden hinzugefügt.");
            }
            BewertungsList.Add(bewertung);
        }
    }
}
