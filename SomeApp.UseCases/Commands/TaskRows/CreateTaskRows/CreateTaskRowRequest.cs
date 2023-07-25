using MediatR;
using SomeApp.Contracts.Dto.TaskRows;

namespace SomeApp.UseCases.Commands.TaskRows.CreateTaskRow
{
    /// <summary>
    /// Запрос на создание задачи
    /// </summary>
    public class CreateTaskRowRequest : IRequest<CreateTaskRowResponse>
    {
        public CreateTaskRowDto CreateTaskRowDto { get; set; }
    }
}
