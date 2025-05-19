using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class SignatoryAuthorityVmValidator : AbstractValidator<SignatoryAuthorityVm>
    {
        public SignatoryAuthorityVmValidator()
        {
            RuleFor(x=>x.AuthorityName).NotEmpty().WithMessage("নাম আবশ্যক");
            RuleFor(x => x.AuthorityNameB).NotEmpty().WithMessage("নাম বাংলা আবশ্যক");
            RuleFor(x => x.Designation).NotEmpty().WithMessage("পদবী আবশ্যক");
            RuleFor(x => x.DesignationB).NotEmpty().WithMessage("পদবী বাংলা আবশ্যক");
        }
    }
}