using AutoMapper;
using ChargeStationDTO = SmartCharger.Application.ChargeStations.DTOs;
using ConnectorDTO = SmartCharger.Application.Connectors.DTOs;
using CreateChargeStation = SmartCharger.Application.ChargeStations.Commands.CreateChargeStation;
using CreateConnector = SmartCharger.Application.Connectors.Commands.CreateConnector;
using CreateGroup = SmartCharger.Application.Groups.Commands.CreateGroup;
using DomainEntities = SmartCharger.Domain.Entities;
using GroupDTO = SmartCharger.Application.Groups.DTOs;

namespace SmartCharger.Application.Common.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<DomainEntities.Group, GroupDTO.Group>();
            CreateMap<CreateGroup.CreateGroupCommand, DomainEntities.Group>();

            CreateMap<DomainEntities.ChargeStation, ChargeStationDTO.ChargeStation>();
            CreateMap<CreateChargeStation.CreateChargeStationCommand, DomainEntities.ChargeStation>();

            CreateMap<DomainEntities.Connector, ConnectorDTO.Connector>();
            CreateMap<CreateConnector.CreateConnectorCommand, DomainEntities.Connector>();
        }
    }
}
