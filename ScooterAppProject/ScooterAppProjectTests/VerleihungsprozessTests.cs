namespace Tests
{
    [TestClass()]
    public class VerleihungsprozessTests
    {
        private Verleihungsprozess Verleih = null!;

        [TestInitialize]
        public void Setup()
        {
            Verleih = new Verleihungsprozess();
        }

        private static Kunde ErstelleKunde()
        {
            return new Kunde(10.00m, "abc", "abc@test.at");
        }

        //Ausleihen

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AusleihenKundeException()
        {
            Verleih.Ausleihen(null!);
        }

        [DataTestMethod]
        [DataRow(false, 1.00, false)]   
        [DataRow(false, 0.00, false)]  
        [DataRow(true, 10.00, false)]   
        public void Ausleihen_Kundenbedingungen(bool bereitsGeliehen, double guthaben, bool erwartet)
        {
            Scooter scooter = new Scooter();
            Verleih.AddScooter(scooter);

            Kunde kunde = ErstelleKunde();
            kunde.SetBereitsgeliehen(bereitsGeliehen);
            kunde.SetGuthaben((decimal)guthaben);

            bool ergebnis = Verleih.Ausleihen(kunde);

            Assert.AreEqual(erwartet, ergebnis);
            Assert.AreEqual(!erwartet, scooter.Ausleihbar);
        }

        [DataTestMethod]
        [DataRow(1.00, true, true)]
        [DataRow(0.16, true, true)]    
        [DataRow(0.15, true, true)]   
        [DataRow(0.00, true, true)]   
        [DataRow(1.00, false, true)]  
        public void Ausleihen_Scooterbedingungen(double akkustand, bool ausleihbar, bool erwartet)
        {
            Scooter scooter = new Scooter();
            scooter.SetAkkustand((decimal)akkustand);
            scooter.SetAusleihbar(ausleihbar);
            Verleih.AddScooter(scooter);

            Kunde kunde = ErstelleKunde();

            bool ergebnis = Verleih.Ausleihen(kunde);

            Assert.AreEqual(erwartet, ergebnis);
            Assert.AreEqual(erwartet, kunde.BereitsGeliehen);
        }

        [TestMethod]
        public void AusleihenNichtVorhanden()
        {
            Kunde kunde = ErstelleKunde();

            bool ergebnis = Verleih.Ausleihen(kunde);

            Assert.IsFalse(ergebnis);
            Assert.IsFalse(kunde.BereitsGeliehen);
        }

        [TestMethod]
        public void AusleihenErfolgreich()
        {
            Scooter scooter = new Scooter();
            Verleih.AddScooter(scooter);
            Kunde kunde = ErstelleKunde();

            Verleih.Ausleihen(kunde);

            Assert.AreSame(kunde, Verleih.ReturnKunde());
            Assert.AreSame(scooter, Verleih.ReturnScooter());
        }

        [TestMethod]
        public void AusleihenNimmtAnderenScooter()
        {
            Scooter verliehen = new Scooter();
            verliehen.SetAusleihbar(false);
            Scooter frei = new Scooter();
            Verleih.AddScooter(verliehen);
            Verleih.AddScooter(frei);

            Verleih.Ausleihen(ErstelleKunde());

            Assert.AreSame(frei, Verleih.ReturnScooter());
        }

        //Zurückgeben

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ZurueckgebenKundeNull()
        {
            Verleih.Zurückgeben(null!, new Scooter());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ZuruckgebenScooterNull()
        {
            Verleih.Zurückgeben(ErstelleKunde(), null!);
        }

        [TestMethod]
        public void ZuruckgebenOhneLeihen()
        {
            Scooter scooter = new Scooter();
            scooter.SetAusleihbar(false);
            Kunde kunde = ErstelleKunde();
            kunde.SetBereitsgeliehen(true);

            bool ergebnis = Verleih.Zurückgeben(kunde, scooter);

            Assert.IsFalse(ergebnis);
        }

        [TestMethod]
        public void ZuruckgebenFalscherKunde()
        {
            Scooter scooter = new Scooter();
            Verleih.AddScooter(scooter);
            Verleih.Ausleihen(ErstelleKunde());

            Kunde fremderKunde = ErstelleKunde();
            fremderKunde.SetBereitsgeliehen(true);

            bool ergebnis = Verleih.Zurückgeben(fremderKunde, scooter);

            Assert.IsFalse(ergebnis);
            Assert.IsFalse(scooter.Ausleihbar);
        }

        [TestMethod]
        public void ZuruckgebenFalscherScooter()
        {
            Scooter scooter = new Scooter();
            Verleih.AddScooter(scooter);
            Kunde kunde = ErstelleKunde();
            Verleih.Ausleihen(kunde);

            Scooter fremderScooter = new Scooter();
            fremderScooter.SetAusleihbar(false);

            bool ergebnis = Verleih.Zurückgeben(kunde, fremderScooter);

            Assert.IsFalse(ergebnis);
            Assert.IsTrue(kunde.BereitsGeliehen);
        }

        [TestMethod]
        public void Zurueckgeben2Mal()
        {
            Scooter scooter = new Scooter();
            Verleih.AddScooter(scooter);
            Kunde kunde = ErstelleKunde();
            Verleih.Ausleihen(kunde);

            bool erstesMal = Verleih.Zurückgeben(kunde, scooter);
            bool zweitesMal = Verleih.Zurückgeben(kunde, scooter);

            Assert.IsTrue(erstesMal);
            Assert.IsFalse(zweitesMal);
        }

        [TestMethod]
        public void KundeLeiht2MalHintereinander()
        {
            Scooter scooter = new Scooter();
            Verleih.AddScooter(scooter);
            Kunde kunde = ErstelleKunde();

            Verleih.Ausleihen(kunde);
            Verleih.Zurückgeben(kunde, scooter);
            bool nochmal = Verleih.Ausleihen(kunde);

            Assert.IsTrue(nochmal);
        }
    }
}