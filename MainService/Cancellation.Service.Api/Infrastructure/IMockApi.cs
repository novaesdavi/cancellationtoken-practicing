using Refit;

namespace Cancellation.Service.Api.Infrastructure
{
    public interface IMockApi
    {

        [Get("/weatherforecast")]
        Task<string> GetWeatherforecast(CancellationToken cancellationToken);
    }
}
