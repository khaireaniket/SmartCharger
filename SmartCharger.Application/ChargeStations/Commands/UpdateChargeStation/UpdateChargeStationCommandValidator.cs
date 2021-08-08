using FluentValidation;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.ChargeStations.Commands.UpdateChargeStation
{
    public class UpdateChargeStationCommandValidator : BaseValidator<UpdateChargeStationCommand>
    {
        public UpdateChargeStationCommandValidator(ISmartChargerDbContext smartChargerDbContext) : base(smartChargerDbContext)
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Id).Must(CheckIfChargeStationExists).WithMessage("Charge Station Id doesn't exist");
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}