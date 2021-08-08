using AutoMapper;
using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DTO = SmartCharger.Application.ChargeStations.DTOs;

namespace SmartCharger.Application.ChargeStations.Queries.GetChargeStationById
{
    public class GetChargeStationByIdQuery : IRequest<DTO.ChargeStation>
    {
        public int Id { get; set; }
    }

    public class GetGroupByIdQueryHandler : IRequestHandler<GetChargeStationByIdQuery, DTO.ChargeStation>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;
        private readonly IMapper _mapper;

        public GetGroupByIdQueryHandler(ISmartChargerDbContext smartChargerDbContext, IMapper mapper)
        {
            _smartChargerDbContext = smartChargerDbContext;
            _mapper = mapper;
        }

        public Task<DTO.ChargeStation> Handle(GetChargeStationByIdQuery request, CancellationToken cancellationToken)
        {
            var chargeStation = _smartChargerDbContext.ChargeStations.FirstOrDefault(a => a.Id == request.Id);
            return Task.FromResult(_mapper.Map<DTO.ChargeStation>(chargeStation));
        }
    }
}
