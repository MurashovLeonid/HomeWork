using MediatR;
using SomeApp.Infrastructure.Interfaces.RepositoryInterfaces;

namespace SomeApp.UseCases.Commands.EntryRows.UpdateEntryRows
{
    /// <summary>
    /// Обработчик по выполнению пула задач, связанных с добавлением записей
    /// </summary>
    internal class UpdateEntryRowsHandler : IRequestHandler<UpdateEntryRowsRequest>
    {
        private readonly IRepository _rep;
        public UpdateEntryRowsHandler(IRepository rep)
        {
            _rep = rep ?? throw new ArgumentNullException(nameof(rep));
        }
        public async Task Handle(UpdateEntryRowsRequest request, CancellationToken cancellationToken)
        {
            await _rep.UpdateEntryRowsAsync();
        }
    }
}
