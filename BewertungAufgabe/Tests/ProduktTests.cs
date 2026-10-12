using BewertungAufgabe;

namespace Tests
{
    [TestClass()]
    public class ProduktTests
    {
        private Hersteller Hersteller = null!;
        private Produkt Produkt = null!;

        [TestInitialize]
        public void Setup()
        {
            Hersteller = new Hersteller("asda", "sadf@abc.at");
            Produkt = new Produkt(1, "Kopfhörer", 59.90m, 10, Hersteller);
        }

        private static Kunde ErstelleKunde(long kundennummer)
        {
            return new Kunde(kundennummer, "abc");
        }

        //Konstruktor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProduktHerstellerNull()
        {
            new Produkt(2, "Maus", 19.90m, 10, null!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ProduktpreisNegativ()
        {
            new Produkt(2, "Maus", -0.01m, 10, Hersteller);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LagerbestandNegativ()
        {
            new Produkt(2, "Maus", 19.90m, -1, Hersteller);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(25)]
        public void LagerbestandGueltig(int lagerbestand)
        {
            Produkt produkt = new Produkt(2, "Maus", 19.90m, lagerbestand, Hersteller);

            Assert.AreEqual(lagerbestand, produkt.Lagerbestand);
            Assert.AreEqual(3, produkt.Mindestbestand);
        }

        [TestMethod]
        public void HerstellerHatMehrereProdukte()
        {
            Produkt produkt2 = new Produkt(2, "Maus", 19.90m, 10, Hersteller);

            Assert.AreEqual(2, Hersteller.Produkte.Count);
            Assert.AreSame(produkt2, Hersteller.Produkte[1]);
        }

        //BerechneDurchschnittssterne

        [TestMethod]
        public void DurchschnittOhneBewertungen()
        {
            decimal ergebnis = Produkt.BerechneDurchschnittssterne();

            Assert.AreEqual(0m, ergebnis);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        public void DurchschnittEineBewertung(int sterne)
        {
            new Bewertung(ErstelleKunde(1), Produkt, sterne, "abc", true);

            decimal ergebnis = Produkt.BerechneDurchschnittssterne();

            Assert.AreEqual((decimal)sterne, ergebnis);
        }

        [DataTestMethod]
        [DataRow(5, 3, 4.0)]
        [DataRow(5, 4, 4.5)]
        [DataRow(1, 1, 1.0)]
        [DataRow(1, 5, 3.0)]
        public void DurchschnittZweiBewertungen(int sterne1, int sterne2, double erwartet)
        {
            new Bewertung(ErstelleKunde(1), Produkt, sterne1, "abc", true);
            new Bewertung(ErstelleKunde(2), Produkt, sterne2, "abc", true);

            decimal ergebnis = Produkt.BerechneDurchschnittssterne();

            Assert.AreEqual((decimal)erwartet, ergebnis);
        }

        [TestMethod]
        public void DurchschnittUngueltigeBewertungZaehltNicht()
        {
            new Bewertung(ErstelleKunde(1), Produkt, 4, "abc", true);

            try
            {
                new Bewertung(ErstelleKunde(2), Produkt, 0, "abc", true);
            }
            catch (ArgumentOutOfRangeException)
            {
            }

            Assert.AreEqual(4m, Produkt.BerechneDurchschnittssterne());
        }
    }
}
