using FluentValidation;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandValidator : BaseValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.CapacityInAmps).GreaterThan(0);
        }
    }
}
