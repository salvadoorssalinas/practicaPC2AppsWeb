namespace si730pc2u20221b127.API.Subscriptions.Domain.Model.Entities;

public class Plan
{

    public Plan()
    {
        Name = string.Empty;
    }

    public Plan(String name, int maxUsers, int isDefault)
    {
        Name = name;
        MaxUsers = maxUsers;
        IsDefault = isDefault;
    }
    
    public int Id { get; set; }
    public String Name { get; set; }
    public int MaxUsers { get; set; }
    public int IsDefault { get; set; }
}