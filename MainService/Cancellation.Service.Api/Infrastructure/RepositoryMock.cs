using Refit;

namespace Cancellation.Service.Api.Infrastructure
{
    public class RepositoryMock : IRepositoryMock
    {

        IMockApi _mockApi;
        public RepositoryMock(IMockApi mockApi)
        {
            _mockApi = mockApi;
        }

        public async Task<string> GetDataAsync(CancellationToken cancellationToken)
        {

            var resultado = await _mockApi.GetWeatherforecast(cancellationToken);

            return resultado;
        }
    }
}
