using System.Linq;

public class Verleihungsprozess
{
    private Kunde? Kunde { get; set; }
    private Scooter? Scooter { get; set; }
    public DateTime dateStart { get; private set; }
    public DateTime dateEnd { get; private set; }
    private static List<Scooter> Scooterlist { get; set; } = new();
    private static List<Kunde> KundenList { get; set; } = new();

    /// <summary>
    /// Fügt den Scooter zur Liste.
    /// </summary>
    /// <param name="scooter"></param>
    public void AddScooter(Scooter scooter)
    {
        ArgumentNullException.ThrowIfNull(scooter);

        if (Scooterlist.Contains(scooter))
        {
            throw new InvalidOperationException($"Scooter '{scooter.ScooterId}' ist bereits in der Liste hinzugefügt.");
        }
        Scooterlist.Add(scooter);
    }

    /// <summary>
    /// Gibt Kunde zurück.
    /// </summary>
    /// <returns></returns>
    public Kunde ReturnKunde()
    {
        return this.Kunde!;
    }

    /// <summary>
    /// Gibt Scooter zurück.
    /// </summary>
    /// <returns></returns>
    public Scooter ReturnScooter()
    {
        return this.Scooter!;
    }

    /// <summary>
    /// Löscht den Scooter aus der Liste.
    /// </summary>
    /// <param name="scooter"></param>
    public void Delete(Scooter scooter)
    {
        ArgumentNullException.ThrowIfNull(scooter);

        for (int i = Scooterlist.Count - 1; i >= 0; i--)
        {
            if (Scooterlist[i].ScooterId == scooter.ScooterId)
            {
                Scooterlist.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Ueberpruft ob ein Scooter frei ist.
    /// </summary>
    /// <returns></returns>
    public Scooter? GetFreeScooter()
    {
        foreach (Scooter scooter in Scooterlist)
        {
            if (scooter.Ausleihbar &&
                scooter.Akkustand > 0.15m)
            {
                return scooter;
            }
        }

        return null;
    }

    /// <summary>
    /// Ueberpruft den Kunden.
    /// </summary>
    /// <param name="kunde"></param>
    /// <returns></returns>
    public bool PruefeKunde(Kunde kunde)
    {
        ArgumentNullException.ThrowIfNull(kunde);

        if (!kunde.BereitsGeliehen &&
            !kunde.Sperre &&
            kunde.Guthaben > 1.00m)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="kunde"></param>
    /// <returns></returns>
    public bool Ausleihen(Kunde kunde)
    {
        ArgumentNullException.ThrowIfNull(kunde);

        Scooter? freierScooter = GetFreeScooter();

        if (!PruefeKunde(kunde) ||
            freierScooter == null)
        {
            return false;
        }

        freierScooter.SetAusleihbar(false);
        kunde.SetBereitsgeliehen(true);
        this.Kunde = kunde;
        this.Scooter = freierScooter;
        dateStart = DateTime.UtcNow;

        return true;
    }

    /// <summary>
    /// Gibt den Scooter zurück.
    /// </summary>
    /// <param name="kunde"></param>
    /// <param name="scooter"></param>
    /// <returns></returns>
    public bool Zurückgeben(Kunde kunde, Scooter scooter)
    {
        ArgumentNullException.ThrowIfNull(kunde);
        ArgumentNullException.ThrowIfNull(scooter);

        if (scooter.Ausleihbar ||
            !kunde.BereitsGeliehen ||
            kunde != this.Kunde ||
            scooter != this.Scooter)
        {
            return false;
        }

        dateEnd = DateTime.UtcNow;
        int minutes = GetMinutes(dateStart, dateEnd);

        scooter.SetAkkustand(scooter.Akkustand - minutes * 0.01m);
        kunde.SetGuthaben(kunde.Guthaben - minutes * 0.20m);
        scooter.SetAusleihbar(true);
        kunde.SetBereitsgeliehen(false);

        return true;
    }

    /// <summary>
    /// Berechnet nur voll Minuten der Differenz.
    /// </summary>
    /// <param name="dateStart"></param>
    /// <param name="dateEnd"></param>
    /// <returns></returns>
    public static int GetMinutes(DateTime dateStart, DateTime dateEnd)
    {
        if (dateEnd < dateStart)
        {
            return 0;
        }

        return (int)Math.Floor((dateEnd - dateStart).TotalMinutes);
    }
}