using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ContractAgreementVmValidator : AbstractValidator<ContractAgreementVm>
    {
        public ContractAgreementVmValidator()
        {
            RuleFor(x => x.AgreementDate)
               .NotEmpty().WithMessage("চুক্তির তারিখ আবশ্যক।");
            RuleFor(x => x.ConstructionCompanyId)
               .NotEmpty().WithMessage("ঠিকাদার প্রতিষ্ঠান আবশ্যক।");
            RuleFor(x => x.AddDGId)
               .NotEmpty().WithMessage("অতিরিক্ত মহাপরিচালক আবশ্যক।");
            RuleFor(x => x.DDGId)
               .NotEmpty().WithMessage("উপ-মহাপরিচালক আবশ্যক।");
            RuleFor(x => x.ProjectDirectorId)
               .NotEmpty().WithMessage("প্রকল্প পরিচালক আবশ্যক।");
            RuleFor(x => x.DirectorId)
               .NotEmpty().WithMessage("পরিচালক আবশ্যক।");
            RuleFor(x => x.ScanDocument)
               .NotEmpty().WithMessage("চুক্তি ডকুমেন্ট আবশ্যক।");
            
        }
    }
}