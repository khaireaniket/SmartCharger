using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCharger.Application.Connectors.Commands.DeleteConnector
{
    public class DeleteConnectorCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteGroupCommandHandler : IRequestHandler<DeleteConnectorCommand, bool>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;

        public DeleteGroupCommandHandler(ISmartChargerDbContext smartChargerDbContext)
        {
            _smartChargerDbContext = smartChargerDbContext;
        }

        public async Task<bool> Handle(DeleteConnectorCommand request, CancellationToken cancellationToken)
        {
            var connector = _smartChargerDbContext.Connectors.FirstOrDefault(a => a.Id == request.Id);

            if (connector == null)
            {
                return await Task.FromResult(false);
            }

            _smartChargerDbContext.Connectors.Remove(connector);
            await _smartChargerDbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(true);
        }
    }
}
