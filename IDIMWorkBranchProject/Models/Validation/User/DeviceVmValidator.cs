using FluentValidation;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Models.Validation.User
{
    public class DeviceVmValidator:AbstractValidator<DeviceVm>
    {
        public DeviceVmValidator()
        {
            RuleFor(e => e.DeviceName)
                .NotEmpty();
        }
    }
}