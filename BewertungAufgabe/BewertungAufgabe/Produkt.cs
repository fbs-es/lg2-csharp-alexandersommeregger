using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BewertungAufgabe
{
    public class Produkt
    {
        public long Produktnummer {  get; private set; }
        public string Produktname { get; private set; }
        public decimal Produktpreis {  get; private set; }
        public int Lagerbestand { get; private set; }
        public int Mindestbestand { get; private set; } = 3;
        public Hersteller Hersteller { get; private set; }

        private List<Bewertung> bewertungslist = new List<Bewertung>();
        public List<Bewertung> Bewertungen => bewertungslist;

        public Produkt(long produktnummer, string produktname, decimal produktpreis, int lagerbestand, Hersteller hersteller)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(produktnummer);
            ArgumentException.ThrowIfNullOrWhiteSpace(produktname);
            ArgumentOutOfRangeException.ThrowIfNegative(produktpreis);
            ArgumentOutOfRangeException.ThrowIfNegative(lagerbestand);
            ArgumentNullException.ThrowIfNull(hersteller);

            this.Produktnummer = produktnummer;
            this.Produktname = produktname;
            this.Produktpreis = produktpreis;
            this.Lagerbestand = lagerbestand;
            this.Hersteller = hersteller;

            hersteller.AddProdukt(this);
        }

        /// <summary>
        /// Fügt die Bewertung zur Liste des Produkts hinzu.
        /// Wird nur vom Konstruktor der Bewertung aufgerufen.
        /// </summary>
        /// <param name="bewertung"></param>
        internal void AddBewertung(Bewertung bewertung)
        {
            ArgumentNullException.ThrowIfNull(bewertung);

            if (bewertungslist.Contains(bewertung))
            {
                throw new InvalidOperationException("Bewertung ist bereits beim Produkt hinzugefügt.");
            }
            bewertungslist.Add(bewertung);
        }

        /// <summary>
        /// Berechnet die durchschnittlichen Sterne aller Bewertungen.
        /// Gibt 0 zurück, wenn es noch keine Bewertung gibt.
        /// </summary>
        /// <returns></returns>
        public decimal BerechneDurchschnittssterne()
        {
            if (bewertungslist.Count == 0)
            {
                return 0m;
            }

            int summe = 0;

            foreach (Bewertung bewertung in bewertungslist)
            {
                summe += bewertung.Sterne;
            }

            return (decimal)summe / bewertungslist.Count;
        }
    }
}
