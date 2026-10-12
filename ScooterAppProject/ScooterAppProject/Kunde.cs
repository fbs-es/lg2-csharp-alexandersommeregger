using System;
using System.Collections.Generic;

public class Kunde
{
    /// <summary>
    /// KundenID erstellen, automatisch mit random.
    /// </summary>
    public long KundenId { get; private set; }
    public string Name { get; private set; }
    public string EMail { get; private set; }
    public bool Mahnung { get; private set; }
    public bool Sperre { get; private set; }
    public bool BereitsGeliehen { get; private set; }
    public decimal Guthaben { get; private set; }

    /// <summary>
    /// Erstellt den Kunden.
    /// </summary>
    /// <param name="aufladungsBetrag"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <exception cref="ArgumentException"></exception>
    public Kunde(decimal aufladungsBetrag, string name, string email)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(aufladungsBetrag, 10.00m);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!ValidateMail(email))
        {
            throw new ArgumentException($"Die Email: {email} hat ein ungültiges Format.");
        }

        this.KundenId = Random.Shared.NextInt64(1, long.MaxValue);
        this.Name = name;
        this.EMail = email;
        this.Guthaben += aufladungsBetrag;
        this.Mahnung = false;
        this.Sperre = false;
        this.BereitsGeliehen = false;
    }

    /// <summary>
    /// Überprüft die Email ob sie ein valides Format hat.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool ValidateMail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        int punkt = email.LastIndexOf('.');
        int emailIndex = email.IndexOf('@');

        if (emailIndex <= 0 ||
            emailIndex != email.LastIndexOf('@'))
        {
            return false;
        }

        return (punkt > emailIndex + 1 &&
                punkt < email.Length - 1);
    }

    /// <summary>
    /// setzt den namen.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool ChangeName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        this.Name = name;

        return true;
    }

    /// <summary>
    /// Setzt Bereitsgeliehen.
    /// </summary>
    /// <param name="geliehen"></param>
    public void SetBereitsgeliehen(bool geliehen)
    {
        this.BereitsGeliehen = geliehen;
    }

    /// <summary>
    /// Setzt guthaben.
    /// </summary>
    /// <param name="guthaben"></param>
    public void SetGuthaben(decimal guthaben)
    {
        this.Guthaben = guthaben;
    }
}