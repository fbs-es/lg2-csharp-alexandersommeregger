using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BewertungAufgabe
{
    public class Hersteller
    {
        public string Herstellername { get; private set; }
        public string SupportEmail { get; private set; }
        private List<Produkt> ProduktList { get; set; } = new();
        public List<Produkt> Produkte => ProduktList;

        public Hersteller(string herstellername, string supportEmail)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(herstellername);
            ArgumentException.ThrowIfNullOrWhiteSpace(supportEmail);

            if (!supportEmail.Contains('@'))
            {
                throw new ArgumentException("SupportEmail muss ein '@' enthalten.", nameof(supportEmail));
            }

            this.Herstellername = herstellername;
            this.SupportEmail = supportEmail;
        }

        /// <summary>
        /// Fügt das Produkt zur Liste des Herstellers hinzu.
        /// Wird nur vom Konstruktor des Produkts aufgerufen.
        /// </summary>
        /// <param name="produkt"></param>
        internal void AddProdukt(Produkt produkt)
        {
            ArgumentNullException.ThrowIfNull(produkt);

            if (ProduktList.Contains(produkt))
            {
                throw new InvalidOperationException($"Produkt '{produkt.Produktnummer}' ist bereits beim Hersteller hinzugefügt.");
            }
            ProduktList.Add(produkt);
        }
    }
}
