using BewertungAufgabe;

namespace Tests
{
    [TestClass()]
    public class KundeHerstellerTests
    {
        //Kunde

        [TestMethod]
        public void KundeErstellen()
        {
            Kunde kunde = new Kunde(1001, "abc");

            Assert.AreEqual(1001L, kunde.Kundennummer);
            Assert.AreEqual("abc", kunde.KundenName);
            Assert.AreEqual(0, kunde.Bewertungen.Count);
        }

        //Hersteller

        [DataTestMethod]
        [DataRow((string?)null)]
        [DataRow("")]
        [DataRow("asdf.at")]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void SupportEmailUngueltig(string email)
        {
            new Hersteller("asdfa", email);
        }

        [TestMethod]
        public void HerstellerErstellen()
        {
            Hersteller hersteller = new Hersteller("abcdef", "adsf@test.at");

            Assert.AreEqual("abcdef", hersteller.Herstellername);
            Assert.AreEqual("asdf@test.at", hersteller.SupportEmail);
            Assert.AreEqual(0, hersteller.Produkte.Count);
        }
    }
}
