using AutoMapper;
using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DTO = SmartCharger.Application.Groups.DTOs;

namespace SmartCharger.Application.Groups.Queries.GetAllGroups
{
    public class GetAllGroupsQuery : IRequest<List<DTO.Group>>
    {

    }

    public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, List<DTO.Group>>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;
        private readonly IMapper _mapper;

        public GetAllGroupsQueryHandler(ISmartChargerDbContext smartChargerDbContext, IMapper mapper)
        {
            _smartChargerDbContext = smartChargerDbContext;
            _mapper = mapper;
        }

        public Task<List<DTO.Group>> Handle(GetAllGroupsQuery query, CancellationToken cancellationToken)
        {
            var groups = _smartChargerDbContext.Groups.ToList();
            return Task.FromResult(_mapper.Map<List<DTO.Group>>(groups));
        }
    }
}
