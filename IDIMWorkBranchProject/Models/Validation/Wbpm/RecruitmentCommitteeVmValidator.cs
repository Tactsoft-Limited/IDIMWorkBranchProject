using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class RecruitmentCommitteeVmValidator : AbstractValidator<RecruitmentCommitteeVm>
    {
        public RecruitmentCommitteeVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("নাম অবশ্যই দিতে হবে");
            RuleFor(x => x.NameB)
                .NotEmpty().WithMessage("বাংলা নাম অবশ্যই দিতে হবে");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("শিরোনাম অবশ্যই দিতে হবে");
            RuleFor(x => x.TitleB)
                .NotEmpty().WithMessage("শিরোনাম বাংলায় অবশ্যই দিতে হবে");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("ঠিকানা অবশ্যই দিতে হবে");
            RuleFor(x => x.AddressB)
                .NotEmpty().WithMessage("ঠিকানা বাংলায় অবশ্যই দিতে হবে");
            RuleFor(x => x.Designation)
                .NotEmpty().WithMessage("পদবি অবশ্যই দিতে হবে");
            RuleFor(x => x.DesignationB)
                .NotEmpty().WithMessage("পদবি বাংলায় অবশ্যই দিতে হবে");
        }
    }
}