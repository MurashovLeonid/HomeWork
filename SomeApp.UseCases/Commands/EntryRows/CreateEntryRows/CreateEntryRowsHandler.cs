using MediatR;
using SomeApp.Infrastructure.Interfaces.RepositoryInterfaces;

namespace SomeApp.UseCases.Commands.EntryRows.CreateEntryRows
{
    /// <summary>
    /// Обработчик по выполнению пула задач, связанных с добавлением записей
    /// </summary>
    internal class CreateEntryRowsHandler : IRequestHandler<CreateEntryRowsRequest>
    {
        private readonly IRepository _rep;
        public CreateEntryRowsHandler(IRepository rep)
        {
            _rep = rep ?? throw new ArgumentNullException(nameof(rep));
        }
        public async Task Handle(CreateEntryRowsRequest request, CancellationToken cancellationToken)
        {
            await _rep.CreateEntryRowsAsync();
        }
    }
}
