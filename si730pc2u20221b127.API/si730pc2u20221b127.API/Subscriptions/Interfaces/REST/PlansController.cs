using Microsoft.AspNetCore.Mvc;
using si730pc2u20221b127.API.Subscriptions.Domain.Model.Commands;
using si730pc2u20221b127.API.Subscriptions.Domain.Model.Queries;
using si730pc2u20221b127.API.Subscriptions.Domain.Services;
using si730pc2u20221b127.API.Subscriptions.Interfaces.REST.Resources;
using si730pc2u20221b127.API.Subscriptions.Interfaces.REST.Transform;

namespace si730pc2u20221b127.API.Subscriptions.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]

public class PlansController(IPlanCommandService planCommandService, IPlanQueryService planQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePlan([FromBody] CreatePlanResource createPlanResource)
    {
        try
        {
            var createPlanCommand = CreatePlanCommandFromResourceAssembler.ToCommandFromResource(createPlanResource);
            var plan = await planCommandService.Handle(createPlanCommand);
            if (plan == null) return BadRequest();
            var resource = PlanResourceFromEntityAssembler.ToResourceFromEntity(plan);
            return CreatedAtAction(nameof(GetPlanById), new { id = plan.Id }, resource);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(new { message = "An error occurred while creating the user. " + e.Message });
        }
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlan([FromBody] UpdatePlanResource updatePlanResource, [FromRoute] int id)
    {
        
        try
        {
            var updatePlanCommand = UpdatePlanCommandFromResourceAssembler.ToCommandFromResource(updatePlanResource, id);
            var plan = await planCommandService.Handle(updatePlanCommand);
            if (plan == null) return NotFound();
            var resource = PlanResourceFromEntityAssembler.ToResourceFromEntity(plan);
            return Ok(resource);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(new { message = "An error occurred while updating the user. " + e.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlans()
    {
        var getAllPlansQuery = new GetAllPlansQuery();
        var plans = await planQueryService.Handle(getAllPlansQuery);
        var resources = plans.Select(PlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlanById(int id)
    {
        var plan = await planQueryService.Handle(new GetPlanByIdQuery(id));
        if (plan == null) return NotFound();
        var resource = PlanResourceFromEntityAssembler.ToResourceFromEntity(plan);
        return Ok(resource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlan(int id)
    {
        var deletePlanCommand = new DeletePlanCommand(id);
        var plan = await planCommandService.Handle(deletePlanCommand);
        if (plan == null) return NotFound();
        return NoContent();
    }
    
}