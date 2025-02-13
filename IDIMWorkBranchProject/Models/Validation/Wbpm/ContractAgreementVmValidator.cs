using BGB.Data.Entities.Wbpm;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ContractAgreementVmValidator : AbstractValidator<ContractAgreement>
    {
        public ContractAgreementVmValidator()
        {
            RuleFor(x => x.ProjectWorkId)
            .NotEmpty()
            .WithMessage("নির্মান কাজের নামটি অবশ্যই প্রদান করতে হবে।");
            
            RuleFor(x => x.ContractDay)
            .NotEmpty()
            .WithMessage("চুক্তির দিন অবশ্যই প্রদান করতে হবে।");
            
            RuleFor(x => x.ContractDate)
            .NotEmpty()
            .WithMessage("চুক্তির তারিখ অবশ্যই প্রদান করতে হবে।");

            RuleFor(x => x.ConstructionCompanyId)
            .NotEmpty()
            .WithMessage("ঠিকাদার প্রতিষ্ঠান অবশ্যই প্রদান করতে হবে।");

            RuleFor(x => x.AgreementCost)
            .NotEmpty()
            .WithMessage("চুক্তির পরিমান অবশ্যই প্রদান করতে হবে।");

            RuleFor(x => x.AgreementCostInWord)
            .NotEmpty()
            .WithMessage("চুক্তির পরিমান কথায় অবশ্যই লিখতে হবে।");


        }
    }
}