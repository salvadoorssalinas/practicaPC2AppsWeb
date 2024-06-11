using si730pc2u20221b127.API.Subscriptions.Domain.Model.Entities;
using si730pc2u20221b127.API.Subscriptions.Domain.Model.Queries;

namespace si730pc2u20221b127.API.Subscriptions.Domain.Services;

public interface IPlanQueryService
{
    Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query);
    Task<Plan?> Handle(GetPlanByIdQuery query);
    Task<Plan?> Handle(GetPlanByNameQuery query);
    Task<Plan?> Handle(GetPlanByIsDefaultQuery query);
}