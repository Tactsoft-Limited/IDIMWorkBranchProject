using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class RecruitmentCommitteVmValidator : AbstractValidator<RecruitmentCommitteeVm>
    {
        public RecruitmentCommitteVmValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("নাম আবশ্যক");
            RuleFor(x => x.NameB).NotEmpty().WithMessage("বাংলা নাম আবশ্যক");
            RuleFor(x => x.Designation).NotEmpty().WithMessage("পদবি আবশ্যক");
            RuleFor(x => x.DesignationB).NotEmpty().WithMessage("পদবি বাংলায় আবশ্যক");
            RuleFor(x => x.Name).NotEmpty().WithMessage("নাম আবশ্যক");
            RuleFor(x => x.Name).NotEmpty().WithMessage("নাম আবশ্যক");
        }
    }
}