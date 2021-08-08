using FluentValidation;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.Connectors.Commands.CreateConnector
{
    public class CreateConnectorCommandValidator : BaseValidator<CreateConnectorCommand>
    {
        public CreateConnectorCommandValidator(ISmartChargerDbContext smartChargerDbContext) : base(smartChargerDbContext)
        {
            RuleFor(x => x.ChargeStationId).GreaterThan(0);
            RuleFor(x => x.ChargeStationId).Must(CheckIfChargeStationExists).WithMessage("Charge Station Id doesn't exist");
            RuleFor(x => x.MaxCurrentInAmps).GreaterThan(0);
            RuleFor(x => x.ChargeStationId).Must(CheckIfConnectorCanBeAddedToChargeStation).WithMessage("Cannot add more than 5 Connectors to the Charge Station");
            RuleFor(x => x.ChargeStationId).Must((c, chargeStationId) => CheckIfMaxAmpsCapacityReached(chargeStationId, c.MaxCurrentInAmps)).WithMessage("Cannot add connector since it will reach beyond max capacity");
        }
    }
}