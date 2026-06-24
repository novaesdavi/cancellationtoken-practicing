namespace Cancellation.Service.Api.Infrastructure
{
    public interface IRepositoryMock
    {
        Task<string> GetDataAsync(CancellationToken cancellationToken);
    }
}
