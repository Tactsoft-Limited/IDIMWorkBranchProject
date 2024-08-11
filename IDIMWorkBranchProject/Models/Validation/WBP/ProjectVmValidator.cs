using FluentValidation;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Models.Validation.WBP
{
    public class ProjectVmValidator : AbstractValidator<ProjectVm>
    {
        public ProjectVmValidator()
        {
            RuleFor(m=>m.DevelopmentTypeId)
                .NotEmpty()
                .WithMessage("Budget Type required");
        }
    }
}