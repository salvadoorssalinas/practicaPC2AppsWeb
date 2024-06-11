using si730pc2u20221b127.API.Shared.Domain.Repositories;
using si730pc2u20221b127.API.Subscriptions.Domain.Model.Commands;
using si730pc2u20221b127.API.Subscriptions.Domain.Model.Entities;
using si730pc2u20221b127.API.Subscriptions.Domain.Repositories;
using si730pc2u20221b127.API.Subscriptions.Domain.Services;

namespace si730pc2u20221b127.API.Subscriptions.Application.Internal.CommandServices;

public class PlanCommandService(IPlanRepository planRepository, IUnitOfWork unitOfWork) : IPlanCommandService
{
    public async Task<Plan?> Handle(CreatePlanCommand command)
    {
        // Validation of MaxUsers
        if (command.MaxUsers <= 0)
        {
            throw new ArgumentException("MaxUsers must be greater than 0");
        }
        
        // Validation of IsDefault
        if (command.IsDefault != 0 && command.IsDefault != 1)
        {
            throw new ArgumentException("IsDefault must be 0 or 1");
        }

        try
        {
            var plan = new Plan(command.Name, command.MaxUsers, command.IsDefault);
            await planRepository.AddAsync(plan);
            await unitOfWork.CompleteAsync();
            return plan;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the user: {e.Message}");
            return null;
        }
    }
    
    public async Task<Plan?> Handle(UpdatePlanCommand command)
    {
        // Validation of MaxUsers
        if (command.MaxUsers <= 0)
        {
            throw new ArgumentException("MaxUsers must be greater than 0");
        }
        
        // Validation of IsDefault
        if (command.IsDefault != 0 && command.IsDefault != 1)
        {
            throw new ArgumentException("IsDefault must be 0 or 1");
        }
        
        // Validation of Plan Id
        var plan = await planRepository.FindByIdAsync(command.Id);
        if (plan == null)
        {
            throw new ArgumentException("Plan not found");
        }
        
        // Update Plan
        plan.Name = command.Name;
        plan.MaxUsers = command.MaxUsers;
        plan.IsDefault = command.IsDefault;
        await unitOfWork.CompleteAsync();
        return plan;
    }
    
    public async Task<Plan?> Handle(DeletePlanCommand command)
    {
        // Validation of Plan Id
        var plan = await planRepository.FindByIdAsync(command.Id);
        if (plan == null)
        {
            throw new ArgumentException("Plan not found");
        }
        
        // Delete Plan
        planRepository.Remove(plan);
        await unitOfWork.CompleteAsync();
        return plan;
    }
    
}