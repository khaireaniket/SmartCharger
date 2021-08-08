using FluentValidation;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.Connectors.Commands.UpdateConnector
{
    public class UpdateConnectorCommandValidator : BaseValidator<UpdateConnectorCommand>
    {
        public UpdateConnectorCommandValidator(ISmartChargerDbContext smartChargerDbContext) : base(smartChargerDbContext)
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Id).Must(CheckIfConnectorExists).WithMessage("Connector Id doesn't exist");
            RuleFor(x => x.MaxCurrentInAmps).GreaterThan(0);
            RuleFor(x => x.Id).Must((c, connectorId) => CheckIfConnectorCanBeUpdate(connectorId, c.MaxCurrentInAmps)).WithMessage("Cannot update MaxAmpsCapacity since it will reach beyond max capacity");
        }
    }
}