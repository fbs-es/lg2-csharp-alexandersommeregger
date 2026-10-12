public class Scooter
{
    public long ScooterId { get; private set; }
    public decimal Akkustand { get; private set; }
    public bool Ausleihbar { get; private set; }

    public Scooter()
    {
        this.ScooterId = Random.Shared.NextInt64(1, long.MaxValue);
        this.Akkustand = 1.00m;
        this.Ausleihbar = true;
    }

    /// <summary>
    /// Setzt den Wert für Ausleihbar.
    /// </summary>
    /// <param name="ausleihbar"></param>
    public void SetAusleihbar(bool ausleihbar)
    {
        this.Ausleihbar = ausleihbar;
    }

    /// <summary>
    /// Setzt den aktuellen Akkustand.
    /// </summary>
    /// <param name="akku"></param>
    public void SetAkkustand(decimal akku)
    {
        this.Akkustand = Math.Max(0m, akku);
    }
}