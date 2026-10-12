using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BewertungAufgabe
{
    public sealed class Bewertung
    {
        public Kunde Kunde { get; private set; }
        public Produkt Produkt { get; private set; }
        public int Sterne {  get; private set; }
        public string Kommentar { get; private set; }
        public DateTime ErstellungsDatum { get; private set; }
        public bool Gekauft { get; private set; }
        public Antwort? Antwort { get; private set; }

        public Bewertung(Kunde kunde, Produkt produkt, int sterne, string kommentar, bool gekauft, DateTime? datetime = null)
        {
            ArgumentNullException.ThrowIfNull(kunde);
            ArgumentNullException.ThrowIfNull(produkt);
            ArgumentNullException.ThrowIfNull(kommentar);

            if (sterne < 1 ||
                sterne > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(sterne), "Sterne müssen zwischen 1 und 5 liegen.");
            }

            this.Kunde = kunde;
            this.Produkt = produkt;
            this.Sterne = sterne;
            this.Kommentar = kommentar;
            this.Gekauft = gekauft;
            this.ErstellungsDatum = datetime ?? DateTime.UtcNow;

            produkt.AddBewertung(this);
            kunde.AddBewertung(this);
        }

        /// <summary>
        /// Verkäufer antwortet auf die Bewertung.
        /// Gibt false zurück, wenn bereits eine Antwort existiert.
        /// </summary>
        /// <param name="antworttext"></param>
        /// <param name="antwortdatum"></param>
        /// <returns></returns>
        public bool Antworten(string antworttext, DateTime? antwortdatum = null)
        {
            if (this.Antwort != null)
            {
                return false;
            }

            this.Antwort = new Antwort(this, antworttext, antwortdatum);

            return true;
        }
    }
}
