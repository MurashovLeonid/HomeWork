using MediatR;
using System.Runtime.CompilerServices;

namespace SomeApp.UseCases.Queries.TaskRows.GetTaskRowById
{
    /// <summary>
    /// Запрос на получение задачи
    /// </summary>
    public class GetTaskRowByIdRequest : IRequest<GetTaskRowByIdResponse>
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; set; }
        
    }
}
