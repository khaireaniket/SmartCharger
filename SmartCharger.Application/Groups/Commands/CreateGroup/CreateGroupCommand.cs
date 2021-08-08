using AutoMapper;
using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Threading;
using System.Threading.Tasks;
using DomainEntities = SmartCharger.Domain.Entities;

namespace SmartCharger.Application.Groups.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<int>
    {
        public string Name { get; set; }
        public float CapacityInAmps { get; set; }
    }

    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, int>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;
        private readonly IMapper _mapper;

        public CreateGroupCommandHandler(ISmartChargerDbContext smartChargerDbContext, IMapper mapper)
        {
            _smartChargerDbContext = smartChargerDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateGroupCommand command, CancellationToken cancellationToken)
        {
            var groupDomainEntity = (_mapper.Map<DomainEntities.Group>(command));
            await _smartChargerDbContext.Groups.AddAsync(groupDomainEntity);
            await _smartChargerDbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(groupDomainEntity.Id);
        }
    }
}
