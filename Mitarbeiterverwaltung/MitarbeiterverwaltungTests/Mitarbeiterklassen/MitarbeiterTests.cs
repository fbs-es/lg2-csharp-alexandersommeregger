using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mitarbeiterverwaltung.Mitarbeiterklassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Mitarbeiterklassen.Tests
{
    [TestClass()]
    public class MitarbeiterTests
    {
        private static readonly DateTime Eintrittsdatum = new DateTime(2024, 1, 1);
        public static IEnumerable<object[]> GehaltsTestdaten =>
        [
            [new Angestellter("hans", "asdfuh", 40.0m, 25.00m, Eintrittsdatum, 25), 1000.00m],
            [new Aushilfe("otto", "ahisdhf", 10.0, Eintrittsdatum, false, 10.0m), 100.00m],
            [new Auszubildender("hdsif", "asdfuh", 40.0m, 25.00m, "Applikationsentwicklung", 2), 0.00m]
        ];

        [TestMethod]
        [DynamicData(nameof(GehaltsTestdaten))]
        public void BerechneMonatsgehalt_LiefertErwartetesGehalt(Mitarbeiter mitarbeiter, decimal erwartet)
        {
            decimal ergebnis = mitarbeiter.BerechneMonatsgehalt();

            Assert.AreEqual(erwartet, ergebnis);
        }
    }
}