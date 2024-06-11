using si730pc2u20221b127.API.Subscriptions.Domain.Model.Entities;
using si730pc2u20221b127.API.Subscriptions.Domain.Model.Queries;
using si730pc2u20221b127.API.Subscriptions.Domain.Repositories;
using si730pc2u20221b127.API.Subscriptions.Domain.Services;

namespace si730pc2u20221b127.API.Subscriptions.Application.Internal.QueryServices;

public class PlanQueryService(IPlanRepository planRepository) : IPlanQueryService
{
    public async Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query)
    {
        return await planRepository.ListAsync();
    }
    
    public async Task<Plan?> Handle(GetPlanByIdQuery query)
    {
        return await planRepository.FindByIdAsync(query.Id);
    }
}