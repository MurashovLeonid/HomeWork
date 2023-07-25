using MediatR;
using Quartz;
using SomeApp.UseCases.Commands.EntryRows.CreateEntryRows;

namespace SomeApp.Jobs
{
    [DisallowConcurrentExecution]
    public class CreateEntryRowsJob : IJob
    {
        private readonly IMediator _mediator;
        public CreateEntryRowsJob(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new CreateEntryRowsRequest());
        }
    }
}
