using MediatR;
using SomeApp.Infrastructure.Interfaces.RepositoryInterfaces;

namespace SomeApp.UseCases.Commands.EntryRows.DeleteEntryRows
{
    /// <summary>
    /// Обработчик по выполнению пула задач, связанных с добавлением записей
    /// </summary>
    internal class DeleteEntryRowsHandler : IRequestHandler<DeleteEntryRowsRequest>
    {
        private readonly IRepository _rep;
        public DeleteEntryRowsHandler(IRepository rep)
        {
            _rep = rep ?? throw new ArgumentNullException(nameof(rep));
        }
        public async Task Handle(DeleteEntryRowsRequest request, CancellationToken cancellationToken)
        {
            await _rep.DeleteEntryRowsAsync();
        }
    }
}
