using FluentValidation;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommandValidator : BaseValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator(ISmartChargerDbContext smartChargerDbContext) : base(smartChargerDbContext)
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Id).Must(CheckIfGroupExists).WithMessage("Group Id doesn't exist");
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.CapacityInAmps).GreaterThan(0);
        }
    }
}
