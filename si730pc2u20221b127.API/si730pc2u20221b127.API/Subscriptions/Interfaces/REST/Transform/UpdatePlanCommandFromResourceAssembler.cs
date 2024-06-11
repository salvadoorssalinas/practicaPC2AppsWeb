using si730pc2u20221b127.API.Subscriptions.Domain.Model.Commands;
using si730pc2u20221b127.API.Subscriptions.Interfaces.REST.Resources;

namespace si730pc2u20221b127.API.Subscriptions.Interfaces.REST.Transform;

public class UpdatePlanCommandFromResourceAssembler
{
    public static UpdatePlanCommand ToCommandFromResource(UpdatePlanResource resource, int planId)
    {
        return new UpdatePlanCommand(planId, resource.Name, resource.MaxUsers, resource.IsDefault);
    }
}