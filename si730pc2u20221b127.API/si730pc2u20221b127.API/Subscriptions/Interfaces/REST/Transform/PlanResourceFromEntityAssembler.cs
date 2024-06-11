using si730pc2u20221b127.API.Subscriptions.Domain.Model.Entities;
using si730pc2u20221b127.API.Subscriptions.Interfaces.REST.Resources;

namespace si730pc2u20221b127.API.Subscriptions.Interfaces.REST.Transform;

public static class PlanResourceFromEntityAssembler
{
    public static PlanResource ToResourceFromEntity(Plan entity)
    {
        return new PlanResource(entity.Id, entity.Name, entity.MaxUsers, entity.IsDefault);
    }
}