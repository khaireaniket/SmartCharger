using AutoMapper;
using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DomainEntities = SmartCharger.Domain.Entities;

namespace SmartCharger.Application.Connectors.Commands.CreateConnector
{
    public class CreateConnectorCommand : IRequest<int>
    {
        public float MaxCurrentInAmps { get; set; }
        public int ChargeStationId { get; set; }
    }

    public class CreateGroupCommandHandler : IRequestHandler<CreateConnectorCommand, int>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;
        private readonly IMapper _mapper;

        public CreateGroupCommandHandler(ISmartChargerDbContext smartChargerDbContext, IMapper mapper)
        {
            _smartChargerDbContext = smartChargerDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateConnectorCommand command, CancellationToken cancellationToken)
        {
            var connectorDomainEntity = (_mapper.Map<DomainEntities.Connector>(command));
            await _smartChargerDbContext.Connectors.AddAsync(connectorDomainEntity);

            await _smartChargerDbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(connectorDomainEntity.Id);
        }
    }
}
