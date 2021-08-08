using FluentValidation;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.Connectors.Commands.DeleteConnector
{
    public class DeleteConnectorCommandValidator : BaseValidator<DeleteConnectorCommand>
    {
        public DeleteConnectorCommandValidator(ISmartChargerDbContext smartChargerDbContext) : base(smartChargerDbContext)
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Id).Must(CheckIfConnectorExists).WithMessage("Connector Id doesn't exist");
        }
    }
}