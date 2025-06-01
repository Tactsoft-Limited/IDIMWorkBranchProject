using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ProjectWorkStatusVmValidator : AbstractValidator<ProjectWorkStatusVm>
    {
        public ProjectWorkStatusVmValidator()
        {
            RuleFor(x => x.StatusTypeId).NotEmpty().WithMessage("প্রকল্পের অবস্থা আবশ্যক");
        }
    }
}