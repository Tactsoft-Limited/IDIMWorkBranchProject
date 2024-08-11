using FluentValidation;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Models.Validation.User
{
    public class ApplicationVmValidator: AbstractValidator<ApplicationVm>
    {
        public ApplicationVmValidator()
        {
            RuleFor(e => e.ApplicationName)
                .NotEmpty();
        }
    }
}