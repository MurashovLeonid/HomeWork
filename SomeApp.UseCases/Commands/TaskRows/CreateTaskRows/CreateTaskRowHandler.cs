using MediatR;
using SomeApp.Infrastructure.Interfaces.RepositoryInterfaces;

namespace SomeApp.UseCases.Commands.TaskRows.CreateTaskRow
{
    public class CreateTaskRowHandler : IRequestHandler<CreateTaskRowRequest, CreateTaskRowResponse>
    {
        private readonly IRepository _rep;
        public CreateTaskRowHandler(IRepository rep)
        {
            _rep = rep ?? throw new ArgumentNullException(nameof(rep));
        }
        public async Task<CreateTaskRowResponse> Handle(CreateTaskRowRequest request, CancellationToken cancellationToken)
        {

            var result = await _rep.CreateTaskRowAsync(request.CreateTaskRowDto);
            return new CreateTaskRowResponse() { IsSuccess = result.FailureInfo == null ? true : false, TaskRow = result };

        }
    }
}
