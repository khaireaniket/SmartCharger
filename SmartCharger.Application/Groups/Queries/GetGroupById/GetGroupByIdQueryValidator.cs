using FluentValidation;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Application.Common.Validators;

namespace SmartCharger.Application.Groups.Queries.GetGroupById
{
    public class GetGroupByIdQueryValidator : BaseValidator<GetGroupByIdQuery>
    {
        public GetGroupByIdQueryValidator(ISmartChargerDbContext smartChargerDbContext) : base(smartChargerDbContext)
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Id).Must(CheckIfGroupExists).WithMessage("Group Id doesn't exist");
        }
    }
}
