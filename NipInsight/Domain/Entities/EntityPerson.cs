namespace NipInsight.Domain.Entities;

public class EntityPerson
{
    public Guid Id { get; set; }

    public string? CompanyName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public Pesel? Pesel { get; set; }

    public string? Nip { get; set; }
}