using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ConstructionCompanyVmValidator : AbstractValidator<ConstructionCompanyVm>
    {
        public ConstructionCompanyVmValidator()
        {
            RuleFor(x => x.FirmName)
                .NotEmpty().WithMessage("প্রতিষ্ঠানের নাম অবশ্যই প্রদান করতে হবে।")
                .MaximumLength(200).WithMessage("প্রতিষ্ঠানের নাম সর্বোচ্চ ২০০ অক্ষর হতে পারে।");

            RuleFor(x => x.FirmNameB)
                .NotEmpty().WithMessage("প্রতিষ্ঠানের নাম (বাংলা) অবশ্যই প্রদান করতে হবে।")
                .MaximumLength(200).WithMessage("প্রতিষ্ঠানের নাম (বাংলা) সর্বোচ্চ ২০০ অক্ষর হতে পারে।");

            RuleFor(x => x.FirmContact)
                .MaximumLength(50).WithMessage("প্রতিষ্ঠানের কন্টাক্ট সর্বোচ্চ ৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.FirmEmail)
                .EmailAddress().WithMessage("সঠিক ইমেইল প্রদান করুন।");

            RuleFor(x => x.FirmAddress)
                .MaximumLength(500).WithMessage("প্রতিষ্ঠানের ঠিকানা সর্বোচ্চ ৫০০ অক্ষর হতে পারে।");

            RuleFor(x => x.FirmAddressB)
                .MaximumLength(500).WithMessage("প্রতিষ্ঠানের ঠিকানা (বাংলা) সর্বোচ্চ ৫০০ অক্ষর হতে পারে।");

            RuleFor(x => x.AccountNumber)
                .MaximumLength(50).WithMessage("হিসাব নম্বর সর্বোচ্চ ৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.BranchName)
                .MaximumLength(50).WithMessage("শাখার নাম সর্বোচ্চ ৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.OwnerName)
                .MaximumLength(150).WithMessage("স্বত্বাধিকারীর নাম সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.OwnerNameB)
                .MaximumLength(150).WithMessage("স্বত্বাধিকারীর নাম (বাংলা) সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.OwnerDesignation)
                .MaximumLength(150).WithMessage("স্বত্বাধিকারীর পদবি সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.OwnerDesignationB)
                .MaximumLength(150).WithMessage("স্বত্বাধিকারীর পদবি (বাংলা) সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.OwnerPhone)
                .MaximumLength(15).WithMessage("স্বত্বাধিকারীর ফোন নম্বর সর্বোচ্চ ১৫ অক্ষর হতে পারে।");

            RuleFor(x => x.OwnerEmail)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.OwnerEmail))
                .WithMessage("সঠিক মালিকের ইমেইল প্রদান করুন।");

            RuleFor(x => x.FirstWitnessName)
                .MaximumLength(150).WithMessage("প্রথম সাক্ষীর নাম সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.FirstWitnessNameB)
                .MaximumLength(150).WithMessage("প্রথম সাক্ষীর নাম (বাংলা) সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.FirstWitnessDesignation)
                .MaximumLength(150).WithMessage("প্রথম সাক্ষীর পদবি সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.FirstWitnessDesignationB)
                .MaximumLength(150).WithMessage("প্রথম সাক্ষীর পদবি (বাংলা) সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.SecondWitnessName)
                .MaximumLength(150).WithMessage("দ্বিতীয় সাক্ষীর নাম সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.SecondWitnessNameB)
                .MaximumLength(150).WithMessage("দ্বিতীয় সাক্ষীর নাম (বাংলা) সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.SecondWitnessDesignation)
                .MaximumLength(150).WithMessage("দ্বিতীয় সাক্ষীর পদবি সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.SecondWitnessDesignationB)
                .MaximumLength(150).WithMessage("দ্বিতীয় সাক্ষীর পদবি (বাংলা) সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.AuthorizedPersonName)
                .MaximumLength(100).WithMessage("অনুমোদিত ব্যক্তির নাম সর্বোচ্চ ১০০ অক্ষর হতে পারে।");

            RuleFor(x => x.AuthorizedPersonNameDesignation)
                .MaximumLength(150).WithMessage("অনুমোদিত ব্যক্তির পদবি সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.AuthorizedPersonNamePhone)
                .MaximumLength(50).WithMessage("অনুমোদিত ব্যক্তির ফোন নম্বর সর্বোচ্চ ৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.LicenceNumber)
                .MaximumLength(50).WithMessage("লাইসেন্স নম্বর সর্বোচ্চ ৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.LicensingOrganization)
                .MaximumLength(150).WithMessage("লাইসেন্স সংস্থা সর্বোচ্চ ১৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.LicenceCategory)
                .MaximumLength(50).WithMessage("লাইসেন্স ক্যাটাগরি সর্বোচ্চ ৫০ অক্ষর হতে পারে।");

            RuleFor(x => x.IssueDate)
                .NotNull().WithMessage("ইস্যুর তারিখ অবশ্যই প্রদান করতে হবে।");

            RuleFor(x => x.ExpiryDate)
                .NotNull().WithMessage("মেয়াদ উত্তীর্ণের তারিখ অবশ্যই প্রদান করতে হবে।");

        }
    }
}