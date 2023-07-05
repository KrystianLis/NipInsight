using NipInsight.Domain.Enums;

namespace NipInsight.Domain.Entities;

public class Entity
{
    public string Nip { get; set; }

    public string Name { get; set; }

    public StatusVat? StatusVat { get; set; }

    public string? Regon { get; set; }

    public Pesel? Pesel { get; set; }

    public string? Krs { get; set; }

    public string? ResidenceAddress { get; set; }

    public string? WorkingAddress { get; set; }

    public List<EntityPerson>? Representatives { get; set; }

    public List<EntityPerson>? AuthorizedClerks { get; set; }

    public List<EntityPerson>? Partners { get; set; }

    public DateTime? RegistrationLegalDate { get; set; }

    public DateTime? RegistrationDenialDate { get; set; }

    public string? RegistrationDenialBasis { get; set; }

    public DateTime? RestorationDate { get; set; }

    public string? RestorationBasis { get; set; }

    public DateTime? RemovalDate { get; set; }

    public string? RemovalBasis { get; set; }

    public List<string>? AccountNumbers { get; set; }

    public bool? HasVirtualAccounts { get; set; }
}