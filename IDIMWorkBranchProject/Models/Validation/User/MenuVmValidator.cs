using FluentValidation;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Models.Validation.User
{
    public class MenuVmValidator:AbstractValidator<MenuVm>
    {
        public MenuVmValidator()
        {
            RuleFor(e => e.ApplicationId)
                .NotEmpty();
            RuleFor(e => e.Title)
                .NotEmpty();
            RuleFor(e => e.ControllerName)
                .NotEmpty();
        }
    }
}