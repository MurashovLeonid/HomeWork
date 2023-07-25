using MediatR;
using SomeApp.Contracts.Dto.TaskRows;

namespace SomeApp.UseCases.Commands.TaskRows.DeleteTaskRow
{
    /// <summary>
    /// Запрос на удаление задачи
    /// </summary>
    public class DeleteTaskRowRequest : IRequest<DeleteTaskRowResponse>
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; set; }
    }
}
