using MediatR;
using SomeApp.Infrastructure.Interfaces.RepositoryInterfaces;

namespace SomeApp.UseCases.Commands.TaskRows.DeleteTaskRow
{
    internal class DeleteTaskRowHandler : IRequestHandler<DeleteTaskRowRequest, DeleteTaskRowResponse>
    {
        private readonly IRepository _rep;
        public DeleteTaskRowHandler(IRepository rep)
        {
            _rep = rep ?? throw new ArgumentNullException(nameof(rep));
        }
        public async Task<DeleteTaskRowResponse> Handle(DeleteTaskRowRequest request, CancellationToken cancellationToken)
        {

            var result = await _rep.DeleteTaskRowAsync(request.Id);
            return new DeleteTaskRowResponse() { IsSuccess = result.FailureInfo == null ? true : false, TaskRow = result };
        }
    }
}
