using MediatR;
using Microsoft.AspNetCore.Mvc;
using SomeApp.UseCases.Commands.TaskRows.CreateTaskRow;
using SomeApp.UseCases.Commands.TaskRows.DeleteTaskRow;
using SomeApp.UseCases.Queries.TaskRows.GetTaskRowById;


namespace SomeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskRowsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaskRowsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }


        [HttpPost]
        public async Task<IActionResult> CreateTaskRowAsync([FromBody] CreateTaskRowRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                return StatusCode(202, result.TaskRow);
            }
            else
            {               
                return StatusCode(409, result.TaskRow);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateTaskRowAsync([FromQuery] GetTaskRowByIdRequest request)
        {
            var response = await _mediator.Send(request); 
            if (response.IsSuccess)
            {
                return StatusCode(202, response.TaskRowDto);
            }
            else
            {
                return StatusCode(400, response.FailureInfo);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTaskRowAsync([FromQuery] DeleteTaskRowRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                return StatusCode(202, result.TaskRow);
            }
            else
            {
                return StatusCode(409, result.TaskRow);
            }
        }

    }
}
