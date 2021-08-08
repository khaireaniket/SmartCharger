using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCharger.Application.Connectors.Commands.UpdateConnector
{
    public class UpdateConnectorCommand : IRequest<int>
    {
        public int Id { get; set; }
        public float MaxCurrentInAmps { get; set; }
    }

    public class UpdateGroupCommandHandler : IRequestHandler<UpdateConnectorCommand, int>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;

        public UpdateGroupCommandHandler(ISmartChargerDbContext smartChargerDbContext)
        {
            _smartChargerDbContext = smartChargerDbContext;
        }

        public async Task<int> Handle(UpdateConnectorCommand request, CancellationToken cancellationToken)
        {
            var connector = _smartChargerDbContext.Connectors.FirstOrDefault(a => a.Id == request.Id);

            if (connector == null)
            {
                return await Task.FromResult(0);
            }

            connector.MaxCurrentInAmps = request.MaxCurrentInAmps;

            await _smartChargerDbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(connector.Id);
        }
    }
}
