using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCharger.Application.ChargeStations.Commands.DeleteChargeStation
{
    public class DeleteChargeStationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteGroupCommandHandler : IRequestHandler<DeleteChargeStationCommand, bool>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;

        public DeleteGroupCommandHandler(ISmartChargerDbContext smartChargerDbContext)
        {
            _smartChargerDbContext = smartChargerDbContext;
        }

        public async Task<bool> Handle(DeleteChargeStationCommand request, CancellationToken cancellationToken)
        {
            var chargeStation = _smartChargerDbContext.ChargeStations.FirstOrDefault(a => a.Id == request.Id);

            if (chargeStation == null)
            {
                return await Task.FromResult(false);
            }

            _smartChargerDbContext.ChargeStations.Remove(chargeStation);
            await _smartChargerDbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(true);
        }
    }
}
