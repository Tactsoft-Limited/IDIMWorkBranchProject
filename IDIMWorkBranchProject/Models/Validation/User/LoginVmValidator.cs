using FluentValidation;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Models.Validation.User
{
    public class LoginVmValidator:AbstractValidator<LoginVm>
    {
        public LoginVmValidator()
        {
            RuleFor(e => e.Username)
                .NotEmpty();
        }
    }
}