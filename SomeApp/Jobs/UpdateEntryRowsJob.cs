using MediatR;
using Quartz;
using SomeApp.UseCases.Commands.EntryRows.UpdateEntryRows;

namespace SomeApp.Jobs
{
    [DisallowConcurrentExecution]
    public class UpdateEntryRowsJob : IJob
    {
        private readonly IMediator _mediator;
        public UpdateEntryRowsJob(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new UpdateEntryRowsRequest());
        }
    }
}
