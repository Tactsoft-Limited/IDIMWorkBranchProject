using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class WorkOrderVmValidator : AbstractValidator<WorkOrderVm>
    {
        public WorkOrderVmValidator()
        {
            RuleFor(x => x.LetterNo).NotEmpty().WithMessage("লেটার নং আবশ্যক");

            RuleFor(x => x.WorkOrderDate).NotEmpty().WithMessage("কার্যাদেশের তারিখ আবশ্যক");

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("কাজ শুরুর তারিখ আবশ্যক");

            RuleFor(x => x.EndDate).NotEmpty().WithMessage("কাজ শেষের তারিখ আবশ্যক");

        }
    }
}