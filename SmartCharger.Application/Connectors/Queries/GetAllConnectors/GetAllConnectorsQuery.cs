using AutoMapper;
using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DTO = SmartCharger.Application.Connectors.DTOs;

namespace SmartCharger.Application.Connectors.Queries.GetAllConnectors
{
    public class GetAllConnectorsQuery : IRequest<List<DTO.Connector>>
    {

    }

    public class GetAllGroupsQueryHandler : IRequestHandler<GetAllConnectorsQuery, List<DTO.Connector>>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;
        private readonly IMapper _mapper;

        public GetAllGroupsQueryHandler(ISmartChargerDbContext smartChargerDbContext, IMapper mapper)
        {
            _smartChargerDbContext = smartChargerDbContext;
            _mapper = mapper;
        }

        public Task<List<DTO.Connector>> Handle(GetAllConnectorsQuery query, CancellationToken cancellationToken)
        {
            var connectors = _smartChargerDbContext.Connectors.ToList();
            return Task.FromResult(_mapper.Map<List<DTO.Connector>>(connectors));
        }
    }
}
