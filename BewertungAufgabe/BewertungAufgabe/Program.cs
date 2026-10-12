using BewertungAufgabe;

Hersteller hersteller = new Hersteller("TechCorp", "support@techcorp.at");
Produkt produkt = new Produkt(1, "Kopfhörer", 59.90m, 10, hersteller);

Kunde kunde1 = new Kunde(1001, "Max Muster");
Kunde kunde2 = new Kunde(1002, "Anna Beispiel");

Bewertung bewertung1 = new Bewertung(kunde1, produkt, 5, "Super Klang!", true);
Bewertung bewertung2 = new Bewertung(kunde2, produkt, 3, "Geht so.", false);

bewertung2.Antworten("Danke für Ihr Feedback, wir melden uns.");

Console.WriteLine($"Produkt: {produkt.Produktname} von {produkt.Hersteller.Herstellername}");
Console.WriteLine($"Durchschnitt: {produkt.BerechneDurchschnittssterne():0.00} Sterne");

foreach (Bewertung bewertung in produkt.Bewertungen)
{
    Console.WriteLine($"{bewertung.Kunde.KundenName}: {bewertung.Sterne} Sterne - {bewertung.Kommentar} (gekauft: {bewertung.Gekauft})");

    if (bewertung.Antwort != null)
    {
        Console.WriteLine($"   Antwort: {bewertung.Antwort.Antworttext}");
    }
}
