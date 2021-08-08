using FluentValidation;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.ChargeStations.Commands.DeleteChargeStation
{
    public class DeleteChargeStationCommandValidator : BaseValidator<DeleteChargeStationCommand>
    {
        public DeleteChargeStationCommandValidator(ISmartChargerDbContext smartChargerDbContext) : base(smartChargerDbContext)
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Id).Must(CheckIfChargeStationExists).WithMessage("Charge Station Id doesn't exist");
        }
    }
}