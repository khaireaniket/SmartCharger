using AutoMapper;
using MediatR;
using SmartCharger.Application.Common.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DTO = SmartCharger.Application.ChargeStations.DTOs;

namespace SmartCharger.Application.ChargeStations.Queries.GetAllChargeStations
{
    public class GetAllChargeStationsQuery : IRequest<List<DTO.ChargeStation>>
    {

    }

    public class GetAllChargeStationsQueryHandler : IRequestHandler<GetAllChargeStationsQuery, List<DTO.ChargeStation>>
    {
        private readonly ISmartChargerDbContext _smartChargerDbContext;
        private readonly IMapper _mapper;

        public GetAllChargeStationsQueryHandler(ISmartChargerDbContext smartChargerDbContext, IMapper mapper)
        {
            _smartChargerDbContext = smartChargerDbContext;
            _mapper = mapper;
        }

        public Task<List<DTO.ChargeStation>> Handle(GetAllChargeStationsQuery query, CancellationToken cancellationToken)
        {
            var chargeStations = _smartChargerDbContext.ChargeStations.ToList();
            return Task.FromResult(_mapper.Map<List<DTO.ChargeStation>>(chargeStations));
        }
    }
}
