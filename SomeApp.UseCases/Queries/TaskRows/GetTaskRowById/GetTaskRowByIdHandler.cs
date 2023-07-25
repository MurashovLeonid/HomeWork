using MediatR;
using SomeApp.Infrastructure.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeApp.UseCases.Queries.TaskRows.GetTaskRowById
{
    internal class GetTaskRowByIdHandler : IRequestHandler<GetTaskRowByIdRequest, GetTaskRowByIdResponse>
    {
        private readonly IRepository _rep;
        public GetTaskRowByIdHandler(IRepository rep)
        {
            _rep = rep ?? throw new ArgumentNullException(nameof(rep));
        }

        public async Task<GetTaskRowByIdResponse> Handle(GetTaskRowByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _rep.GetTaskRowByIdAsync(request.Id);

                return new GetTaskRowByIdResponse() { TaskRowDto = response, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new GetTaskRowByIdResponse() { IsSuccess = false, FailureInfo = ex.Message };
            }
        }
    }
}
