﻿using Microsoft.EntityFrameworkCore;
using si730pc2u20221b127.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using si730pc2u20221b127.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using si730pc2u20221b127.API.Subscriptions.Domain.Model.Entities;
using si730pc2u20221b127.API.Subscriptions.Domain.Repositories;

namespace si730pc2u20221b127.API.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

public class PlanRepository(AppDbContext context) : BaseRepository<Plan>(context), IPlanRepository
{
    public async Task<Plan?> FindByNameAsync(String name)
    {
        return await context.Plans.FirstOrDefaultAsync(p => p.Name == name);
    }
    
    public async Task<Plan?> FindByIsDefaultAsync(int isDefault)
    {
        return await context.Plans.FirstOrDefaultAsync(p => p.IsDefault == isDefault);
    }
    
}