using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BewertungAufgabe
{
    public sealed class Antwort
    {
        public Bewertung Bewertung { get; private set; }
        public string Antworttext { get; private set; }
        public DateTime Antwortdatum { get; private set; }

        /// <summary>
        /// Wird nur über Bewertung.Antworten() erstellt,
        /// damit eine Antwort immer genau einer Bewertung gehört.
        /// </summary>
        internal Antwort(Bewertung bewertung, string antworttext, DateTime? antwortdatum = null)
        {
            ArgumentNullException.ThrowIfNull(bewertung);
            ArgumentException.ThrowIfNullOrWhiteSpace(antworttext);

            DateTime datum = antwortdatum ?? DateTime.UtcNow;

            if (datum < bewertung.ErstellungsDatum)
            {
                throw new ArgumentException("Antwortdatum darf nicht vor dem Erstellungsdatum der Bewertung liegen.", nameof(antwortdatum));
            }

            this.Bewertung = bewertung;
            this.Antworttext = antworttext;
            this.Antwortdatum = datum;
        }
    }
}
