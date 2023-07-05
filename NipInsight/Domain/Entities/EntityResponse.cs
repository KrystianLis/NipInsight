namespace NipInsight.Domain.Entities;

public class EntityResponse
{
    public Exception Exception { get; set; }

    public EntityItem Result { get; set; }
}