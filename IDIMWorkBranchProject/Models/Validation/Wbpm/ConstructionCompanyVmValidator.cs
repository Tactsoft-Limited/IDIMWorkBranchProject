using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ConstructionCompanyVmValidator : AbstractValidator<ConstructionCompanyVm>
    {
        public ConstructionCompanyVmValidator()
        {
            RuleFor(x => x.FirmName)
                .NotEmpty().WithMessage("ফার্মের নাম প্রদান করুন")
                .MaximumLength(200).WithMessage("ফার্মের নাম সর্বাধিক 200 অক্ষর হতে হবে");

            RuleFor(x => x.FirmNameB)
                .NotEmpty().WithMessage("ফার্মের নাম (বাংলা) প্রদান করুন")
                .MaximumLength(200).WithMessage("ফার্মের নাম (বাংলা) সর্বাধিক 200 অক্ষর হতে হবে");

            RuleFor(x => x.ContactPerson)
                .NotEmpty().WithMessage("যোগাযোগ ব্যক্তির নাম প্রদান করুন")
                .MaximumLength(100).WithMessage("যোগাযোগ ব্যক্তির নাম সর্বাধিক 100 অক্ষর হতে হবে");

            RuleFor(x => x.ContactPersonB)
               .NotEmpty().WithMessage("যোগাযোগ ব্যক্তির নাম (বাংলা) প্রদান করুন")
               .MaximumLength(150).WithMessage("যোগাযোগ ব্যক্তির নাম (বাংলা) সর্বাধিক 150 অক্ষর হতে হবে");

            RuleFor(x => x.ContactPhone)
                .NotEmpty().WithMessage("ফোন নম্বর প্রদান করুন")
                .MaximumLength(50).WithMessage("ফোন নম্বর সর্বাধিক 50 অক্ষর হতে হবে");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("ভুল ইমেইল ঠিকানা প্রদান করা হয়েছে");

            RuleFor(x => x.FirmAddress)
                .NotEmpty().WithMessage("ফার্মের ঠিকানা প্রদান করুন")
                .MaximumLength(500).WithMessage("ফার্মের ঠিকানা সর্বাধিক 500 অক্ষর হতে হবে");

            RuleFor(x => x.FirmAddressB)
                .NotEmpty().WithMessage("ফার্মের ঠিকানা (বাংলা) প্রদান করুন")
                .MaximumLength(500).WithMessage("ফার্মের ঠিকানা (বাংলা) সর্বাধিক 500 অক্ষর হতে হবে");
        }
    }
}