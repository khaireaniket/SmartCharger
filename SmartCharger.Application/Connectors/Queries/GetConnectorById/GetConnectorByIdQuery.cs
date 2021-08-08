using AutoMapper;
using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DTO = SmartCharger.Application.Connectors.DTOs;

namespace SmartCharger.Application.Connectors.Queries.GetConnectorById
{
    public class GetConnectorByIdQuery : IRequest<DTO.Connector>
    {
        public int Id { get; set; }
    }

    public class GetGroupByIdQueryHandler : IRequestHandler<GetConnectorByIdQuery, DTO.Connector>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;
        private readonly IMapper _mapper;

        public GetGroupByIdQueryHandler(ISmartChargerDbContext smartChargerDbContext, IMapper mapper)
        {
            _smartChargerDbContext = smartChargerDbContext;
            _mapper = mapper;
        }

        public Task<DTO.Connector> Handle(GetConnectorByIdQuery request, CancellationToken cancellationToken)
        {
            var connector = _smartChargerDbContext.Connectors.FirstOrDefault(a => a.Id == request.Id);
            return Task.FromResult(_mapper.Map<DTO.Connector>(connector));
        }
    }
}