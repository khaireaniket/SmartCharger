using AutoMapper;
using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DTO = SmartCharger.Application.Groups.DTOs;

namespace SmartCharger.Application.Groups.Queries.GetGroupById
{
    public class GetGroupByIdQuery : IRequest<DTO.Group>
    {
        public int Id { get; set; }
    }

    public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, DTO.Group>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;
        private readonly IMapper _mapper;

        public GetGroupByIdQueryHandler(ISmartChargerDbContext smartChargerDbContext, IMapper mapper)
        {
            _smartChargerDbContext = smartChargerDbContext;
            _mapper = mapper;
        }

        public Task<DTO.Group> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = _smartChargerDbContext.Groups.FirstOrDefault(a => a.Id == request.Id);
            return Task.FromResult(_mapper.Map<DTO.Group>(group));
        }
    }
}
