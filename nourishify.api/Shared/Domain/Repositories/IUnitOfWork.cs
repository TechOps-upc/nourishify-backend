namespace nourishify.api.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}