using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

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
                     
            
        }
    }
}