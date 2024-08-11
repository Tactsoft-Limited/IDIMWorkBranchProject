using FluentValidation;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Models.Validation.User
{
    public class UserVmValidator : AbstractValidator<UserVm>
    {
        public UserVmValidator()
        {
            RuleFor(e => e.Username)
                .NotEmpty();
        }
    }
}