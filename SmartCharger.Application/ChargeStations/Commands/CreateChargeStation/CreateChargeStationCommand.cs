using AutoMapper;
using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Threading;
using System.Threading.Tasks;
using DomainEntities = SmartCharger.Domain.Entities;

namespace SmartCharger.Application.ChargeStations.Commands.CreateChargeStation
{
    public class CreateChargeStationCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int GroupId { get; set; }
    }

    public class CreateGroupCommandHandler : IRequestHandler<CreateChargeStationCommand, int>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;
        private readonly IMapper _mapper;

        public CreateGroupCommandHandler(ISmartChargerDbContext smartChargerDbContext, IMapper mapper)
        {
            _smartChargerDbContext = smartChargerDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateChargeStationCommand command, CancellationToken cancellationToken)
        {
            var chargeStationDomainEntity = (_mapper.Map<DomainEntities.ChargeStation>(command));
            await _smartChargerDbContext.ChargeStations.AddAsync(chargeStationDomainEntity);
            await _smartChargerDbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(chargeStationDomainEntity.Id);
        }
    }
}
