namespace Cancellation.Service.Api.Application
{
    public interface IProcessUseCase
    {
        Task<string> ExecuteAsync(CancellationToken cancellationToken);
    }
}
