using BewertungAufgabe;

namespace Tests
{
    [TestClass()]
    public class BewertungTests
    {
        private Hersteller Hersteller = null!;
        private Produkt Produkt = null!;
        private Kunde Kunde = null!;

        [TestInitialize]
        public void Setup()
        {
            Hersteller = new Hersteller("adsfa", "sppt@abd.at");
            Produkt = new Produkt(1, "Kopfhörer", 59.90m, 10, Hersteller);
            Kunde = new Kunde(1001, "abc");
        }

        private Bewertung ErstelleBewertung(int sterne)
        {
            return new Bewertung(Kunde, Produkt, sterne, "Kommentar", true);
        }

        [TestMethod]
        public void BewertungWirdVerknuepft()
        {
            Bewertung bewertung = ErstelleBewertung(4);

            Assert.AreSame(Kunde, bewertung.Kunde);
            Assert.AreSame(Produkt, bewertung.Produkt);
            Assert.AreSame(bewertung, Produkt.Bewertungen[0]);
            Assert.AreSame(bewertung, Kunde.Bewertungen[0]);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void BewertungGekauftFlag(bool gekauft)
        {
            Bewertung bewertung = new Bewertung(Kunde, Produkt, 4, "abc", gekauft);

            Assert.AreEqual(gekauft, bewertung.Gekauft);
        }


        [TestMethod]
        public void BewertungDatumStandardJetzt()
        {
            DateTime vorher = DateTime.UtcNow;

            Bewertung bewertung = ErstelleBewertung(4);

            DateTime nachher = DateTime.UtcNow;
            Assert.IsTrue(bewertung.ErstellungsDatum >= vorher);
            Assert.IsTrue(bewertung.ErstellungsDatum <= nachher);
        }

        [TestMethod]
        public void KundeBewertetMehrereProdukte()
        {
            Produkt produkt2 = new Produkt(2, "Maus", 19.90m, 10, Hersteller);

            ErstelleBewertung(5);
            new Bewertung(Kunde, produkt2, 2, "abc", true);

            Assert.AreEqual(2, Kunde.Bewertungen.Count);
            Assert.AreEqual(1, Produkt.Bewertungen.Count);
            Assert.AreEqual(1, produkt2.Bewertungen.Count);
        }

        [TestMethod]
        public void ProduktVonMehrerenKundenBewertet()
        {
            Kunde kunde2 = new Kunde(1002, "def");

            ErstelleBewertung(5);
            new Bewertung(kunde2, Produkt, 1, "abc", false);

            Assert.AreEqual(2, Produkt.Bewertungen.Count);
            Assert.AreEqual(1, Kunde.Bewertungen.Count);
            Assert.AreEqual(1, kunde2.Bewertungen.Count);
        }

        //Antworten

        [TestMethod]
        public void AntwortenErfolgreich()
        {
            Bewertung bewertung = ErstelleBewertung(3);

            bool ergebnis = bewertung.Antworten("Danke!");

            Assert.IsTrue(ergebnis);
            Assert.IsNotNull(bewertung.Antwort);
            Assert.AreEqual("Danke!", bewertung.Antwort.Antworttext);
            Assert.AreSame(bewertung, bewertung.Antwort.Bewertung);
        }

        [TestMethod]
        public void AntwortenDatumStandardJetzt()
        {
            Bewertung bewertung = ErstelleBewertung(3);
            DateTime vorher = DateTime.UtcNow;

            bewertung.Antworten("Danke!");

            DateTime nachher = DateTime.UtcNow;
            Assert.IsTrue(bewertung.Antwort!.Antwortdatum >= vorher);
            Assert.IsTrue(bewertung.Antwort.Antwortdatum <= nachher);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AntwortenDatumVorBewertung()
        {
            DateTime bewertungsdatum = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc);
            Bewertung bewertung = new Bewertung(Kunde, Produkt, 3, "abc", true, bewertungsdatum);

            bewertung.Antworten("Danke!", bewertungsdatum.AddDays(-1));
        }

        [TestMethod]
        public void AntwortenDatumGleichWieBewertung()
        {
            DateTime datum = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc);
            Bewertung bewertung = new Bewertung(Kunde, Produkt, 3, "abc", true, datum);

            bool ergebnis = bewertung.Antworten("Danke!", datum);

            Assert.IsTrue(ergebnis);
            Assert.AreEqual(datum, bewertung.Antwort!.Antwortdatum);
        }
    }
}
