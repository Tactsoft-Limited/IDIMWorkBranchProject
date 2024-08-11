using FluentValidation;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Models.Validation.User
{
    public class EditInformationVmValidator : AbstractValidator<EditInformationVm>
    {
        public EditInformationVmValidator()
        {
            RuleFor(m => m.RegimentNumber).NotEmpty();
        }
    }
}