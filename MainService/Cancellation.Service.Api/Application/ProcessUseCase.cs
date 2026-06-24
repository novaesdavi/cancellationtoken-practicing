using Cancellation.Service.Api.Infrastructure;

namespace Cancellation.Service.Api.Application
{
    public class ProcessUseCase : IProcessUseCase
    {
        IRepositoryMock _repositoryMock;
        public ProcessUseCase(IRepositoryMock repositoryMock)
        {
            _repositoryMock = repositoryMock;
        }
        public async Task<string> ExecuteAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 10; i++)
            {
                //if (cancellationToken.IsCancellationRequested)
                //{
                //    return "Process was cancelled.";
                //}
                //await Task.Delay(1000); // Simulate work
            }

            var data = await _repositoryMock.GetDataAsync(cancellationToken);

            return "Process completed successfully.";
        }
    }
}

