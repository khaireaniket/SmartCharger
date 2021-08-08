using FluentValidation;
using SmartCharger.Application.Common.Interface;
using System.Linq;

namespace SmartCharger.Application.Common.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public readonly ISmartChargerDbContext _smartChargerDbContext;

        public BaseValidator(ISmartChargerDbContext smartChargerDbContext = default)
        {
            _smartChargerDbContext = smartChargerDbContext;
        }

        public bool CheckIfGroupExists(int groupId)
        {
            return _smartChargerDbContext.Groups.Any(a => a.Id == groupId);
        }

        public bool CheckIfChargeStationExists(int chargeStationId)
        {
            return _smartChargerDbContext.ChargeStations.Any(a => a.Id == chargeStationId);
        }

        public bool CheckIfConnectorExists(int connectorId)
        {
            return _smartChargerDbContext.Connectors.Any(a => a.Id == connectorId);
        }

        public bool CheckIfConnectorCanBeAddedToChargeStation(int chargeStationId)
        {
            return _smartChargerDbContext.ChargeStations.Any(a => a.Id == chargeStationId && a.Connectors.Count < 5);
        }

        public bool CheckIfMaxAmpsCapacityReached(int chargeStationId, float maxCurrentInAmps)
        {
            var chargeStation = _smartChargerDbContext.ChargeStations.First(a => a.Id == chargeStationId);
            var group = _smartChargerDbContext.Groups.First(a => a.Id == chargeStation.GroupId);

            var maxCapacity = group.CapacityInAmps;
            var currentCapacity = _smartChargerDbContext.Connectors.Where(a => a.ChargeStationId == chargeStationId).Sum(a => a.MaxCurrentInAmps);

            var hasCapacityReached = maxCapacity > currentCapacity + maxCurrentInAmps;

            return hasCapacityReached;
        }

        public bool CheckIfConnectorCanBeUpdate(int connectorId, float maxCurrentInAmps)
        {
            var connector = _smartChargerDbContext.Connectors.First(a => a.Id == connectorId);
            var chargeStation = _smartChargerDbContext.ChargeStations.First(a => a.Id == connector.ChargeStationId);
            var group = _smartChargerDbContext.Groups.First(a => a.Id == chargeStation.GroupId);

            var maxCapacity = group.CapacityInAmps;
            var currentCapacity = _smartChargerDbContext.Connectors.Where(a => a.Id != connectorId && a.ChargeStationId == connector.ChargeStationId).Sum(a => a.MaxCurrentInAmps);

            var hasCapacityReached = maxCapacity > currentCapacity + maxCurrentInAmps;

            return hasCapacityReached;
        }
    }
}
