namespace si730pc2u20221b127.API.Subscriptions.Domain.Model.Commands;

public record CreatePlanCommand(String Name, int MaxUsers, int IsDefault);