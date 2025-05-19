using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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