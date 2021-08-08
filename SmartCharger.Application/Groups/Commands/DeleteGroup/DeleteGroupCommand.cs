using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCharger.Application.Groups.Commands.DeleteGroup
{
    public class DeleteGroupCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, bool>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;

        public DeleteGroupCommandHandler(ISmartChargerDbContext smartChargerDbContext)
        {
            _smartChargerDbContext = smartChargerDbContext;
        }

        public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = _smartChargerDbContext.Groups
                                              .Include(a => a.ChargeStations)
                                              .FirstOrDefault(a => a.Id == request.Id);

            if (group == null)
            {
                return await Task.FromResult(false);
            }

            _smartChargerDbContext.ChargeStations.RemoveRange(group.ChargeStations);
            _smartChargerDbContext.Groups.Remove(group);
            await _smartChargerDbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(true);
        }
    }
}
