using Cancellation.Service.Api.Application;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Cancellation.Service.Api
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessUseCase _processUseCase;
        public ProcessController(IProcessUseCase processUseCase)
        {
            _processUseCase = processUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetProcess(CancellationToken cancellationToken)
        {
            var result = await _processUseCase.ExecuteAsync(cancellationToken);
            Console.WriteLine(result);
            return Ok(result);
        }

    }
    
}
