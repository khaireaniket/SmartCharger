using FluentValidation;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.ChargeStations.Commands.CreateChargeStation
{
    public class CreateChargeStationCommandValidator : BaseValidator<CreateChargeStationCommand>
    {
        public CreateChargeStationCommandValidator(ISmartChargerDbContext smartChargerDbContext) : base(smartChargerDbContext)
        {
            RuleFor(x => x.GroupId).GreaterThan(0);
            RuleFor(x => x.GroupId).Must(CheckIfGroupExists).WithMessage("Group Id doesn't exist");
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
