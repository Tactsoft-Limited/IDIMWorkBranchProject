using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class BGBMiscellaneousFundController : BaseController
    {
        private readonly IBGBMiscellaneousFundService _bGBMiscellaneousFundService;
        private readonly IADPReceivePaymentService _aDPReceivePaymentService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IConstructionCompanyService _constructionCompanyService;
        private readonly IMapper _mapper;

        public BGBMiscellaneousFundController(IActivityLogService activityLogService,
            IBGBMiscellaneousFundService bGBMiscellaneousFundService,
            IMapper mapper,
            IADPReceivePaymentService aDPReceivePaymentService,
            IProjectWorkService projectWorkService,
            IConstructionCompanyService constructionCompanyService) : base(activityLogService)
        {
            _bGBMiscellaneousFundService = bGBMiscellaneousFundService;
            _mapper = mapper;
            _aDPReceivePaymentService = aDPReceivePaymentService;
            _projectWorkService = projectWorkService;
            _constructionCompanyService = constructionCompanyService;
        }

        // GET: BGBFund
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> CreateOrEdit(int id)
        {
            var adpPayment = await _aDPReceivePaymentService.GetByIdAsync(id);
            var projectWork = await _projectWorkService.GetByIdAsync(adpPayment.ProjectWorkId);
            var data = await _bGBMiscellaneousFundService.GetByADPPaymentReceiveIdAsync(adpPayment.ADPReceivePaymentId);

            var model = new BGBMiscellaneousFundVm();
            model.ADPReceivePaymentId = adpPayment.ADPReceivePaymentId;
            model.ProjectWorkId = projectWork.ProjectWorkId;
            model.ProjectWorkTitle = projectWork.ProjectWorkTitleB;            

            if (data != null)
            {
                model.FundId = data.FundId;
                model.LetterNo = data.LetterNo;
                model.DepositeDate = data.DepositeDate;
                model.PayOrderNo = data.PayOrderNo;
                model.PayOrderDate = data.PayOrderDate;
                model.AccountName = data.AccountName;
                model.AccountNumber = data.AccountNumber;
                model.BankName = data.BankName;
                model.BrunchName = data.BrunchName;
                model.Amount = data.Amount;
                model.Remarks = data.Remarks;               
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEdit(BGBMiscellaneousFundVm model)
        {
            try
            {
                if (model.FundId > 0)
                {
                    await _bGBMiscellaneousFundService.UpdateAsync(_mapper.Map<BGBMiscellaneousFund>(model));
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                }
                else
                {
                    await _bGBMiscellaneousFundService.CreateAsync(_mapper.Map<BGBMiscellaneousFund>(model));
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                }

                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}