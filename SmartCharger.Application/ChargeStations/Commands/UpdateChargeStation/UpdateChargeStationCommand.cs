using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCharger.Application.ChargeStations.Commands.UpdateChargeStation
{
    public class UpdateChargeStationCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateGroupCommandHandler : IRequestHandler<UpdateChargeStationCommand, int>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;

        public UpdateGroupCommandHandler(ISmartChargerDbContext smartChargerDbContext)
        {
            _smartChargerDbContext = smartChargerDbContext;
        }

        public async Task<int> Handle(UpdateChargeStationCommand request, CancellationToken cancellationToken)
        {
            var chargeStation = _smartChargerDbContext.ChargeStations.FirstOrDefault(a => a.Id == request.Id);

            if (chargeStation == null)
            {
                return await Task.FromResult(0);
            }

            chargeStation.Name = request.Name;

            await _smartChargerDbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(chargeStation.Id);
        }
    }
}
