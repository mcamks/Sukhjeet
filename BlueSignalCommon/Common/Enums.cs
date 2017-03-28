namespace BlueSignalCommon.Common
{
    /// <summary>
    ///     Enum for labels used in Login Screen
    /// </summary>
    public enum LoginScreenLabels
    {
        LanguageId = 5,
        MicrosoftAdvantage = 2,
        Welcome = 1
    }

    /// <summary>
    ///     Enum for labels used in FAQ Screen
    /// </summary>
    public enum FaqScreenLabels
    {
        Faq = 1
    }

    /// <summary>
    ///     Enum used in prize screen
    /// </summary>
    public enum PrizeScreenEnums
    {
        DefaultNoOfPrizes = 14
    }

    /// <summary>
    ///     Enum for labels used in Quiz Screen
    /// </summary>
    public enum QuizScreenLabels
    {
        Quiz = 4
    }

    /// <summary>
    ///     Enum for labels used in Settings Screen
    /// </summary>
    public enum SettingsScreenLabels
    {
        Contact = 28,
        Faq = 31,
        HowToParticipate = 26,
        LogOff = 25,
        ManageYourProfile = 25,
        MyPrizeOrders = 30,
        MyProfile = 15,
        Quiz = 14,
        TermsAndConditions = 26
    }

    /// <summary>
    ///     Enum for id_org fields
    /// </summary>
    public enum CategoryImages
    {
        Appliances = 52,
        Avatar = 127,
        Clothing = 36,
        Electronics = 21,
        Home = 80,
        Leisure = 27
    }

    /// <summary>
    ///     Enums for transaction status
    /// </summary>
    public enum TransactionStatusText
    {
        New = 1,
        Ordered = 2,
        Completed = 3,
        Backordered = 4,
        Approved = 5,
        Shipped = 6
    }

    /// <summary>
    ///     Enums for transaction type
    /// </summary>
    public enum TransactionTypeText
    {
        Sale = 1,
        Order = 2,
        Birthday = 3,
        Quiz = 4,
        Learning = 5,
        Random = 6,
        Welcome = 7
    }

    public enum GlobalCodeCategoryValue
    {
        Bedtypes = 1001,
        CodeTypes = 1201,
        RoomType = 1601,
        UserType = 2304,
        BedRateCardType = 6001,
        UnitTypes = 8001,
        //Frequency = 9001,
        ScreenGroup = 9991,
        PatientRelationTypes = 1101,
        ServiceCodeStartRange = 2401,
        ServiceCodeFinishRange = 2405,
        CPTCodestartRange = 2401,
        CPTCodesFinishRange = 2499,
        HCPCSCodestartRange = 2401,
        HCPCSCodesFinishRange = 2499,
        TimeUnitType = 7001,
        PatientPhoneTypes = 1102,
        PatientDocumentTypes = 1103,
        EncounterConfidentialityLevel = 1105,
        EncounterModeofArrival = 1106,
        EncounterPatientType = 1107,
        EncounterServiceCategory = 1103,
        EncounterAdmitType = 1108,
        EncounterAccidentRelated = 1109,
        EncounterAccidentType = 1110,
        //EncounterPoliceInvolved = 1103,
        //EncounterSpecimenType = 1103,
        EncounterType = 1501,
        EncounterEndType = 1301,
        EncounterStartType = 1401,
        EncounterSpecialty = 2101,
        EncounterPhysicianType = 1104,
        BedUnitType = 18,
        AlergyStartRange = 8101,
        AlergyEndRange = 8999,
        Vitals = 1901,
        UnitOfMeasure = 3101,
        OrderStatus = 3102,
        ActivityStatus = 3103,
        BillStatus = 3104,
        LabImagesType = 2305,
        LegalDocumentType = 2306,
        OldMedicalRecordType = 2307,
        LabTest = 11080,
        Anesthesia = 11001,
        EvaluationAndManagement = 11009,
        Medicine = 11090,
        Pharmacy = 11100,
        Radiology = 11070,
        Surgery = 11010,
        _PhysicianType1 = 1087,
        _NurseType = 1088,
        _CoderType = 1089,
        BillHeaderStatus = 14700,
        OrderFrequencyType = 1024,
        MCContractLevel = 950,
        PhysicianLicenseType = 1104,
        NurseLicenseType = 1118,
        CoderLicenseType = 1119,
        ClerkLicenseType = 1120,
        CorrectionCodes = 0101,
        McManagedCareCode = 951,
        ActiveMedicalProblems = 960,
        PatientInstructions = 961,
        TypesOfFollowup = 962,
        NoteTypes = 963,
        Statistics = 1064,
        LabTestOrderSet = 3109,
        LabMeasurementValue = 3108,
        LabTestResultAgeFrom = 3106,
        LabTestResultSpecimen = 3105,
        LabTestResultGender = 3107,
        DashBoardBudgetType = 3112,
        DashboardStatDesc = 3110,
        DataTypes = 14000,
        DataComparer = 14100,
        TableName = 14200,
        Column = 14400,
        ConditionStart = 14101,
        ConditionEnd = 14102,
        RulesSpecifiedfor = 14103,
        CodeTime = 3113,
        ErrorTypes = 1010,
        Frequencytype = 1011,
        EncounterpatientType = 1107,
        CalculationMethod = 1014,
        AuthorizationType = 1701,
        DashboardTargetsUOM = 1012,
        DashboardTargetsTimingIncrement = 1013,
        DashboardBudgetFor = 3119,
        Calculationtype = 1015,
        ManagedCareElseTableType = 1016,
        MagaedCareElsePartTableColumns = 1017,
        OnlineHelp = 1018,
        OperatingRoomType = 3131,
        FacilityRegions = 4141,
        FacilityTypes = 4242,
        LoginTrackingTypes = 4000,
        DashboardFrequencyType = 4344,
        DashboardFormatType = 4343,
        ExternalDashboardType = 4345,
        KPICategoryType = 4346,
        KpiSubCategories1 = 4347,
        PerPatientType = 4348,
        LtcPatients = 4349,
        Rehabpatients = 4350,
        KpiSubCategories2 = 4351,
        IndicatorDataType = 4352,
        ReactionCategory = 4999,
        CurrentMedicationDuration = 4801,
        CurrentMedicationVolume = 4802,
        CurrentMedicationDosage = 4803,
        DashboardTypes = 4401,
        ExternalDashboardCategories = 4402,
        ExternalDashboardDataFields = 4403,
        ExternalDashboardArguments = 4405,
        ExternalDashboardColors = 4406,
        TypeofData = 4407,
        ProjectsPercentageValue = 4601,
        DashboardProjectsType = 4408,
        DashboardIndicatorSettings = 4400,
        DefaultTableNumber = 4603,
        PhysicianSpecialties = 1121,
        AppointmentDefaultTime = 4904,
        PatientTriageLevels = 4952,
        PatientState = 4951,
        LicenceType=1104
    }

    public enum OrderTypesNotInUse
    {
        USCLS = 3,
        DRUG = 5
    }

    public enum PermissionTypes
    {
        Screen = 1,
    }

    public enum LoginResponseTypes
    {
        Success = 0,
        Failed = 1,
        InActive = 2,
        IsDeleted = 3,
        CaptchaFailed = 4,
        FailedAttemptsOver = 5,
        Blocked = 6,
        UnknownError = 7,
        AccountNotActivated = 8,
        OddLoginTiming = 9,
        WrongPassword = 10,
        UserNotFound = 11,
        UserNotFoundInCorporate = 12
    }

    public enum SessionEnum
    {
        User,
        SessionUser,
        GroupId,
        SelectedCulture,
        parentId,
        previousUrl,
        eventviewstate,
        PreviousGroupId,
        SecretLogin,
        SessionGroup,
        ContentStream,
        ContentStreamDoc,
        ContentLength,
        ContentType,
        UtcDiffWithuniversalTime,
        CroppedContentType,
        IsDayLight,
        SessionGuardianUserId,
        SessionFiltersToPersist,
        TimezoneName,
        PatientId,
        TempFile
    }

    //Added By ashwani
    public enum EncounterPatientType
    {
        ERPatient = 1,
        InPatient = 2,
        OutPatient = 3
    }

    public enum EncounterEndTypeEnum
    {
        Dischargedwithapproval = 1,
        Dischargedagainstadvice = 2,
        Dischargedabsentwithoutleave = 3,
        Dischargetransfertoacute = 4,
        Notdischarged = 5,
        Dischargedtransfertononacutecare = 6
    }

    public enum EncounterStates
    {
        admitpatient = 1,
        outpatient = 2,
        discharge = 3,
        endencounter = 4,
        transferpatient = 5,
        editencounter = 6,
    }

    //Added By ashwani
    public enum OrderType
    {
        CPT = 3,
        HCPCS = 4,
        BedCharges = 8,
        DRG = 9,
        DRUG = 5,
        MedicationOrders = 6,
        Labs = 7,
        PhysicalTherapy = 1,
        RespiratoryTherapy = 2,
        NursingTreatment = 10,
        Dietary = 11,
        Imaging = 12,
        Surgical = 13,
        OccupationalTherapy = 14,
        Others = 15,
        Diagnosis = 16,
        Orders = 17,
        ServiceCode = 18,
        BillEditRules = 19
    }

    public enum SessionNames
    {
        SessionClass,
        SessoionModuleAccess,
        SelectedFacilityId,
        FacilityStructureData
    }

    public enum MappingRoleType
    {
        Facility,
        Corporate
    }

    //enum Phone Type
    public enum PhoneType
    {
        HomePhone = 1,
        MobilePhone = 2,
        BusinessPhone = 3,
        Others = 4
    }

    public enum DenialTypes
    {
        Authorization = 1,
    }

    public enum DefaultRoles
    {
        SysAdmin = 40
    }

    public enum MaxValueKeys
    {
        MedicalRecordNumber = 1
    }

    public enum DiagnosisType
    {
        Primary = 1,
        Secondary = 2,
        DRG = 3,
        CPT = 4
    }

    //Added By Shashank
    public enum MedicalRecordType
    {
        Vitals = 1,
        History = 2,
        Allergies = 3,
        LabTest = 4
    }

    public enum DefaultRoleIDs
    {
        SysAdminRole = 40 //To be changed later
    }

    //Added By Shashank
    public enum NotesUserType
    {
        Physician = 1,
        Nurse = 2
    }

    public enum DocumentTemplateTypes
    {
        ProfileImage = 1,
        RadImaging = 2,
        LegalDocuments = 3,
        OldMedicalRecords = 4
    }

    public enum SearchType
    {
        CPT = 3,
        HCPCS = 4,
        BedCharges = 8,
        DRG = 9,
        DRUG = 5,
        MedicationOrders = 6,
        Labs = 7,
        PhysicalTherapy = 1,
        RespiratoryTherapy = 2,
        NursingTreatment = 10,
        Dietary = 11,
        Imaging = 12,
        Surgical = 13,
        OccupationalTherapy = 14,
        Others = 15,
        Diagnosis = 16,
        Orders = 17,
        ServiceCode = 18,
        Denial
    }

    public enum BaseFacilityStucture
    {
        Floor = 82,
        Department = 83,
        Rooms = 84,
        Bed = 85
    }

    public enum MessageType
    {
        AdmitPatient = 1,
        StartOutPatient = 2,
        Discharge = 3,
        EndEncounter = 4,
        EncounterDetails = 5,
        ViewEHR = 6,
        ViewAuthorization = 7,
        ViewBillingHeader = 8,
        ViewScrubReport = 9,
        EnterManualPayment = 10,
        ViewPatientPortal = 11,
        ViewEM = 6,
        Other = 99,
        PatientSchedular = 12,
        ServiceCodeMainCategory = 6200
    }

    public enum FavoriteCategories
    {
        Orders = 1,
        Diagnosis = 2,
        CPT = 3,
        HCPCS = 4,
        BedCharges = 8,
        DRG = 9,
        DRUG = 5,
    }

    public enum StoredProcedures
    {
        SPROC_GetMedicalNecessityData,
        SPROC_GetFacilityStructureData,
        SPROC_GetMostOrdered,
        SPROC_GetVitals,
        SPROC_GetDBEncounter,
        SPROC_DefaultFacilityItems,
        SendEClaim,
        SPROC_ApplyBedChargesToBill,
        SPROC_ApplyOrderToBill,
        SPROC_ScrubBill,
        SPROC_ScrubBill_Batch,
        SPROC_GetAgeingPayorWise,
        SPROC_GetAgeingPatientWise,
        SPROC_GetAgeingDepartmentWise,
        SPROC_GetReconcilationARPayorWise,
        SPROC_GetReconcilationARPatientWise,
        SPROC_GetReconcilationARDepartmentWise,
        SPROC_GetDBBedOccupancyRate,
        XMLParser,
        SPROC_GetDBBedOccupancybyFloor,
        SPROC_GetDBEncounterType,
        XMLRemittanceAdviceParser,
        SPROC_GetRevenueForecastFacility,
        SPROC_GetRevenueForecastbyPatient,
        SPROC_ApplyOrderActivityToBill,
        SPROC_BillDetailView,
        SPROC_ScrubReportCorrections,
        SPROC_ApplyOrderToBillSetHeader,
        GenerateRemittanceInfo,
        GenerateRemittanceXMLFile,
        SPROC_ApplyPaymentManualToBill,
        SPROC_ApplyAdvicePayments,
        SPROC_GetDBRegistrationProductivity,
        SPROC_REP_AccountStatementByPatient,
        SPROC_REP_NoPaymentsReceived,
        SPROC_REP_PaymentsUnMatched,
        SPROC_EncounterTransactionView,
        SPROC_Get_REP_LoginTimeDayNightWise,
        SPROC_Get_REP_ClaimTransDetails,
        SPROC_GetXPaymentReturnDenialClaims,
        SPROC_Get_REP_DenialCode,
        SPROC_ScrubRule_Preview,
        SPROC_GetDBBedStruture,
        SPROC_GetPatientBedInformation,
        SPROC_EncounterEndCheckBillEdit,
        SPROC_GetSelectedBedInformation,
        SPROC_SetCorrectedDiagnosis,
        SPROC_Get_REP_JEByDepartment,
        SPROC_GetMCOverview,
        SPROC_GetDBBudgetActual,
        SPROC_GetPatientEnciunterInPayment,
        SPROC_GetScrubberEditTrack,
        SPORC_GetAppointmentTypes,
        /*Added by Amit Jain for Reports in Review Expected Payment Screen
         */
        SPROC_REP_ExpectedPaymentInsNotPaid,
        SPROC_REP_ExpectedPaymentPatientVar,
        SPROC_REP_ExpectedPaymentInsVariance,


        /*
         * On: 17022015
         * Purpose: New Vital Chart 1
         * By: Amit Jain
         */
        SPROC_GetVitalsDR,
        SPROC_GetLabTestResultStatus,
        SPROC_GetClaimDenialPercent,
        SPROC_GetPhysicianPreviousOrders,
        SPROC_GetDBCharges,
        SPROC_SetDBChargesActuals,
        SPROC_GetJobClosedEncounterAndOrders,
        SPROC_BillHeaderDetailsView,
        SPROC_DeleteBillActivites,
        SPROC_SetDBCountersActuals,
        SPROC_GetCounterRegistrationProductivity,
        SPROC_GetActiveEncounterGraphs,
        SPROC_GetBillingTrendData,
        SPROC_GetPatientAgeingPayorWise,
        SPROC_ApplySurguryChargesToBill,
        SPROC_GetRiskFactors,
        SPROC_GetSrubHeaderDetail,
        SPROC_Get_REP_ChargeReport,
        SPORC_GetDashboardTransactionCounterData,
        SPROC_GET_REP_CHargeReportDetails,
        SPROC_GetDBChargesByCorporate,
        SPROC_GetPatientFallRate_ManualDashboard,
        SPROC_GetSubCategoryCharts_Manual,
        SPROC_GetSubCategoryCharts_PixorMix,
        SPROC_GetManualDashboardDataList,
        SPROC_GetManualDashboardDataListV1,
        SPROC_GetManualDashboardDataByIndicatorNumber,
        SPROC_GetManualDashboardDataByIndicatorNumberV1,
        SPROC_GetDBBudgetActualManual_Acute,
        SPROC_GetDBBudgetActualManualIndicators,
        SPROC_GetPhysicianFavoriteOrders,
        SPROC_GetActiveLabOrdersByPhysicianId,
        Sproc_GetPatientAllergyByPharmacyOrderCode,
        SPROC_ReValuateCurrentBill,
        SPROC_ReValuateBedChargesPerEncounter,
        SPROC_GetPreliminaryXmlFile,
        SPROC_GetDBBudgetManualBalanceSheet,
        GenerateEAuthorizationFile,
        GetDashboardParameters,
        SPROC_UpdateDashboardIndicatorsData,
        SPROC_GetProjectDashbaordData,
        SPROC_AddMonthWisevaluesInProjectDashboard,
        SPROC_GenerateUpdateToken,
        //SPROC_UpdateCalculativeIndicatorData,
        SPROC_UpdateCalculativeIndicatorData_SA,
        SPROC_GetManualDashboardDataSA,
        SPROC_SaveIndicatorDataAfterNewIndicator,
        SPROC_SaveIndicatorDataAfterNewIndicator_New,
        SPROC_DeleteIndicatorDataCheckList,
        SPROC_SetStaticBudgetTarget,
        SPROC_GetManualDashboardDataListRebind,
        SPROC_GetManualDashboardDataListRebindV1,
        SPROC_GetCMODashboardData,
        SPROC_DBDMain_IndicatorUpdate,
        SPROC_DBDCalc_IndicatorUpdate,
        SPROC_DBDGenerate_IndicatorEffects,
        SPROC_GetManualDashboardDataStatList_SA,
        SPROC_GetDashboardDataStatList_SA,
        SPROC_GetDashboardBalanceSheetDataList_SA,
        SPROC_GetDashboardDataBalanceSheetDataList_SA,
        SPROC_GetDashboardDataGraph_SA,
        SPROC_GetDBBudgetManualBalanceSheetV1,
        SPROC_GetPatientFallRate_DashboardData,
        SPROC_UpdateProjectTaskTargetTaskNumber,
        SPROC_GetXMLMissingDataDetail,
        SPROC_DeleteCorporateData,
        SPROC_DefaultCorporateItems,
        SPROC_DeleteFacilityData,
        SPROC_SortDashboardIndicatorCols,
        SPROC_SaveBillingTableDataForTableNumber,
        SPROC_GetBillEditRoleUser,
        SPROC_UpdateTableNumberInBillingCodes,
        SPROC_GetDetailsByBillingSystemParametersId,
        SPROC_SaveEvaluationManagement,
        SPROC_UpdatePatientEvaluation,
        SPROC_UpdateOutPatientNurseAssessmentForm,
        SPROC_AddUpdateFacultyLunchTimming,
        //SPROC_CheckRoomsAndEquipmentsForScheduling,
        SPROC_CheckRoomsAndEquipmentsForScheduling_New,
        SProc_UpdatePatientEmailId,
        SPROC_GetTimeSlotAvailablity,
        SPROC_GetAvailableTimeSlotsList,
        SPROC_GetSchedulingEvents,
        SPROC_GetScrubberSummaryReport,
        SPROC_GetAvailableTimeSlotsList_KP,
        SPROC_GetErrorDetailReportByRuleCode,
        SPROC_GetErrorSummaryReportByRuleCode,
        SPROC_GET_REP_PhysicianChargeReportDetails,
        SPROC_GetTimeSlotAvialablotyForRecurrance,
        SPROC_GetPatientSchedulingEvents,
        SPROC_CreateRecurringEventsSchedular,
        SPROC_GetSchedulingEvents_Back,
        SPROC_GetUtilization,
        SPROC_CreateFacilityHolidays,
        SPROC_GetPatientNextSchedulingEvents,
        AvailableSlotsMonthlyView_V1,
        SPROC_CreateRecurringEventsFaculty,
        SPROC_CheckForDuplicateRecordFaculty,
        SPROC_AddFacultyDefaultTiming,
        SPROC_CreatePatientCarePlanActivites,
        SPROC_GetOrderTypeActivity,
        SPROC_GetPhyVacationsEvents,
        SPROC_InsertDrugExcelToDB,
        SPROC_CreatePartialOrderActvities,
        SPROC_GetFutureChargeReport,
        SPROC_AddFutureOrdersToCurrentEncounter,
        SPROC_ApprovePharmacyOrder,
        SPROC_MARView_V1,
        SPROC_GetPayerWiseFinalBills,
        SPROC_GetFinalBillsByPayer,
        SPROC_CreatePatientCarePlanActivites_V1,
        SPROC_ChangeCurrentOrderDetail,
        SPROC_DeletePatientCarePlanActivites,
        SPROC_GetDashBoardIndicators,
        SPROC_CreateOpenOrderActivityTimeForFrequency,
        SPROC_SortDashboardIndicatorCols_Bak,
        SPROC_MakeIndicatorInActive,
        SPROC_GetOrderActivitiesByEncounterId,
        SPROC_GetPhysicianOrders,
        SPROC_AddUpdateModuleAccess,
        SPROC_GetXMLReport_BatchReport,
        SPROC_GetXMLReport_InitialClaimErrorReport,
        SPROC_AddUpdateRolePermission,
        Delete_XMlParsedData_SA,
        SPROC_EncounterEndChecks_SA,
        SPROC_GetOverridableBeds,
        SPROC_CancelOpenOrder,
        SPROC_GetBarCodeDetailsByOrderActivityID,
        SPROC_GetOrdersByEncounterId,
        SPROC_GetPaitentFutureOrder,
        SPROC_UpdateBedRateCardByPassingServiceCodes,
        SPROC_GetXPaymentReturnDenialClaimsByPatientId,
        SPROC_FindEClaim,
        SPROC_GetXMLParsedDataRemittanceAdvice,
        SPROC_GetXMLParsedDataRemittanceAdviceById,
        SPROC_ApplyAdvicePaymentsByFileID,
        SPROC_GetRuleMasterByTableNumber,
        SendEClaimByPayerIDs,
        SPROC_UpdateBillDate,
        SPROC_AddVirtualDischargeLog,
        SPROC_EncounterEndCheckBillEdit_VirtualDischarge,
        SPROC_GetFacilityStructueChildParent,
        SPORC_GetEncounterData,
        SPROC_REP_PasswordChangelog,
        SPROC_REP_PasswordDisablelog,
        SPROC_Get_Rep_CorrectionLog,
        SPROC_REP_UserActivityLog,
        SPROC_GetBillFormatData,
        SPROC_GetReconcilationARPatientWise_Monthly,
        SPROC_GetReconcilationARPayorWise_Monthly,
        SPROC_GetReconcilationARPatientWise_Weekly,
        SPROC_GetReconcilationARPayorWise_Weekly,
        SPROC_BillHeaderList,
        SROC_SearchOrderingCodes,
        SPROC_GetOrdersData,
        SPROC_GetGlobalCodesByCategories,
        SPROC_GetFinalBillHeadersList,
        SPROC_SaveAllDashboardIndicatorData,
        SPROC_GetUsersRoleWise,
        SPROC_GetPhysiciansByFacility,
        SPROC_GetFacilityHolidays,
        SPROC_GetPhysicianRoleWise,
        SPROC_GetPhysiciansWithSchedulingApplied,
        CleanupAllDataByCorporate,
        SPROC_CheckRoomsAndEquipmentsInSchedulingUpdated,
        SPROC_GetRoomsByDepartments,
        SPROC_GetAppointmentRoomsAssignements,
        SPROC_GetSchedulerDataUpdated,
        SPROC_GetPatientSchedulerDataUpdated,
        SPROC_GetSchedulingAuditLog,
        SavePatientInfoInScheduler,
        SPROC_CreateRecurringEventsSchedularMultiple,
        SPROC_ValidateSchedulerAppointment
    }

    public enum OpenOrderActivityStatus
    {
        Open = 1,
        OpenAndAdministered = 2,
        Closed = 3,
        OnBill = 4,
        Cancel = 9,
        LabSectionWaitingForSpecimen = 20,
        LabSectionWaitingForResult = 30,
        PartiallyExecutedForResult = 40,
        WaitingForApproval = 50
    }

    public enum OrderStatus
    {
        Open = 1,
        OpenAndAdministered = 2,
        Closed = 3,
        OnBill = 4,
        Cancel = 9,
    }


    public enum ParamtersLevel
    {
        Corporate = 1073,
        Facility = 1074,
        Bill = 1075,
        Claim = 1076,
    }

    public enum ParamtersType
    {
        Single = 1077,
        Range = 1078,
    }

    public enum ParamtersDataType
    {
        Integer = 1079,
        Numeric = 1080,
        Date = 1081,
        String = 1082,
        Bool = 1083,
        LocalTimeType = 5127,
        DemoTimeType = 4125
    }

    public enum AttachmentType
    {
        ProfilePicture = 1,
        OnlineHelp = 99
    }


    public enum EncounterProcessStatus
    {
        NotStarted = 0,
        Intiailized = 1,
        Billed = 2,
    }

    public enum RHSFrom
    {
        Table = 1,
        DirectValue = 2,
        RangeValue = 3,
        CustomQuery = 4,
    }

    public enum TableNames
    {
        Authorization = 1,
        BillHeader = 2,
        Corporate = 3,
        Denial = 4,
        Diagnosis = 5,
        DiagnosisCode = 6,
        Drug = 7,
        Encounter = 8,
        Facility = 9,
        HCPCSCodes = 10,
        InsuranceCompany = 11,
        InsurancePlans = 12,
        InsurancePolices = 13,
        MCContract = 14,
        OpenOrder = 15,
        OpenOrderActivitySchedule = 16,
        OpenOrderActivityTime = 17,
        OrderActivity = 18,
        PatientInfo = 19,
        RuleMaster = 20,
        PatientInsurance = 21,
        BillActivity = 22,
        CPTCodes = 23,
        DRGCodes = 24,
        ServiceCode = 25
    }

    public enum ReportingType
    {
        UserLoginActivity = 1,
        PasswordDisablesLog = 2,
        PasswordChangeLog = 3,
        DailyChargeReport = 4,
        ChargesByPayorReport = 5,
        PayorWiseAgeingReport = 6,
        PatientWiseAgeingReport = 7,
        DepartmentWiseAgeingReport = 8,
        PayorWiseReconciliationReport = 9,
        PatientWiseReconciliationReport = 10,
        DepartmentWiseReconciliationReport = 11,
        RevenueForecastReport = 12,
        ClaimTransactionDetailReport = 13,
        DenialReport = 14,
        JournalEntrySupport = 15,
        JournalEntrySupportMCD = 16,
        JournalEntrySupportWO = 17,
        CorrectionLog = 18,
        ChargeReport = 19,
        ChargeDetailReport = 20,
        PhysicianActivityReport = 21,
        ScrubbeSummary = 22,
        ErrorDetail = 23,
        ErrorSummary = 24,
        DetailUserTransactions = 25,
        PhysicianUtilization = 26,
        DepartmentUtilization = 27,
        FutureOrders = 30,
        SchedulingActivityLog=31
    }

    public enum ReportingTypeActions
    {
        UserLoginActivityPdf = 1,
        PasswordDisablesLogPdf = 2,
        PasswordChangeLogPdf = 3,
        DailyChargeReportPdf = 4,
        ChargesByPayorReportPdf = 5,
        PayorWiseAgeingReport = 6,
        PatientWiseAgeingReport = 7,
        DepartmentWiseAgeingReport = 8,
        PayorWiseReconciliationReport = 9,
        PatientWiseReconciliationReport = 10,
        DepartmentWiseReconciliationReport = 11,
        OverviewAgingReport = 12,
        CorrectionLog = 18,
        ChargeReport = 19
    }


    public enum BillHeaderStatus
    {
        Initialized = 0,
        P = 45,
        E1 = 50,
        E2 = 100,
        E3 = 150,
        F1 = 55,
        F2 = 105,
        F3 = 155,
        S1 = 60,
        S2 = 110,
        S3 = 160,
        RA1 = 65,
        RA2 = 115,
        RA3 = 165,
        RD1 = 70,
        RD2 = 120,
        RD3 = 170,
        Authorization = 5,
        ManagedCare = 10,
        BothAuthAndManagedCare = 11,
        ReadyForPreliminary = 40
    }

    public enum QueryStringType
    {
        Encounter = 1,
        Patient = 2,
        BillHeader = 3,
    }

    public enum ReviewSummarySections
    {
        Approved = 1,
        Corrected = 2,
        BillEditErrors = 3,
        Denials = 4
    }

    public enum MCOrderType
    {
        Policy = 100,
        Encounter = 110,
    }

    public enum OrderTypeCategory
    {
        EvaluationandManagement = 11009,
        Anesthesia = 11001,
        Surgery = 11010,
        Radiology = 11070,
        PathologyandLaboratory = 11080,
        Medicine = 11090,
        Pharmacy = 11100,
        LabTest = 11020,
    }

    public enum ParamterSystemCode
    {
        LogOffTime = 130,
        OPEndTime = 200,
        ClaimDaysLimit = 300,
    }


    public enum RuleStepDataType
    {
        STRING = 1,
        BIT = 2,
        DATETIME = 3,
        DECIMAL = 4,
        INT = 5,
    }

    public enum ControllerAccess
    {
        Summary,              //EHR TAB
        PreliminaryBill,      //Bed Transactions View
        BillHeader,           //Bill Header View
        Authorization,        //Authorization,
        PatientInfo,          //Register New Patient, Edit Patient Details, 
        Encounter,            //Admit Patient, 
        Diagnosis,            //Additional Diagnosis View
        ActiveEncounter,      //Active Encounters, Addtional Diagnosis, Unclosed Orders (O/P and ER),
        PatientSearch,        //Encounter Views,
    }

    public enum ActionNameAccess
    {
        PatientSummary,             //EHR TAB
        Index,                      //General, Bed Transactions View, Bill Header View, Encounter Details
        AuthorizationMain,          //AuthorizationMain,
        RegisterPatient,            //Register New Patient, Edit Patient Details
        AdditionalDiagnosis,        //Additional Diagnosis
        ActiveEncounter,            //Active Encounter, Addtional Diagnosis
        PatientSearch,              //Encounter Views
        UnauthorizedView
    }


    public enum ChargesDashBoard
    {
        RoomGrossCharges = 10,
        AncillaryGrossCharges = 1018,
        OutpatientGrossCharges = 1019,
        EmergencyRoomGrossCharges = 1020,
        InpatientGrossRevenuePerPatientDay = 14,
        OutpatientRevenuePerEncounter = 15,
        ERpatientRevenuePerEncounter = 16,
        InpatientEncounters = 17,
        OutpatientEncounters = 1002,
        EREncounters = 1004,
        Admissions = 1015,
        Discharges = 1014,
        ALOS = 22,
        InpatientsADC = 1023,
        NumberofTotalClaimsSubmitted = 24,
        DollarAmountofTotalClaimsSubmitted = 25,
        NumberofTotalInpatientClaimsSubmitted = 26,
        DollarAmountofTotalInpatientClaimsSubmitted = 27,
        NumberofTotalOutpatientClaimsSubmitted = 28,
        DollarAmountoftotalOutpatientClaimsSubmitted = 29,
        NumberofTotalEmergencyRoomClaimsSubmitted = 30,
        DollarAmountoftotalEmergencyRoomClaimsSubmitted = 31,
        AverageDollarAmountofTotalClaims = 32,
        AverageDollarAmountofInpatientClaim = 33,
        AverageDollarAmountofOutpatientClaim = 34,
        AverageDollarAmountofEmergencyRoomClaim = 35,
        NumberofTotalClaimsPaidonRemittance = 36,
        NumberofTotalClaimsDeniedonRemittance = 37,
        ClaimsAcceptancePercentageFirstSubmission = 38,
        PatientDays = 1001,
        PharmacyOrders = 1011,
        LabTests = 1009,
        SurgeryMinutes = 1026,
        PhysicianExams = 1007,
        ManagedCareDiscounts = 1025,
        Writeoffs = 1024,
        PatientCashCollected = 1022,
        SugeryCPT = 1021,
        InsuranceCashCollected = 1016
    }

    public enum OperatingRoomTypes
    {
        Surgery = 1,
        Anesthesia = 2
    }

    public enum LoginTrackingTypes
    {
        UserLogin = 1,
        PatientLogin = 2
    }

    public enum DashboardType
    {
        ExecutiveDashboard = 1,
        SummaryDashboard = 2,
        ClinicalQulaityDashboard = 3,
        FinancialMGTDashboard = 4,
        RevenueMGTDashboard = 5,
        CaseMGTDashboard = 6,
        HumanResourcesDashboard = 7,
        ProjectsDashboard = 8,
        ExecutiveKeyPerformanceDashboard = 9,
        ClinicalCompliance = 10,
        BillScrubber = 11
    }

    public enum ExecutiveDashboardSection1Stat
    {
        Admissions = 101,
        Discharges = 102,
        InpatientDays = 103,
        OutpatientEncounters = 104,
        TotalOperatingBeds = 108,
        OccupancyRate = 109,
        AverageDailyCensus = 144,
        PatientDays = 242,
        AverageLengthofStay = 106,
        AdjustedPatientDays = 105
    }

    public enum ExecutiveDashboardSection5Stat
    {
        NetRevenue = 110,
        SWB = 111,
        OtherDirect = 113,
        OperatingMargin = 114,
        SWBIndirect = 155,
        IndirectCosts = 115,
        EBITDA = 116,
        DeprandAmort = 117,
        Interest = 118,
        NetIncomeLoss = 119,
        SWBPercentageofNetRevenue = 120,
        IndirectPercentageofNetRevenue = 121,
        EBITDAMargin = 122,
        NetIncomeMargin = 162,
        OperatingMarginPercentage = 145,
        NewMarketDevelopmentCosts = 260,
        PaidInterest = 261,
        FacilityRentUtilities = 280,
        MarketingBDCosts = 281,
        NewMarketDevelopmentOtherCosts = 282,
        ExpansionPreOpeningCosts = 610,
        Consumables = 609,
    }

    public enum ExecutiveDashboardSection10Stat
    {
        CashfromOperations = 124,
        ChangesinWorkingCapital = 125,
        OtherAdjustments = 126,
        CapitalExpenditures = 127,
        NetCashChange = 128,
        CashintheBank = 129,
        DaysofTotalExpendituresinCash = 130,
        ARNetDays = 131,
        NetCashflowfromOperations = 283,
        NetCashFlowfromInvesting = 284,
        IssueofDebt = 285,
        IssueofCapitalShare = 286,
        NetCashFlowfromFinancing = 287,
        OpeningCash = 288
    }

    public enum ExecutiveDashboardBalanceSheetStat
    {
        CashAndBank = 289,
        AccountsRecivable = 290,
        OtherReceivables = 291,
        Prepaids = 292,
        CurrentAssets = 293,
        FixedAssetsnet = 294,
        OtherAssets = 295,
        TotalAssets = 296,
        Accruals = 297,
        TradeandOtherPayables = 298,
        CurrentPortionLTD = 299,
        TotalCurrentLiabilities = 300,
        LongTermDebt = 301,
        TotalLiabilities = 302,
        ShareCapital = 303,
        RetainedEarnings = 304,
        TotalEquity = 305,
        TotalEquityLiabilities = 306,
        Prepayments = 600,
        LoansAdvances = 601,
        Inventory = 602,
        Investments = 603,
        RelatedPartyPayable = 604,
        AccrualsDeferredPayments = 605,
        Gratuity = 606,
        LoansLongTerm = 607,
        ProfitLoss = 608,
        DepositRentutilities = 1319,
        Lessaccumulateddepreciation = 1318,
        FixedAssets = 1317
    }

    public enum ExecutiveDashboardAcuteSection1Stat
    {
        Admissions = 101,
        Discharges = 102,
        InpatientDays = 103,
        OutpatientEncounters = 104,
        AdjustedPatientDays = 105,
        AverageLengthofStay = 106,
        AverageDailyCensus = 144,
        CaseMix = 107,
        TotalOperatingBeds = 108,
        OccupancyRate = 109,
    }

    public enum ExecutiveDashboardAcuteSection5Stat
    {
        NetRevenue = 110,
        SWB = 111,
        Supplies = 112,
        OtherDirect = 113,
        OperatingMargin = 114,
        SWBIndirect = 155,
        IndirectCosts = 115,
        EBITDA = 116,
        DeprandAmort = 117,
        Interest = 118,
        NetIncomeLoss = 119,
        SWBPercentageofNetRevenue = 120,
        IndirectPercentageofNetRevenue = 121,
        EBITDAMargin = 122,
        NetIncomeMargin = 162,
        OperatingMarginPercentage = 145
    }

    public enum ExecutiveDashboardAcuteSection10Stat
    {
        CashfromOperations = 124,
        ChangesinWorkingCapital = 125,
        OtherAdjustments = 126,
        CapitalExpenditures = 127,
        NetCashChange = 128,
        CashintheBank = 129,
        DaysofTotalExpendituresinCash = 130,
        ARNetDays = 131,
    }

    public enum OtherStats
    {
        ClinicalPositionsFilled = 132,
        ClinicalPositionsOpen = 133,
        TotalClinicalPositions = 134,
        AdmPositionsFilled = 135,
        AdmPositionsOpen = 136,
        TotalAdmPositions = 137,
        TotalPositions = 138,
        ClicnicalStaffingperPatientDay = 139,
        TotalStaffingperPatientDay = 140,
        AdmissionbyReferralSource = 141,
        NursetoPatientRatio = 142,
        CashCollections = 143,
    }

    public enum Section2Narrations
    {
        Section1 = 1,
        Section2 = 2,
        Section3 = 3,
        Section4 = 4
    }

    public enum Section6Narrations
    {
        Section5 = 5,
        Section6 = 6,
        Section7 = 7,
        Section8 = 8,
        Section9 = 9
    }

    public enum Section11Narrations
    {
        Section10 = 10,
        Section11 = 11,
        Section12 = 12,
    }


    public enum ExecutiveDashboardGraphs
    {
        Section3G1 = 144,
        Section3G2 = 156,
        Section4G1 = 109,
        Section4G2 = 104,
        Section7G1 = 120,
        Section7G2 = 110,
        Section8G1 = 121,
        Section8G2 = 122,
        Section9G1 = 159,
        Section9G2 = 141,
        Section12G1 = 124,
    }

    public enum DashboardProjectType
    {
        Strategic = 1,
        Operational = 2,
        Financial = 3,
        Individual = 4,
    }
    public enum ExternalDashboardColor
    {
        Green = 1,
        Yellow = 2,
        Red = 3,
    }

    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
    }

    public enum CalenderViewType
    {
        Day,
        Week,
        Month
    }

    public enum SchedularNotificationTypes
    {
        fromschedulartopatientonnewbooking = 1,
        patientreminderbefore3days = 2,
        patientreminderbefore1day = 3,
        patientReminderbeforeonappointmentday = 4,
        everymorningnotificationtophysician = 5,
        appointmenttypessubtemplate = 6,
        appointmentapprovaltophysician = 7,
        physicianapporovelemail = 8,
        physiciancancelappointment = 9,
        appointmentapprovedbyphysician = 10,
        preapprovedappointmentconfirm = 20,
        preapprovedappointmentcancel = 30,
    }

    public enum XmlReportingType
    {
        BatchReport = 1,
        XmlReportingInitialClaimErrorReport = 2
    }

    public enum ModulesAccessible
    {
        EhrView = 1,
        TransactionsView = 2,
        DiagnosisView = 3,
        BillHeaderView = 4,
        AuthorizationView = 5,
        PatientInfoView = 6,
        AdmitPatientView = 7,
        StartOpView = 8,
        DischargePatientView = 9,
        EndOpView = 10
    }
}