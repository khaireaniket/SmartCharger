using FluentValidation;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.Groups.Commands.DeleteGroup
{
    public class DeleteGroupCommandValidator : BaseValidator<DeleteGroupCommand>
    {
        public DeleteGroupCommandValidator(ISmartChargerDbContext smartChargerDbContext) : base(smartChargerDbContext)
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Id).Must(CheckIfGroupExists).WithMessage("Group Id doesn't exist");
        }
    }
}
