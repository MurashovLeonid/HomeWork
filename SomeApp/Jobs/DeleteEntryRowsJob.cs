using MediatR;
using Quartz;
using SomeApp.UseCases.Commands.EntryRows.DeleteEntryRows;

namespace SomeApp.Jobs
{
    [DisallowConcurrentExecution]
    public class DeleteEntryRowsJob : IJob
    {
        private readonly IMediator _mediator;
        public DeleteEntryRowsJob(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new DeleteEntryRowsRequest());
        }
    }
}
