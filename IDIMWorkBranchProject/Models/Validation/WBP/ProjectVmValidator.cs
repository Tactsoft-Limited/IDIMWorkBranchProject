using FluentValidation;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Models.Validation.WBP
{
    public class ProjectVmValidator : AbstractValidator<ProjectVm>
    {
        public ProjectVmValidator()
        {
            RuleFor(m => m.ProjectName)
                .NotEmpty()
                .WithMessage("Project Name required");
        }
    }
}