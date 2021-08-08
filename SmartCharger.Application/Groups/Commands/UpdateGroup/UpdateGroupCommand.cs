using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCharger.Application.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float CapacityInAmps { get; set; }
    }

    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, int>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;

        public UpdateGroupCommandHandler(ISmartChargerDbContext smartChargerDbContext)
        {
            _smartChargerDbContext = smartChargerDbContext;
        }

        public async Task<int> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = _smartChargerDbContext.Groups.FirstOrDefault(a => a.Id == request.Id);

            if (group == null)
            {
                return await Task.FromResult(0);
            }

            group.Name = request.Name;
            group.CapacityInAmps = request.CapacityInAmps;

            await _smartChargerDbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(group.Id);
        }
    }
}
