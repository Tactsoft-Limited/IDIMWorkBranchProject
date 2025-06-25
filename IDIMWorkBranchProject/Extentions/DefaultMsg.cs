namespace IDIMWorkBranchProject.Extentions
{
    public class DefaultMsg
    {

        #region Common Message
        public const string NullObjectReference = "Null object reference error occured.";

        public const string LoginSuccess = "Welcome back, {0}! You have successfully logged in.";
        public const string LogoutSuccess = "You have been successfully logged out. We hope to see you again soon!";
        public const string RegisterSuccess = "Congratulations, {0}! Your registration was successful.";

        public const string SaveSuccess = "The {0} was saved successfully.";
        public const string UpdateSuccess = "The {0} was updated successfully.";
        public const string DeleteSuccess = "The {0} was deleted successfully.";

        public const string SaveFailed = "Unable to save {0}. Error: {1}.";
        public const string UpdateFailed = "Unable to update {0}. Error: {1}.";
        public const string DeleteFailed = "Unable to delete {0}. Error: {1}.";

        public const string DeleteOperationNotValid = "Cannot delete {0} because it is referenced in {1}.";
        public const string NotFound = "The {0} was not found.";
        public const string Required = "The {0} is required.";
        public const string SendMail = "If an account with that email address {0} exists, a verification email has been sent. Please check your email.";

        public const string Lockout = "Your account {0} has been locked due to too many failed login attempts. Please try again later or contact support if you need immediate assistance.";

        #endregion

        #region Validation Messages
        public const string DateValidation = "The {0} cannot be later than {1}.";
        public const string DateRequiredValidation = "At least one {0} and {1} is required.";
        public const string QueueEmpty = "No pending data found for this entry.";
        public const string GetNextInvalid = "The request to get the next item is invalid.";
        public const string AlreadyExists = "The name <strong>\"{0}\"</strong> is already in use. Please choose a different name.";
        public const string PleaseEnterUniqueName = "Please enter a unique name for the {0}.";
        public const string NotValid = "{0} is not valid.";
        public const string NotAvailable = "{0} is not available.";
        public const string InvalidDateInput = "Invalid date input. Please check the effective date, current date, and inactive date.";
        public const string InvalidValue = "The value provided is invalid.";
        public const string InvalidInput = "Invalid input. Please review the provided information and try again.";
        public const string NotExist = "{0} does not exist.";
        public const string PolicyCertificateInvalid = "The combination of policy number and certificate number is invalid.";
        public const string ContainsNonActive = "{0} contains non-active {1}.";
        public const string SendToQueueRequestInvalid = "The request to send to the {0} queue is invalid.";
        public const string ImportedSuccessfully = "{0} ({1} data item(s)) imported successfully.";
        public const string AlreadyExistsImport = "{0} already exists. Please correct the data and import again. Existing data: {1}.";
        public const string AlreadyAssigned = "{0} is already assigned.";
        public const string NotValidInCertificate = "{0} is not valid for certificate number {1}.";
        public const string CouldNotFetchFromLifeline = "Data could not be fetched from Lifeline.";
        public const string SomethingWentWrong = "Something went wrong.";
        public const string OrganizationExistsInGroupPolicy = "This organization is already listed in the group policy.";
        public const string DateOverlapped = "Date range overlapped!";
        public const string CashValueNoDataInLifeline = "Cash Value - No data found in Lifeline.";
        public const string SurrenderValueNoDataInLifeline = "Surrender Value - No data found in Lifeline.";
        public const string BenefitCalculationNoDataInLifeline = "Benefit Calculation - No data found in Lifeline.";
        public const string NPolicyInfoNoDataInLifeline = "N Policy Data - No data found in Lifeline.";
        public const string LoanAplValueNoDataInLifeline = "Loan Application Value - No data found in Lifeline.";
        public const string LoanQuotationValueNoDataInLifeline = "Loan Quotation Value - No data found in Lifeline.";

        #endregion
    }

}