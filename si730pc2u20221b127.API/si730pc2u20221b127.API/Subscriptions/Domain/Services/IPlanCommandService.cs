using si730pc2u20221b127.API.Subscriptions.Domain.Model.Commands;
using si730pc2u20221b127.API.Subscriptions.Domain.Model.Entities;

namespace si730pc2u20221b127.API.Subscriptions.Domain.Services;

public interface IPlanCommandService
{
    Task<Plan?> Handle(CreatePlanCommand command);
    Task<Plan?> Handle(UpdatePlanCommand command);
    Task<Plan?> Handle(DeletePlanCommand command);
}