namespace si730pc2u20221b127.API.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}