USE [IDIMDB]
GO
/****** Object:  Table [wbpm].[ConstructionCompany]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[ConstructionCompany](
	[ConstructionCompanyId] [int] IDENTITY(1,1) NOT NULL,
	[FirmName] [nvarchar](200) NOT NULL,
	[FirmNameB] [nvarchar](200) NOT NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[ContactPersonB] [nvarchar](150) NULL,
	[ContactPhone] [nvarchar](50) NULL,
	[Email] [nvarchar](255) NULL,
	[FirmAddress] [nvarchar](500) NULL,
	[FirmAddressB] [nvarchar](500) NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ConstructionCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wbpm].[ProjectWork]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[ProjectWork](
	[ProjectWorkId] [int] IDENTITY(1,1) NOT NULL,
	[ADPProjectId] [int] NOT NULL,
	[ConstructionCompanyId] [int] NOT NULL,
	[ProjectWorkTitle] [nvarchar](500) NULL,
	[EstimatedCost] [decimal](20, 2) NULL,
	[NOADate] [datetime] NOT NULL,
	[AgreementDate] [datetime] NOT NULL,
	[WorkOrderDate] [datetime] NOT NULL,
	[WorkStartDate] [datetime] NOT NULL,
	[WorkEndDate] [datetime] NOT NULL,
	[OnFieldStartDate] [datetime] NULL,
	[OnFieldEndDate] [datetime] NULL,
	[HandOverDate] [datetime] NOT NULL,
	[BankGuaranteeEndDate] [datetime] NOT NULL,
	[ConstructionProgress] [float] NOT NULL,
	[BankGuaranteeDocument] [nvarchar](255) NOT NULL,
	[NOADocument] [nvarchar](255) NOT NULL,
	[AgreementDocument] [nvarchar](255) NOT NULL,
	[WorkOrderDocument] [nvarchar](255) NOT NULL,
	[WorkStatus] [nvarchar](100) NOT NULL,
	[Remarks] [nvarchar](300) NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectWorkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wbpm].[ADPReceivePayment]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[ADPReceivePayment](
	[ADPReceivePaymentId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectWorkId] [int] NOT NULL,
	[LetterNo] [nvarchar](150) NOT NULL,
	[BillNumber] [int] NOT NULL,
	[BillDate] [datetime] NOT NULL,
	[ExtraTime] [datetime] NULL,
	[BillPaymentSector] [nvarchar](200) NOT NULL,
	[ActualWorkProgressPer] [float] NOT NULL,
	[FinancialProgressPer] [float] NOT NULL,
	[BillPaidPer] [float] NOT NULL,
	[BillPaidAmount] [decimal](20, 2) NOT NULL,
	[TaxPer] [float] NOT NULL,
	[TaxAmount] [decimal](20, 2) NOT NULL,
	[VatPer] [float] NOT NULL,
	[VatAmount] [decimal](20, 2) NOT NULL,
	[CollateralPer] [float] NOT NULL,
	[CollateralAmount] [decimal](20, 2) NOT NULL,
	[TotalDeductionAmount] [decimal](20, 2) NOT NULL,
	[DepositInBGBFund] [decimal](20, 2) NOT NULL,
	[IsDepositeBGBFund] [bit] NULL,
	[Remarks] [nvarchar](250) NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
	[BillPaidTillDate] [decimal](18, 0) NULL,
	[BillPaidAmountInWord] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ADPReceivePaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [wbpm].[ViewADPReceivePayments]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [wbpm].[ViewADPReceivePayments] AS
SELECT 
    wbpm.ADPReceivePayment.ADPReceivePaymentId, 
    wbpm.ADPReceivePayment.LetterNo, 
    wbpm.ADPReceivePayment.BillNumber, 
    wbpm.ADPReceivePayment.BillDate, 
    wbpm.ADPReceivePayment.ExtraTime, 
    wbpm.ADPReceivePayment.BillPaymentSector, 
    wbpm.ADPReceivePayment.ActualWorkProgressPer, 
    wbpm.ADPReceivePayment.FinancialProgressPer, 
    wbpm.ADPReceivePayment.BillPaidPer, 
    wbpm.ADPReceivePayment.BillPaidAmount, 
    wbpm.ADPReceivePayment.BillPaidAmountInWord, 
    wbpm.ADPReceivePayment.TotalDeductionAmount, 
    wbpm.ADPReceivePayment.DepositInBGBFund, 
    wbpm.ConstructionCompany.FirmNameB, 
    wbpm.ConstructionCompany.FirmName, 
    wbpm.ProjectWork.ProjectWorkTitle, 
    wbpm.ProjectWork.EstimatedCost, 
    wbpm.ProjectWork.WorkStartDate, 
    wbpm.ProjectWork.WorkEndDate
FROM 
    wbpm.ADPReceivePayment 
INNER JOIN 
    wbpm.ProjectWork ON wbpm.ADPReceivePayment.ProjectWorkId = wbpm.ProjectWork.ProjectWorkId 
INNER JOIN 
    wbpm.ConstructionCompany ON wbpm.ProjectWork.ConstructionCompanyId = wbpm.ConstructionCompany.ConstructionCompanyId;
GO
/****** Object:  Table [wbpm].[ADPProject]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[ADPProject](
	[ADPProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectTitle] [nvarchar](500) NOT NULL,
	[MinistryDepartment] [nvarchar](255) NULL,
	[EstimatedExpenses] [decimal](20, 2) NOT NULL,
	[StartingDate] [datetime] NOT NULL,
	[EndingDate] [datetime] NOT NULL,
	[NoOfWork] [int] NOT NULL,
	[FinancialProgress] [float] NOT NULL,
	[PhysicalProgress] [float] NOT NULL,
	[Remarks] [nvarchar](250) NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ADPProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wbpm].[BGBMiscellaneousFund]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[BGBMiscellaneousFund](
	[FundId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectWorkId] [int] NOT NULL,
	[ADPReceivePaymentId] [int] NULL,
	[LetterNo] [nvarchar](150) NOT NULL,
	[DepositeDate] [datetime] NOT NULL,
	[PayOrderNo] [nvarchar](150) NOT NULL,
	[PayOrderDate] [datetime] NOT NULL,
	[BankName] [nvarchar](150) NOT NULL,
	[BrunchName] [nvarchar](150) NOT NULL,
	[AccountName] [nvarchar](150) NOT NULL,
	[AccountNumber] [nvarchar](150) NOT NULL,
	[Amount] [decimal](20, 2) NOT NULL,
	[Remarks] [nvarchar](150) NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FundId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wbpm].[ContractorCompanyPayment]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[ContractorCompanyPayment](
	[ContractorCompanyPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectWorkId] [int] NULL,
	[EstimatedCost] [decimal](20, 2) NOT NULL,
	[EstimatedCostTaxPer] [float] NOT NULL,
	[EstimatedCostTaxAmount] [decimal](20, 2) NOT NULL,
	[EstimatedCostVatPer] [float] NOT NULL,
	[EstimatedCostVatAmount] [decimal](20, 2) NOT NULL,
	[EstimatedCostCollateralPer] [float] NOT NULL,
	[EstimatedCostCollateralAmount] [decimal](20, 2) NOT NULL,
	[EstimatedCostDeductionAmount] [decimal](20, 2) NOT NULL,
	[NetEstimatedCostAmount] [decimal](20, 2) NOT NULL,
	[ProgressPer] [float] NOT NULL,
	[ProgressAmount] [decimal](20, 2) NOT NULL,
	[ProgressTaxPer] [float] NOT NULL,
	[ProgressTaxAmount] [decimal](20, 2) NOT NULL,
	[ProgressVatPer] [float] NOT NULL,
	[ProgressVatAmount] [decimal](20, 2) NOT NULL,
	[ProgressCollateralPer] [float] NOT NULL,
	[ProgressCollateralAmount] [decimal](20, 2) NOT NULL,
	[ProgressDeductionAmount] [decimal](20, 2) NOT NULL,
	[NetProgressAmount] [decimal](20, 2) NOT NULL,
	[PerformanceSecurityPer] [float] NOT NULL,
	[PerformanceSecurityAmount] [decimal](20, 2) NOT NULL,
	[ContactorProgressPer] [float] NOT NULL,
	[ContactorProgressAmount] [decimal](20, 2) NOT NULL,
	[BillPaymentNumber] [int] NOT NULL,
	[PreviouslyPaidAmount] [decimal](20, 2) NOT NULL,
	[PayableAmountOnCurrentBill] [decimal](20, 2) NOT NULL,
	[FinalPaymentAmount] [decimal](20, 2) NOT NULL,
	[Remarks] [nvarchar](150) NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ContractorCompanyPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wbpm].[FinancialYearAllocation]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[FinancialYearAllocation](
	[FinancialYearAllocationId] [int] IDENTITY(1,1) NOT NULL,
	[ADPProjectId] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[TotalAllocation] [float] NULL,
	[ADPAllocation] [float] NULL,
	[RADPAllocation] [float] NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime2](7) NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime2](7) NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FinancialYearAllocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wbpm].[FiscalYearExpense]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[FiscalYearExpense](
	[FiscalYearExpenseId] [int] IDENTITY(1,1) NOT NULL,
	[ADPProjectId] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[TotalExpense] [float] NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDATETIME] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FiscalYearExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wbpm].[FormalMeeting]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[FormalMeeting](
	[FormalMeetingId] [int] IDENTITY(1,1) NOT NULL,
	[ADPProjectId] [int] NOT NULL,
	[MeetingTitle] [nvarchar](150) NULL,
	[NumberOfMeeting] [int] NULL,
	[MeetingDate] [datetime] NULL,
	[MeetingDocument] [nvarchar](150) NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FormalMeetingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wbpm].[Masterplan]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[Masterplan](
	[MasterplanId] [int] IDENTITY(1,1) NOT NULL,
	[ADPProjectId] [int] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Document] [nvarchar](255) NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MasterplanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wbpm].[ProjectDirector]    Script Date: 2/10/2025 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wbpm].[ProjectDirector](
	[ProjectDirectorId] [int] IDENTITY(1,1) NOT NULL,
	[ADPProjectId] [int] NOT NULL,
	[ProjectDirectorName] [nvarchar](150) NULL,
	[ProjectDirectorNameB] [nvarchar](150) NULL,
	[Designation] [nvarchar](150) NULL,
	[DesignationB] [nvarchar](150) NULL,
	[JoiningDate] [datetime] NULL,
	[TransferDate] [datetime] NULL,
	[PDDocument] [nvarchar](150) NULL,
	[IsActive] [bit] NULL,
	[CreatedUser] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedUser] [int] NULL,
	[UpdatedDateTime] [datetime] NULL,
	[UpdateNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectDirectorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [wbpm].[ADPProject] ON 

INSERT [wbpm].[ADPProject] ([ADPProjectId], [ProjectTitle], [MinistryDepartment], [EstimatedExpenses], [StartingDate], [EndingDate], [NoOfWork], [FinancialProgress], [PhysicalProgress], [Remarks], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (1, N'বর্ডার গার্ড বাংলাদেশ, এর নবসৃজিত নারায়নগঞ্জ (৬২ বিজিবি) ব্যাটালিয়নের অবকাঠামোগত বিভিন্ন স্থাপনা নির্মাণ', N'স্বরাষ্ট্র মন্ত্রণালয়', CAST(20359000.00 AS Decimal(20, 2)), CAST(N'2023-12-04T00:00:00.000' AS DateTime), CAST(N'2024-12-04T00:00:00.000' AS DateTime), 5, 0, 0, NULL, 1, CAST(N'2024-11-25T10:16:22.060' AS DateTime), 1, CAST(N'2024-12-02T17:22:51.937' AS DateTime), 1)
SET IDENTITY_INSERT [wbpm].[ADPProject] OFF
GO
SET IDENTITY_INSERT [wbpm].[ADPReceivePayment] ON 

INSERT [wbpm].[ADPReceivePayment] ([ADPReceivePaymentId], [ProjectWorkId], [LetterNo], [BillNumber], [BillDate], [ExtraTime], [BillPaymentSector], [ActualWorkProgressPer], [FinancialProgressPer], [BillPaidPer], [BillPaidAmount], [TaxPer], [TaxAmount], [VatPer], [VatAmount], [CollateralPer], [CollateralAmount], [TotalDeductionAmount], [DepositInBGBFund], [IsDepositeBGBFund], [Remarks], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo], [BillPaidTillDate], [BillPaidAmountInWord]) VALUES (1, 1, N'88.02.1205.007.03.301.23/16', 1, CAST(N'2024-02-01T00:00:00.000' AS DateTime), NULL, N'বার্ষিক উন্নয়ন বাজেটের গ্রান্ড নং -১৯ হিসাবের খাত ১২২০৩-২২৪৩৪১১০০', 55, 0, 50, CAST(10179500.00 AS Decimal(20, 2)), 7, CAST(712565.00 AS Decimal(20, 2)), 7.5, CAST(763463.00 AS Decimal(20, 2)), 10, CAST(1017950.00 AS Decimal(20, 2)), CAST(2493978.00 AS Decimal(20, 2)), CAST(7685522.00 AS Decimal(20, 2)), 0, NULL, 3, CAST(N'2025-02-09T14:38:38.873' AS DateTime), 3, CAST(N'2025-02-09T16:01:00.777' AS DateTime), 1, NULL, N'এক কোটি এক লাখ ঊনঊননব্বই হাজার পাঁচ শো টাকা এবং শুন্য পয়সা')
INSERT [wbpm].[ADPReceivePayment] ([ADPReceivePaymentId], [ProjectWorkId], [LetterNo], [BillNumber], [BillDate], [ExtraTime], [BillPaymentSector], [ActualWorkProgressPer], [FinancialProgressPer], [BillPaidPer], [BillPaidAmount], [TaxPer], [TaxAmount], [VatPer], [VatAmount], [CollateralPer], [CollateralAmount], [TotalDeductionAmount], [DepositInBGBFund], [IsDepositeBGBFund], [Remarks], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo], [BillPaidTillDate], [BillPaidAmountInWord]) VALUES (2, 1, N'44.02.1205.007.03.301.24/16', 2, CAST(N'2025-02-09T00:00:00.000' AS DateTime), NULL, N'বার্ষিক উন্নয়ন বাজেটের গ্রান্ড নং -১৯ হিসাবের খাত ১২২০৩-২২৪৩৪১১০০', 0, 50, 37.68, CAST(7671271.00 AS Decimal(20, 2)), 7, CAST(536989.00 AS Decimal(20, 2)), 7.5, CAST(575345.00 AS Decimal(20, 2)), 10, CAST(767127.00 AS Decimal(20, 2)), CAST(1879461.00 AS Decimal(20, 2)), CAST(5791810.00 AS Decimal(20, 2)), 0, NULL, 3, CAST(N'2025-02-09T16:50:42.343' AS DateTime), NULL, NULL, 0, CAST(10179500 AS Decimal(18, 0)), N' ছিয়াত্তর লাখ একাত্তর হাজার দুই শো একাত্তর টাকা এবং শুন্য পয়সা')
SET IDENTITY_INSERT [wbpm].[ADPReceivePayment] OFF
GO
SET IDENTITY_INSERT [wbpm].[ConstructionCompany] ON 

INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (1, N'Alpha Construction', N'আলফা কন্সট্রাকশন', N'Pro: Mahfuzur Rahman', N'প্রো: মাহফুজুর রহমান', N'01845678901', N'mahfuz@alfaconstruction.com', N'67, Kunjban, Gulshan-1, Dhaka-1212', N'৬৭, কুঞ্জবন, গুলশান-১, ঢাকা-১২১২', 1, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-04-10T14:30:10.760' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (2, N'Arab Fine Construction', N'আরব ফাইন কন্সট্রাকশন', N'Pro: Selim Sikdar', N'প্রো: সেলিম সিকদার', N'01856789012', N'selim@arbfine.com', N'107, Malibagh, Dhaka-1205', N'১০৭, মালিবাগ, ঢাকা-১২০৫', 3, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-05-12T09:50:23.440' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (3, N'MS Enterprise', N'এমএস এন্টারপ্রাইজ', N'Pro: Masud Rana', N'প্রো:  মাসুদ রানা', N'01867890123', N'masud@msenterprise.com', N'85, Hatirjhil, Dhaka-1207', N'৮৫, হাতিরঝিল, ঢাকা-১২০৭', 2, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-06-17T13:35:10.540' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (4, N'Green Haven Construction', N'গ্রীন হেভেন কন্সট্রাকশন', N'Pro: Ashikul Islam', N'প্রো: আশিকুল ইসলাম', N'01878901234', N'ashikul@greenhaven.com', N'150, Mirpur, Dhaka-1216', N'১৫০, মিরপুর, ঢাকা-১২১৬', 2, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-07-22T11:00:33.500' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (5, N'Saif Construction', N'সৈফ কন্সট্রাকশন', N'Pro: Mehedi Hasan', N'প্রো: মেহেদী হাসান', N'01889012345', N'mehedi@saifconstruction.com', N'84, Palashi, Dhaka-1205', N'৮৪, পলাশী, ঢাকা-১২০৫', 1, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-08-09T11:40:45.150' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (6, N'Host Construction', N'হোস্ট কন্সট্রাকশন', N'Pro: Abdul Kader', N'প্রো: আব্দুল কাদের', N'01890123456', N'abdul@hostconstruction.com', N'20, Central Road, Dhaka-1207', N'২০, সেন্ট্রাল রোড, ঢাকা-১২০৭', 3, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-09-10T14:30:50.980' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (7, N'Elite Builders', N'এলিট বিল্ডার্স', N'Pro: Tanveer Alam', N'প্রো: তানভীর আলম', N'01901234567', N'tanveer@elitebuilders.com', N'99, New Airport Road, Dhaka-1213', N'৯৯, নিউ এয়ারপোর্ট রোড, ঢাকা-১২১৩', 1, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-10-15T12:10:55.100' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (8, N'Vision Builders', N'ভিশন বিল্ডার্স', N'Pro: Anwar Hossain', N'প্রো: আনোয়ার হোসেন', N'01912345678', N'anwar@visionbuilders.com', N'200, Shantinagar, Dhaka-1217', N'২০০, শান্তিনগর, ঢাকা-১২১৭', 2, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-11-22T15:45:30.210' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (9, N'Apex Construction', N'এপেক্স কন্সট্রাকশন', N'Pro: Sumon Sarker', N'প্রো: সুমন সরকার', N'01723456789', N'sumon@apexconstruction.com', N'45, Kazipara, Dhaka-1219', N'৪৫, কাজীপাড়া, ঢাকা-১২১৯', 3, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-12-05T16:30:12.540' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (10, N'Redstone Builders', N'রেডস্টোন বিল্ডার্স', N'Pro: Feroze Khan', N'প্রো: ফেরোজ খান', N'01923456789', N'feroze@redstonebuilders.com', N'303, Farmgate, Dhaka-1209', N'৩০৩, ফার্মগেট, ঢাকা-১২০৯', 1, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-12-15T11:20:33.430' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (11, N'Infinity Construction', N'ইনফিনিটি কন্সট্রাকশন', N'Pro: Hossain Sayeed', N'প্রো: হোসেন সায়ীদ', N'01734567890', N'hossain@infinityconstruction.com', N'121, Banani, Dhaka-1212', N'১২১, বনানী, ঢাকা-১২১২', 2, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2021-12-20T10:15:05.540' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (12, N'Pioneer Builders', N'পায়নিার বিল্ডার্স', N'Pro: Jashim Uddin', N'প্রো: জশিম উদ্দিন', N'01845678901', N'jashim@pioneerbuilders.com', N'56, Karwan Bazar, Dhaka-1215', N'৫৬, কারওয়ান বাজার, ঢাকা-১২১৫', 3, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2022-01-10T13:30:45.630' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (13, N'Prime Developers', N'প্রাইম ডেভেলপার্স', N'Pro: Alok Chandra', N'প্রো:  চন্দ্র', N'01856789012', N'alok@primedevelopers.com', N'66, Shyamoli, Dhaka-1207', N'৬৬, শ্যামলী, ঢাকা-১২০৭', 1, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2022-02-03T14:45:50.440' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (14, N'Skyline Construction', N'স্কাইলাইন কন্সট্রাকশন', N'Pro: Mirza Shohid', N'প্রো:  মির্জা সোহীদ', N'01967890123', N'mirza@skylineconstruction.com', N'77, Green Road, Dhaka-1215', N'৭৭, গ্রীন রোড, ঢাকা-১২১৫', 2, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2022-02-18T11:10:22.570' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (15, N'Elite Construction', N'এলিট কন্সট্রাকশন', N'Pro: Rashedul Islam', N'প্রো: রাশেদুল ইসলাম', N'01778901234', N'rashedul@eliteconstruction.com', N'82, Kalabagan, Dhaka-1205', N'৮২, কালাবাগান, ঢাকা-১২০৫', 3, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2022-03-10T09:40:10.620' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (16, N'Sonic Construction', N'সনিক কন্সট্রাকশন', N'Pro: Nazmul Hossain', N'প্রো: নাজমুল হোসেন', N'01978901234', N'nazmul@sonicconstruction.com', N'130, Mirpur, Dhaka-1216', N'১৩০, মিরপুর, ঢাকা-১২১৬', 2, CAST(N'2024-11-30T09:42:10.360' AS DateTime), 1, CAST(N'2022-03-22T13:25:55.750' AS DateTime), 1)
INSERT [wbpm].[ConstructionCompany] ([ConstructionCompanyId], [FirmName], [FirmNameB], [ContactPerson], [ContactPersonB], [ContactPhone], [Email], [FirmAddress], [FirmAddressB], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (1001, N'M/S Titus Enterprises', N'মেসার্স তিতাস এন্টারপ্রাইস', N'Pro - Md. Samsuzzaman Babu', N'প্রোঃ মোঃ সামসুজ্জামান বাবু', N'01782249633', N'samsuzzaman@gmail.com', N'No. 61 Abdul Aziz Lane, Lalbagh, Dhaka-1211', N'নং-৬১ আব্দুল আজিজ লেন, লালবাগ, ঢাকা-১২১১', 3, CAST(N'2025-02-09T11:28:39.347' AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [wbpm].[ConstructionCompany] OFF
GO
SET IDENTITY_INSERT [wbpm].[FinancialYearAllocation] ON 

INSERT [wbpm].[FinancialYearAllocation] ([FinancialYearAllocationId], [ADPProjectId], [FiscalYearId], [TotalAllocation], [ADPAllocation], [RADPAllocation], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (3, 1, 2, 2000000, 10000000, 10000000, 1, CAST(N'2025-02-06T07:12:07.2930000' AS DateTime2), NULL, NULL, 0)
SET IDENTITY_INSERT [wbpm].[FinancialYearAllocation] OFF
GO
SET IDENTITY_INSERT [wbpm].[FiscalYearExpense] ON 

INSERT [wbpm].[FiscalYearExpense] ([FiscalYearExpenseId], [ADPProjectId], [FiscalYearId], [TotalExpense], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDATETIME], [UpdateNo]) VALUES (1, 1, 1, 900000, 3, CAST(N'2025-02-06T16:10:07.197' AS DateTime), 3, CAST(N'2025-02-06T16:19:56.697' AS DateTime), 1)
INSERT [wbpm].[FiscalYearExpense] ([FiscalYearExpenseId], [ADPProjectId], [FiscalYearId], [TotalExpense], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDATETIME], [UpdateNo]) VALUES (2, 1, 1, 680000, 3, CAST(N'2025-02-06T16:19:20.313' AS DateTime), 3, CAST(N'2025-02-06T16:20:03.647' AS DateTime), 1)
SET IDENTITY_INSERT [wbpm].[FiscalYearExpense] OFF
GO
SET IDENTITY_INSERT [wbpm].[FormalMeeting] ON 

INSERT [wbpm].[FormalMeeting] ([FormalMeetingId], [ADPProjectId], [MeetingTitle], [NumberOfMeeting], [MeetingDate], [MeetingDocument], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (1, 1, N'Abc', 5, CAST(N'2025-02-06T00:00:00.000' AS DateTime), NULL, 3, CAST(N'2025-02-06T17:07:46.277' AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [wbpm].[FormalMeeting] OFF
GO
SET IDENTITY_INSERT [wbpm].[ProjectDirector] ON 

INSERT [wbpm].[ProjectDirector] ([ProjectDirectorId], [ADPProjectId], [ProjectDirectorName], [ProjectDirectorNameB], [Designation], [DesignationB], [JoiningDate], [TransferDate], [PDDocument], [IsActive], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (1, 1, N'Nurul Islam', N'নূরুল ইসলাম', N'Colonel', N'কর্নেল', CAST(N'2018-08-01T00:00:00.000' AS DateTime), CAST(N'2022-11-20T00:00:00.000' AS DateTime), N'COL_Nurul_Islam_Permission_Document.pdf', 1, 102, CAST(N'2023-02-25T11:45:00.000' AS DateTime), 103, CAST(N'2023-06-12T14:00:00.000' AS DateTime), 1)
INSERT [wbpm].[ProjectDirector] ([ProjectDirectorId], [ADPProjectId], [ProjectDirectorName], [ProjectDirectorNameB], [Designation], [DesignationB], [JoiningDate], [TransferDate], [PDDocument], [IsActive], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (5, 1, N'Ahmed Khan', N'আহমেদ খান', N'Major', N'মেজর', CAST(N'2019-05-15T00:00:00.000' AS DateTime), CAST(N'2023-01-10T00:00:00.000' AS DateTime), N'COL_Ahmed_Khan_Permission_Document.pdf', 0, 104, CAST(N'2023-03-01T09:30:00.000' AS DateTime), 105, CAST(N'2023-07-15T16:00:00.000' AS DateTime), 2)
INSERT [wbpm].[ProjectDirector] ([ProjectDirectorId], [ADPProjectId], [ProjectDirectorName], [ProjectDirectorNameB], [Designation], [DesignationB], [JoiningDate], [TransferDate], [PDDocument], [IsActive], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (6, 1, N'Sarah Ali', N'সারা আলী', N'Lieutenant Colonel', N'লেফটেন্যান্ট কর্নেল', CAST(N'2020-07-20T00:00:00.000' AS DateTime), CAST(N'2024-02-01T00:00:00.000' AS DateTime), N'COL_Sarah_Ali_Permission_Document.pdf', 0, 106, CAST(N'2023-04-05T13:30:00.000' AS DateTime), 107, CAST(N'2023-08-01T10:00:00.000' AS DateTime), 3)
INSERT [wbpm].[ProjectDirector] ([ProjectDirectorId], [ADPProjectId], [ProjectDirectorName], [ProjectDirectorNameB], [Designation], [DesignationB], [JoiningDate], [TransferDate], [PDDocument], [IsActive], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (7, 1, N'Mohammad Ali', N'মুহাম্মদ আলী', N'Captain', N'ক্যাপ্টেন', CAST(N'2021-11-10T00:00:00.000' AS DateTime), CAST(N'2025-05-25T00:00:00.000' AS DateTime), N'COL_Mohammad_Ali_Permission_Document.pdf', 0, 108, CAST(N'2023-05-22T08:15:00.000' AS DateTime), 109, CAST(N'2023-09-18T14:00:00.000' AS DateTime), 4)
SET IDENTITY_INSERT [wbpm].[ProjectDirector] OFF
GO
SET IDENTITY_INSERT [wbpm].[ProjectWork] ON 

INSERT [wbpm].[ProjectWork] ([ProjectWorkId], [ADPProjectId], [ConstructionCompanyId], [ProjectWorkTitle], [EstimatedCost], [NOADate], [AgreementDate], [WorkOrderDate], [WorkStartDate], [WorkEndDate], [OnFieldStartDate], [OnFieldEndDate], [HandOverDate], [BankGuaranteeEndDate], [ConstructionProgress], [BankGuaranteeDocument], [NOADocument], [AgreementDocument], [WorkOrderDocument], [WorkStatus], [Remarks], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (1, 1, 1001, N'বর্ডার গার্ড ব্যটালিয়ন, নারায়ণগঞ্জ-এ ০১টি মাল্টিপারপাস শেড নির্মান কাজ', CAST(20359000.00 AS Decimal(20, 2)), CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'2025-02-09T00:00:00.000' AS DateTime), 0, N'2_20250209_111856.pdf', N'3_20250209_111856.pdf', N'Hemayet-Mondol-FlowCV-Resume-20241001_20250208_133543.pdf', N'Hemayet-Mondol-Q_20250208_133545.pdf', N'Running', NULL, 3, CAST(N'2025-02-08T13:35:49.997' AS DateTime), 3, CAST(N'2025-02-09T11:50:26.000' AS DateTime), 1)
INSERT [wbpm].[ProjectWork] ([ProjectWorkId], [ADPProjectId], [ConstructionCompanyId], [ProjectWorkTitle], [EstimatedCost], [NOADate], [AgreementDate], [WorkOrderDate], [WorkStartDate], [WorkEndDate], [OnFieldStartDate], [OnFieldEndDate], [HandOverDate], [BankGuaranteeEndDate], [ConstructionProgress], [BankGuaranteeDocument], [NOADocument], [AgreementDocument], [WorkOrderDocument], [WorkStatus], [Remarks], [CreatedUser], [CreatedDateTime], [UpdatedUser], [UpdatedDateTime], [UpdateNo]) VALUES (2, 1, 1, N'test', CAST(326122.00 AS Decimal(20, 2)), CAST(N'2025-02-08T00:00:00.000' AS DateTime), CAST(N'2025-02-08T00:00:00.000' AS DateTime), CAST(N'2025-01-31T00:00:00.000' AS DateTime), CAST(N'2025-02-01T00:00:00.000' AS DateTime), CAST(N'2025-02-07T00:00:00.000' AS DateTime), CAST(N'2025-01-31T00:00:00.000' AS DateTime), CAST(N'2025-02-07T00:00:00.000' AS DateTime), CAST(N'2025-02-08T00:00:00.000' AS DateTime), CAST(N'2025-02-08T00:00:00.000' AS DateTime), 20, N'Hemayet-Mondol-FlowCV-Resume-20250202 (1)_20250208_134805.pdf', N'Hemayet-Mondol-FlowCV-Resume-20250202 (1)_20250208_134805.pdf', N'OID1025778134_20250208_134805.pdf', N'Hemayet-Mondol-FlowCV-Resume-20241117 (1)_20250208_134805.pdf', N'Running', N'Good', 3, CAST(N'2025-02-08T13:48:05.400' AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [wbpm].[ProjectWork] OFF
GO
ALTER TABLE [wbpm].[ADPReceivePayment] ADD  DEFAULT ((0)) FOR [IsDepositeBGBFund]
GO
ALTER TABLE [wbpm].[ADPReceivePayment]  WITH CHECK ADD FOREIGN KEY([ProjectWorkId])
REFERENCES [wbpm].[ProjectWork] ([ProjectWorkId])
GO
ALTER TABLE [wbpm].[ADPReceivePayment]  WITH CHECK ADD FOREIGN KEY([ProjectWorkId])
REFERENCES [wbpm].[ProjectWork] ([ProjectWorkId])
GO
ALTER TABLE [wbpm].[BGBMiscellaneousFund]  WITH CHECK ADD FOREIGN KEY([ADPReceivePaymentId])
REFERENCES [wbpm].[ADPReceivePayment] ([ADPReceivePaymentId])
GO
ALTER TABLE [wbpm].[BGBMiscellaneousFund]  WITH CHECK ADD FOREIGN KEY([ProjectWorkId])
REFERENCES [wbpm].[ProjectWork] ([ProjectWorkId])
GO
ALTER TABLE [wbpm].[ContractorCompanyPayment]  WITH CHECK ADD FOREIGN KEY([ProjectWorkId])
REFERENCES [wbpm].[ProjectWork] ([ProjectWorkId])
GO
ALTER TABLE [wbpm].[FinancialYearAllocation]  WITH CHECK ADD FOREIGN KEY([ADPProjectId])
REFERENCES [wbpm].[ADPProject] ([ADPProjectId])
GO
ALTER TABLE [wbpm].[FinancialYearAllocation]  WITH CHECK ADD FOREIGN KEY([FiscalYearId])
REFERENCES [budget].[FiscalYear] ([FiscalYearId])
GO
ALTER TABLE [wbpm].[FiscalYearExpense]  WITH CHECK ADD FOREIGN KEY([ADPProjectId])
REFERENCES [wbpm].[ADPProject] ([ADPProjectId])
GO
ALTER TABLE [wbpm].[FiscalYearExpense]  WITH CHECK ADD FOREIGN KEY([FiscalYearId])
REFERENCES [budget].[FiscalYear] ([FiscalYearId])
GO
ALTER TABLE [wbpm].[FormalMeeting]  WITH CHECK ADD FOREIGN KEY([ADPProjectId])
REFERENCES [wbpm].[ADPProject] ([ADPProjectId])
GO
ALTER TABLE [wbpm].[Masterplan]  WITH CHECK ADD FOREIGN KEY([ADPProjectId])
REFERENCES [wbpm].[ADPProject] ([ADPProjectId])
GO
ALTER TABLE [wbpm].[ProjectDirector]  WITH CHECK ADD FOREIGN KEY([ADPProjectId])
REFERENCES [wbpm].[ADPProject] ([ADPProjectId])
GO
ALTER TABLE [wbpm].[ProjectWork]  WITH CHECK ADD FOREIGN KEY([ADPProjectId])
REFERENCES [wbpm].[ADPProject] ([ADPProjectId])
GO
ALTER TABLE [wbpm].[ProjectWork]  WITH CHECK ADD FOREIGN KEY([ConstructionCompanyId])
REFERENCES [wbpm].[ConstructionCompany] ([ConstructionCompanyId])
GO
