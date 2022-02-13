using System;
using System.Collections;

namespace ConnectorDEPKG
{
    // Decompiled with JetBrains decompiler
    // Type: THiRA.MES.UI.Infrastructure.Shell.Constant
    // Assembly: THiRA.MES.UI.Infrastructure.Shell.PRO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
    // MVID: 97BA0A01-0EEF-4FBC-BACC-3DC421A4F2FB
    // Assembly location: C:\Users\pebbl\AppData\Local\Apps\2.0\JNZ4LOWM.DTP\TB163BCR.EVN\thir..tion_0000000000000000_0002.0007_52a252dee6aa3c2a\THiRA.MES.UI.Infrastructure.Shell.PRO.exe
    
    public class Constant
    {
        public static string VERSION = string.Empty;
        public const string COMMA = ",";
        public const string DOT = ".";
        public const string SLASH = "/";
        public const string STAR = "*";
        public const string COLON = ":";
        public const string SEMICOLON = ";";
        public const string SPACE = " ";
        public const string PIPE = "|";
        public const string DASH = "-";
        public const string UNDERSCORE = "_";
        public const string ALL = "ALL";
        public const string Enter = "\r\n";
        public const int MAXTABCOUNT = 10;

        public enum MessageCommand
        {
            [Constant.DetailCommand("GetDataByQueryIDMulti")] Query,
            [Constant.DetailCommand("ExecuteProcedure")] Procedure,
            [Constant.DetailCommand("LogIn")] LogIn,
            [Constant.DetailCommand("LogOut")] LogOut,
            [Constant.DetailCommand("TrackInLot")] TrackInLot,
            [Constant.DetailCommand("TrackOutLot")] TrackOutLot,
            [Constant.DetailCommand("TrackOutLot_NEW")] TrackOutLot_NEW,
            [Constant.DetailCommand("TrackOutLot_EVA")] TrackOutLot_EVA,
            [Constant.DetailCommand("HoldLot")] Hold,
            [Constant.DetailCommand("MultiHoldLot")] MultiHold,
            [Constant.DetailCommand("ReleaseHoldLot")] Release,
            [Constant.DetailCommand("UpsertLotComment")] UpsertLotComment,
            [Constant.DetailCommand("SplitLot")] Split,
            [Constant.DetailCommand("RecastProcessSequence")] ReProcess,
            [Constant.DetailCommand("UpsertFutureAction")] FutureAction,
            [Constant.DetailCommand("GenerateIdPattern")] GenerateID,
            [Constant.DetailCommand("IncreaseLabelPrintCount")] RePrint,
            [Constant.DetailCommand("CancelWorkOrder")] CancelWo,
            [Constant.DetailCommand("SetFavoriteMenu")] SetFavorite,
            [Constant.DetailCommand("CheckPassword")] CheckPassword,
            [Constant.DetailCommand("ParallelEquipmentTrackOut")] ParallelEquipmentTrackOut,
            [Constant.DetailCommand("ParallelEquipmentTrackIn")] ParallelEquipmentTrackIn,
            [Constant.DetailCommand("CCLTrackOutLot")] CCLTrackOut,
            [Constant.DetailCommand("ManageEquipmentCommonCode")] ManageEquipmentCommonCode,
            [Constant.DetailCommand("ManageEquipmentComment")] ManageEquipmentComment,
            [Constant.DetailCommand("ChangeEquipmentState")] ChangeEquipmentState,
            [Constant.DetailCommand("BoxingLot")] BoxingLot,
            [Constant.DetailCommand("BoxingLotAfterFinish")] BoxingLotAfterFinish,
            [Constant.DetailCommand("ChangeEquipmentComments")] ChangeEquipmentComments,
            [Constant.DetailCommand("UpdateWaterUserPlan")] UpdateWaterUserPlan,
            [Constant.DetailCommand("InputWaterUsePlan")] InputWaterUsePlan,
            [Constant.DetailCommand("ManagePreventiveMaintenance")] ManagePreventiveMaintenance,
            [Constant.DetailCommand("TrackOutCCLMaterialLot")] CCLTrackOutNormal,
            [Constant.DetailCommand("TrackInRequest")] TrackInRequestTODas,
            [Constant.DetailCommand("CancelTrackInLot")] CancelTrackIn,
            [Constant.DetailCommand("GenerateBoxingId")] GenerateBoxID,
            [Constant.DetailCommand("GenerateIdPattern")] GenerateIDPattern,
            [Constant.DetailCommand("UpsertTable")] UPSERT,
            [Constant.DetailCommand("IssueReport")] IssueReport,
            [Constant.DetailCommand("PPCuttingResultUp")] PPFoundationResult,
            [Constant.DetailCommand("BundlePackingLot")] BundlePackingLot,
            [Constant.DetailCommand("CancelBundlePackingLot")] CancelBundlePackingLot,
            [Constant.DetailCommand("PreventCheckTableRegiste")] PreventCheckTableRegiste,
            [Constant.DetailCommand("PreventCheckResultRegister")] PreventCheckResultRegister,
            [Constant.DetailCommand("EventTroubleCheckRequest")] EventTroubleCheckRequest,
            [Constant.DetailCommand("EventTroubleCheckRegister")] EventTroubleCheckRegister,
            [Constant.DetailCommand("GetSAPInterfacePackingInfo")] SAP_RFC,
            [Constant.DetailCommand("GetSAPInterfaceLotHistInfo")] SAP_RFC_LotInfo,
            [Constant.DetailCommand("UpsertUserClass")] UpsertUserClass,
            [Constant.DetailCommand("PPCuttingResultUp2")] PPFoundationResult2,
            [Constant.DetailCommand("RequestCCLLot")] RequestCCLQty,
            [Constant.DetailCommand("TrackOutCCLMaterialLot")] TrackOutCCLMaterialLot,
            [Constant.DetailCommand("TrackOutCCLMaterialLot_NEW")] TrackOutCCLMaterialLot_NEW,
            [Constant.DetailCommand("AutoTrackingLot")] AutoTrackingLot,
            [Constant.DetailCommand("UpsertTable")] UpsertTable,
            [Constant.DetailCommand("AutoTrackingLot")] AutoTracking,
            [Constant.DetailCommand("CreateUpdateTable")] CreateuUpdate,
            [Constant.DetailCommand("HotTypeLot")] HotTypeLot,
            [Constant.DetailCommand("CheckEqpConstraint")] CheckEqpConstraint,
            [Constant.DetailCommand("SendEmail")] SendEmail,
            [Constant.DetailCommand("RackInputLot")] RackInputLot,
            [Constant.DetailCommand("RackOutputChoiceLot")] RackOutputChoiceLot,
            [Constant.DetailCommand("AutoTrackInLot")] AutoTrackInLot,
            [Constant.DetailCommand("AutoTrackOutLot")] AutoTrackOutLot,
            [Constant.DetailCommand("PopLotCommentManagements")] PopLotCommentManagements,
            [Constant.DetailCommand("UpsertParallelLotDefectUp")] UpsertParallelLotDefectUp,
            [Constant.DetailCommand("PopInputLossOper")] PopInputLossOper,
            [Constant.DetailCommand("InActivePlanRegister")] InActivePlanRegister,
            [Constant.DetailCommand("MarkingPackTypeMaster")] MarkingPackTypeMaster,
            [Constant.DetailCommand("PsrInkBarcode")] PsrInkBarcode,
            [Constant.DetailCommand("IF_EAI_MES_SAP_0019")] IF_EAI_MES_SAP_0019,
            [Constant.DetailCommand("IF_EAI_MES_SAP_0005")] IF_EAI_MES_SAP_0005,
            [Constant.DetailCommand("IF_EAI_MES_SAP_0008")] IF_EAI_MES_SAP_0008,
            [Constant.DetailCommand("EESDefectRegister_TrackOutLot_NEW")] EESDefectRegister_TrackOutLot_NEW,
            [Constant.DetailCommand("EESDefectRegister_CancelTrackOutLot")] EESDefectRegister_CancelTrackOutLot,
            [Constant.DetailCommand("EapAlarmReport")] EapAlarmReport,
            [Constant.DetailCommand("LabelBoxPacking")] LabelBoxPacking,
            [Constant.DetailCommand("CancelLabelBoxPacking")] CancelLabelBoxPacking,
            [Constant.DetailCommand("LabelTagRemark")] LabelTagRemark,
            [Constant.DetailCommand("CancelLotSelection")] CancelLotSelection,
            [Constant.DetailCommand("CancelSplitLot")] CancelSplitLot,
            [Constant.DetailCommand("CancelSplitLotEAI")] CancelSplitLotEAI,
            [Constant.DetailCommand("ExpectReleaseDateChange")] ExpectReleaseDateChange,
            [Constant.DetailCommand("FBReportSearchSave")] FBReportSearchSave,
            [Constant.DetailCommand("RepairOutLot")] RepairOutLot,
            [Constant.DetailCommand("ProcMoveMaster")] ProcMoveMaster,
            [Constant.DetailCommand("ProcMoveRequest")] ProcMoveRequest,
            [Constant.DetailCommand("ProcMoveCancel")] ProcMoveCancel,
            [Constant.DetailCommand("ProcMoveRegister")] ProcMoveRegister,
            [Constant.DetailCommand("MoveReceiveLot")] MoveReceiveLot,
            [Constant.DetailCommand("OutSideOrderSheet_In")] OutSideOrderSheet_In,
            [Constant.DetailCommand("MoveSendLot")] MoveSendLot,
            [Constant.DetailCommand("OutSideOrderSheet")] OutSideOrderSheet,
            [Constant.DetailCommand("UpsertRecreate")] UpsertRecreate,
            [Constant.DetailCommand("WorkCenterChangeSave")] WorkCenterChangeSave,
            [Constant.DetailCommand("WorkCenterChangeSendToEai")] WorkCenterChangeSendToEai,
            [Constant.DetailCommand("CancelSendLot")] CancelSendLot,
            [Constant.DetailCommand("CancelOutSideOrderSheet")] CancelOutSideOrderSheet,
            [Constant.DetailCommand("OnlineStateChange_OIC")] OnlineStateChange_OIC,
            [Constant.DetailCommand("LotReserveCancel_OIC")] LotReserveCancel_OIC,
            [Constant.DetailCommand("LotReserveInfoChange_OIC")] LotReserveInfoChange_OIC,
            [Constant.DetailCommand("LotTrackOutRequest_OIC")] LotTrackOutRequest_OIC,
            [Constant.DetailCommand("LotTrackOutRequest")] LotTrackOutRequest,
            [Constant.DetailCommand("LotReserveRequest_OIC")] LotReserveRequest_OIC,
            [Constant.DetailCommand("FBReport")] PopFBReportSend,
            [Constant.DetailCommand("FBReportIrCardSave")] PopFBReportIrCardSave,
            [Constant.DetailCommand("FBReportWorstDefectInfoSave")] PopFBReportWorstDefectInfoSave,
            [Constant.DetailCommand("OutGoingInspection")] OutGoingInspection,
            [Constant.DetailCommand("PopInputAdditionItem")] PopInputAdditionItem,
            [Constant.DetailCommand("PopAdditionItem")] PopAdditionItem,
            [Constant.DetailCommand("MG3MaterialCheck")] MG3MaterialCheck,
            [Constant.DetailCommand("GetFileInfoByFTP")] GetFileInfoByFTP,
            [Constant.DetailCommand("WhseInfoRequest")] WhseInfoRequest,
            [Constant.DetailCommand("UpsertWorkOrderPrintCnt")] UpsertWorkOrderPrintCnt,
            [Constant.DetailCommand("RackInfoRequest")] RackInfoRequest,
            [Constant.DetailCommand("RackReleaseRequest")] RackReleaseRequest,
            [Constant.DetailCommand("DryFilmStockInsert")] DryFilmStockInsert,
            [Constant.DetailCommand("UpsertSPCmeasureData")] UpsertSPCmeasureData,
            [Constant.DetailCommand("InsertLotSourceInspection")] InsertLotSourceInspection,
            [Constant.DetailCommand("ReworkInspectionProcess")] ReworkInspectionProcess,
        }

        public class DetailCommand : Attribute
        {
            private string DetailString;

            public DetailCommand(string detailString) => this.DetailString = detailString;

            public override string ToString() => this.DetailString;
        }

        public class MessageElement
        {
            public const string QueryID = "queryId";
            public const string QueryVersion = "queryVersion";
            public const string ReturnID = "returnId";
            public const string ParamList = "paramList";
            public const string WorkCenter = "workCenter";
            public const string ProcessSegmentID = "processSegmentId";
            public const string EquipmentID = "equipmentId";
            public const string LotID = "lotId";
            public const string Comments = "comments";
            public const string UserID = "userId";
            public const string Password = "password";
            public const string SiteID = "siteId";
            public const string PrtCode = "labelPrintCode";
            public const string FacilityID = "facilityId";
            public const string ProcessNodeId = "processNodeId";
            public const string ProductDefinitionId = "productDefinitionId";
            public const string ProcessDefinitionId = "processDefinitionId";
            public const string ProcessNodeType = "processNodeType";
            public const string IsStartNode = "isStartNode";
            public const string IsEndNode = "isEndNode";
            public const string GroupCode = "groupCode";
            public const string BusinessUnit = "businessUnit";
            public const string CommentType = "commentType";
            public const string SPECNR = "SPECNR";
            public const string ActionComment = "actionComment";
            public const string IsPopUp = "isPopUp";
            public const string Status = "status";
            public const string PRDHA = "PRDHA";
            public const string SpecType = "specType";
            public const string Judge = "judge";
            public const string QueryClassId = "queryClassId";
            public const string QueryRequestType = "queryRequestType";
        }

        public class QueryClassDefinition
        {
            public const string Common = "MES_UI_COM";
            public const string EMS = "MES_UI_EMS";
            public const string HMS = "MES_UI_HMS";
            public const string JIG = "MES_UI_JIG";
            public const string JMS = "MES_UI_JMS";
            public const string LPS = "MES_UI_LPS";
            public const string MMS = "MES_UI_MMS";
            public const string MGT = "MES_UI_SYSTEMMGT";
            public const string MNT = "MES_UI_MNT";
            public const string OMS = "MES_UI_OMS";
            public const string PMS = "MES_UI_PMS";
            public const string PPS = "MES_UI_PPS";
            public const string QMS = "MES_UI_QMS";
            public const string REPORT = "MES_UI_REPORT";
            public const string SMS = "MES_UI_SMS";
            public const string TEC = "MES_UI_TEC";
            public const string WMS = "MES_UI_WMS";
            public const string SPC = "MES_UI_SPC";
            public const string OIBusiness = "BizRule";
        }

        public class SPECTYPE
        {
            public const string BVH = "BVH";
            public const string BVHC = "BVH-C";
        }

        public class AlarmState
        {
            public const string Active = "Active";
            public const string Clear = "Clear";
            public const string Processing = "Processing";
        }

        public class CarrierState
        {
            public const string Active = "Active";
            public const string Cleaning = "Cleaning";
            public const string Created = "Created";
            public const string Hold = "Hold";
            public const string Terminated = "Terminated";
        }

        public class EquipmentState
        {
            public const string Down = "Down";
            public const string Hold = "Hold";
            public const string Idle = "Idle";
            public const string PM = "PM";
            public const string Run = "Run";
        }

        public class EquipmentClassType
        {
            public const string PRESS = "PRS";
            public const string TDRILL = "TDR";
        }

        public class LotState
        {
            public const string Active = "Active";
            public const string Converted = "Converted";
            public const string Created = "Created";
            public const string Finished = "Finished";
            public const string Hold = "Hold";
            public const string Scrapped = "Scrapped";
            public const string Shipped = "Shipped";
            public const string Terminated = "Terminated";
        }

        public class MESState
        {
            public const string Wait = "Wait";
            public const string Processing = "Processing";
            public const string WaitForSend = "WaitForSend";
            public const string InTransit = "InTransit";
            public const string WaitForInspection = "WaitForInspection";
            public const string WaitForReceiptSegment = "WaitForReceiptSegment";
            public const string ProcessingInspection = "ProcessingInspection";
            public const string WaitForLayup = "WaitForLayup";
            public const string HoldInspection = "HoldInspection";
            public const string WaitForReceive = "WaitForReceive";
            public const string WaitForSpcInput = "WaitForSpcInput";
            public const string Wait_Distribution_Rack = "Wait_Distribution_Rack";
            public const string WaitForSend_Rack = "WaitForSend_Rack";
            public const string InTransit_Rack = "InTransit_Rack";
            public const string Wait_Proc_Rack = "Wait_Proc_Rack";
            public const string InTransit_AGV = "InTransit_AGV";
            public const string Proc_Move = "Proc_Move";
        }

        public class LotProcessingState
        {
            public const string WaitForSegment = "WaitForSegment";
            public const string WaitForRule = "WaitForRule";
            public const string ProcessingRule = "ProcessingRule";
        }

        public class MaterialState
        {
            public const string Active = "Active";
            public const string Created = "Created";
            public const string Hold = "Hold";
            public const string Inventory = "Inventory";
            public const string Scrapped = "Scrapped";
            public const string Terminated = "Terminated";
        }

        public class ProductOrderState
        {
            public const string Active = "Active";
            public const string Created = "Created";
            public const string Hold = "Hold";
            public const string Terminated = "Terminated";
        }

        public class ParallelState
        {
            public const string IN = "IN";
            public const string OUT = "OUT";
        }

        public class MenuType
        {
            public const string Group = "Group";
            public const string Item = "Item";
        }

        public class CodelClass
        {
            public const string LOTHOLD = "LOTHOLD";
            public const string LOTHOLDRELEASE = "LOTHOLDRELEASE";
            public const string LOTSCRAP = "LOTSCRAP";
            public const string LOTUNSCRAP = "LOTUNSCRAP";
            public const string LOTREWORK = "LOTREWORK";
            public const string LOTREPAIR = "LOTREPAIR";
            public const string EQUIPMENTHOLD = "EQUIPMENTHOLD";
            public const string BBT = "BBT";
            public const string PinCharge = "PinCharge";
            public const string TSide = "TSide";
            public const string PcbFvi = "PcbFvi";
            public const string New = "NEW";
            public const string EquipmentType = "DETAILEQUIPMENTTYPE";
            public const string EquipmentModel = "EQUIPMENTTYPE";
            public const string EquipmentGroup = "EQUIPMENTCATEGORY";
            public const string Location = "LOCATION";
            public const string UserType = "USERTYPE";
            public const string ToolCategory = "TOOLCATEGORY";
            public const string JIGToolType = "JIGTOOLTYPE";
            public const string AutoSetOutsourcing = "AUTOSETOUTSOURCING";
            public const string PRDHASmall = "PRDHASMALL";
            public const string ISPRINT = "ISPRINT";
            public const string ORDERTYPE = "OrderType";
            public const string ISFINISHED = "ISFINISHED";
            public const string PRINTCONDITION = "PRINTCONDITION";
            public const string SPECTYPE = "SPECTYPE";
            public const string FERT = "FERT";
        }

        public class CodeType
        {
            public const string UI = "UI";
            public const string SPC = "SPC";
            public const string EQUIPMENT = "EQUIPMENT";
            public const string UTILITYEQUIPMENTTYPE = "UTILITYEQUIPMENTTYPE";
        }

        public class FacilityType
        {
            public const string SITE = "SITE";
            public const string BUSINESSUNIT = "BUSINESSUNIT";
            public const string WORKCENTER = "WORKCENTER";
        }

        public class IsUsable
        {
            public const string Usable = "Usable";
            public const string Unusable = "Unusable";
        }

        public class Request
        {
            public const string Create = "CREATE";
            public const string Update = "UPDATE";
            public const string Delete = "DELETE";
            public const string RealDelete = "REALDELETE";
            public const string UpdateCheckListSpecLimitFlag = "UPDATE_CHECKLIST_SPEC_LIMIT_FLAG";
        }

        public class IsFlag
        {
            public const string Y = "Y";
            public const string N = "N";
        }

        public class SegmentType
        {
            public const string Segment = "Segment";
        }

        public class VendorType
        {
            public const string Outsourcing = "Outsourcing";
            public const string Customer = "Customer";
        }

        public class UnitType
        {
            public const string EA = "EA";
            public const string G = "G";
            public const string KG = "KG";
            public const string PCS = "PC";
            public const string PNL = "PNL";
            public const string ROL = "ROL";
            public const string SHT = "SHT";
            public const string Strip = "Strip";
        }

        public class UserType
        {
            public const string SINGLE = "Single";
            public const string GROUP = "Group";
        }

        public class UserClassType
        {
            public const string MENU = "Menu";
            public const string BUSINESSUNIT = "BusinessUnit";
            public const string SEGMENT = "Segment";
            public const string ALARMEMAIL = "AlarmEmail";
            public const string ALARMSMS = "AlarmSMS";
            public const string LOTHOLDRELEASE = "LotHoldRelease";
        }

        public class FunctionType
        {
            public const string Search = "SEARCH";
            public const string Send = "SEND";
            public const string Receive = "RECEIVE";
            public const string Save = "SAVE";
            public const string Link = "LINK";
            public const string PopUp = "POPUP";
        }

        public class DurableType
        {
            public const string A = "A";
            public const string B = "B";
            public const string C = "C";
        }

        public class StateModel
        {
            public const string EquipmentState = "EquipmentState";
        }

        public class LotType
        {
            public const string MAIN = "MAIN";
            public const string SUB = "SUB";
            public const string CCL = "CCL";
        }

        public class QTimeActionType
        {
            public const string Error = "ERROR";
        }

        public class ToolingType
        {
            public const string NOTEXIST = "X";
            public const string NEW = "N";
            public const string MODIFY = "M";
        }

        public class ItemType
        {
            public const string SCALE = "SCALE";
            public const string THICKNESS = "THICKNESS";
        }

        public class SubCategory
        {
            public const string SUPER = "SUPER";
            public const string MAIN = "MAIN";
            public const string SUB = "SUB";
        }

        public class InputType
        {
            public const string IN = "In";
            public const string OUT = "Out";
            public const string WAIT = "Wait";
        }

        public class DetailSegmentType
        {
            public const string Packing = "Packing";
        }

        public class MeasureDataType
        {
            public const string DrillMethod = "DRILLMETHOD";
            public const string DrillType = "DRILLTYPE";
        }

        public class WorkType
        {
            public const string TrackIn = "TrackInLot";
            public const string TrackOut = "TrackOutLot";
            public const string Main = "Main";
        }

        public class BusinessUnit
        {
            public const string HDI = "HDI";
            public const string HDIYT = "HDIYT";
            public const string MLB = "MLB";
            public const string MLBDEINS = "MLBDEINS";
            public const string PKG1 = "PKG1";
            public const string PKG2 = "PKG2";
            public const string DPCENTER = "DPCENTER";
        }

        public class Location
        {
            public const string HDI = "HS";
            public const string HDIYT = "YS";
            public const string MLB = "MS";
            public const string MLBDEINS = "DS";
            public const string PKG1 = "PS";
            public const string PKG2 = "SS";
            public const string BGA1 = "BA";
            public const string BGA2 = "BB";
            public const string BGA3 = "BC";
            public const string DPCENTER = "DP";
        }

        public class Site
        {
            public const string PCB = "1110";
            public const string PKG = "1130";
            public const string DDPI = "2510";
        }

        public class PrevieAlarmType
        {
            public const string Day = "일";
            public const string Time = "시간";
            public const string First = "첫째";
            public const string Second = "둘째";
            public const string Third = "셋째";
            public const string Forth = "넷째";
            public const string Fifth = "다섯째";
            public const string Monday = "월요일";
            public const string Tuesday = "화요일";
            public const string Wendsday = "수요일";
            public const string Thursday = "목요일";
            public const string Friday = "금요일";
            public const string Saturday = "토요일";
            public const string Sunday = "일요일";
        }

        public class ColumnType
        {
            public const string Text = "Text";
            public const string Integer = "Integer";
            public const string Float = "Float";
            public const string ComboBoxQuery = "ComboBoxQuery";
            public const string ComboBoxExpression = "ComboBoxExpression";
            public const string Date = "Date";
            public const string CheckBox = "CheckBox";
        }

        public class VendorInputType
        {
            public const string ALL = "ALL";
            public const string SHIPTO = "S";
            public const string PRODUCT = "P";
            public const string MODEL = "M";
            public const string SHIPTO_PRODUCT = "SP";
            public const string SHIPTO_MODEL = "SM";
            public const string PRODUCT_MODEL = "PM";
            public const string SHIPTO_PRODUCT_MODEL = "SPM";
        }

        public class PolicyType
        {
            public const string MATERIAINPUTMANAGEMENT = "MATERIALINPUTMANAGEMENT";
            public const string INLINEPROCESSING = "INLINEPROCESSING";
            public const string ISREPAIR = "ISREPAIR";
            public const string ISMATERIALQTYINPUT = "ISMATERIALQTYINPUT";
            public const string ISIMPORTINSPECTION = "ISIMPORTINSPECTION";
            public const string ISQCSAMPLING = "ISQCSAMPLING";
            public const string AUTOSENDOUTSOURCING = "AUTOSENDOUTSOURCING";
            public const string ISNOTSPLITLOT = "ISNOTSPLITLOT";
            public const string ISNOTVIEWMATERIAL = "ISNOTVIEWMATERIAL";
            public const string ISPREVIOUSMATERIALINPUTFLAG = "ISPREVIOUSMATERIALINPUTFLAG";
            public const string LIQUIDANALYSISSEGMENT = "LIQUIDANALYSISSEGMENT";
            public const string LIQUIDANALYSISOUTSOURCING = "LIQUIDANALYSISOUTSOURCING";
            public const string MULTITRACKIN = "MULTITRACKIN";
            public const string MULTITRACKOUT = "MULTITRACKOUT";
            public const string ISPOSSIBLEREPAIR = "ISPOSSIBLEREPAIR";
            public const string SKIPDESIGNATE = "SKIPDESIGNATE";
            public const string ISSPCSPECEXISTVALIDATION = "ISSPCSPECEXISTVALIDATION";
            public const string STRIPDEFECT = "STRIPDEFECT";
            public const string TRANSFERSMALLGROUP = "TRANSFERSMALLGROUP";
            public const string TRANSFERMIDDLEGROUP = "TRANSFERMIDDLEGROUP";
            public const string WORKRANKNOT = "WORKRANKNOT";
            public const string EQUIPMENTCHANGENOT = "EQUIPMENTCHANGENOT";
            public const string SEGMENTPMNOT = "SEGMENTPMNOT";
            public const string TRACKIN_RECIPE_BARCODE = "TRACKIN_RECIPE_BARCODE";
            public const string CHECKLIST_EQUIPMENTDOWN = "CHECKLIST_EQUIPMENTDOWN";
            public const string WEEKALARM = "WEEKALARM";
            public const string CHEMICALNOTICE_EQUIPMENTDOWN = "CHEMICALNOTICE_EQUIPMENTDOWN";
        }

        public class ColumnList
        {
            protected Hashtable ColumnTable;

            public ColumnList()
            {
                this.ColumnTable = new Hashtable();
                this.ColumnTable.Add((object)"CHK", (object)30);
                this.ColumnTable.Add((object)"LotID", (object)120);
                this.ColumnTable.Add((object)"LOTNAME", (object)120);
                this.ColumnTable.Add((object)"HotType", (object)50);
                this.ColumnTable.Add((object)"KUNNR", (object)80);
                this.ColumnTable.Add((object)"ProductDefinition", (object)120);
                this.ColumnTable.Add((object)"ProductRevision", (object)50);
                this.ColumnTable.Add((object)"SPECNR", (object)100);
                this.ColumnTable.Add((object)"ProcessState", (object)100);
                this.ColumnTable.Add((object)"PieceQty", (object)50);
                this.ColumnTable.Add((object)"StripQty", (object)50);
                this.ColumnTable.Add((object)"PannelQty", (object)50);
                this.ColumnTable.Add((object)"SqareMeter", (object)50);
                this.ColumnTable.Add((object)"FTRMI", (object)80);
                this.ColumnTable.Add((object)"VDATU", (object)80);
                this.ColumnTable.Add((object)"WindowTime", (object)60);
                this.ColumnTable.Add((object)"RunningTime", (object)70);
                this.ColumnTable.Add((object)"NextProcessSegmentName", (object)120);
                this.ColumnTable.Add((object)"WaitingTime", (object)80);
                this.ColumnTable.Add((object)"POTX1", (object)40);
                this.ColumnTable.Add((object)"LAYERNR", (object)40);
                this.ColumnTable.Add((object)"Hold", (object)40);
                this.ColumnTable.Add((object)"Method", (object)60);
                this.ColumnTable.Add((object)"AUFNR", (object)100);
                this.ColumnTable.Add((object)"ProcessSegmentID", (object)60);
                this.ColumnTable.Add((object)"MesProcessState", (object)100);
                this.ColumnTable.Add((object)"USR01", (object)60);
                this.ColumnTable.Add((object)"SITEID", (object)50);
                this.ColumnTable.Add((object)"LSL", (object)150);
                this.ColumnTable.Add((object)"TARGET", (object)150);
                this.ColumnTable.Add((object)"USL", (object)150);
                this.ColumnTable.Add((object)"DATA_DESC_KO", (object)150);
                this.ColumnTable.Add((object)"ITEM_NAME", (object)250);
            }

            public int GetColumnSize(string columnKey, int defaultWidth) => !this.ColumnTable.ContainsKey((object)columnKey) ? defaultWidth : DepkgHelper.getInteger(this.ColumnTable[(object)columnKey], 100);
        }

        public class EmailClassType
        {
            public const string BM = "BME";
            public const string PM = "PME";
        }

        public class SMSClassType
        {
            public const string BM = "BMS";
            public const string PM = "PMS";
        }

        public class EQUIPMENTPROPERTY
        {
            public const string FACTORYOWNER = "FACTORYOWNER";
            public const string VENDOR = "VENDOR";
        }

        public enum RangeOver
        {
            Normal,
            Over,
            Down,
        }

        public class UTILITYEQUIPMENTTYPE
        {
            public const string UTILITY = "UTILITY";
            public const string FILM = "FILM";
            public const string REASERCH = "REASERCH";
        }

        public class DeliveryType
        {
            public const string PERIODSCHEDULE = "납기예정";
            public const string PACKINGRESULT = "포장실적";
        }

        public class CHKLST_STATUS
        {
            public const string WAIT = "CHKLST_STATUS_WAIT";
            public const string CHECK = "CHKLST_STATUS_CHECK";
            public const string UNCHECK = "CHKLST_STATUS_UNCHECK";
            public const string DELAY = "CHKLST_STATUS_DELAY";
            public const string PASS = "CHKLST_STATUS_PASS";
        }

        public class CHKLST_PASSTYPE
        {
            public const string PM = "CHKLST_PASSTYPE_PM";
            public const string DAYOFF = "CHKLST_PASSTYPE_DAYOFF";
            public const string NONE = "CHKLST_PASSTYPE_NONE";
            public const string BM = "CHKLST_PASSTYPE_BM";
        }

        public class CHKLST_SPECTYPE
        {
            public const string UPPER = "CHKLST_SPECTYPE_UPPER";
            public const string LOWER = "CHKLST_SPECTYPE_LOWER";
            public const string BETWEEN = "CHKLST_SPECTYPE_BETWEEN";
            public const string TARGET = "CHKLST_SPECTYPE_TARGET";
            public const string LIMIT = "CHKLST_SPECTYPE_LIMIT";
        }

        public class CHKLST_METHOD
        {
            public const string ISOK = "CHKLST_METHOD_ISOK";
            public const string SPEC = "CHKLST_METHOD_SPEC";
            public const string ALL = "CHKLST_METHOD_ALL";
        }

        public class SHIFT_COUNT
        {
            public const string AB = "2";
            public const string ABC = "3";
        }

        public class CHKLST_CHECKCYCLE
        {
            public const string SHIFT = "CHKLST_CHECKCYCLE_SHIFT";
            public const string DAY = "CHKLST_CHECKCYCLE_DAY";
            public const string WEEK = "CHKLST_CHECKCYCLE_WEEK";
            public const string MONTH = "CHKLST_CHECKCYCLE_MONTH";
        }

        public class WS_SORTTYPE
        {
            public const string C = "WS_SORTTYPE_C";
            public const string L = "WS_SORTTYPE_L";
            public const string R = "WS_SORTTYPE_R";
        }

        public class WS_DISPLAYTYPE
        {
            public const string H = "WS_DISPLAYTYPE_H";
            public const string I = "WS_DISPLAYTYPE_I";
            public const string S = "WS_DISPLAYTYPE_S";
        }

        public class WS_INPUTTYPE
        {
            public const string COMBOBOX = "WS_INPUTTYPE_COMBOBOX";
            public const string TEXT = "WS_INPUTTYPE_TEXT";
            public const string CHECKBOX = "WS_INPUTTYPE_CHECKBOX";
        }

        public class WS_DATATYPE
        {
            public const string CODE = "WS_DATATYPE_CODE";
            public const string DATE = "WS_DATATYPE_DATE";
            public const string FLOAT = "WS_DATATYPE_FLOAT";
            public const string INT = "WS_DATATYPE_INT";
            public const string TEXT = "WS_DATATYPE_TEXT";
            public const string TIME = "WS_DATATYPE_TIME";
        }

        public class WS_TABLE_TYPE
        {
            public const string WORKSHEET_LOT = "DD_REPORT_WORKSHEET_LOT";
            public const string WORKSHEET_LOTITEM = "DD_REPORT_WORKSHEET_LOTITEM";
        }

        public class ISSPECHECK
        {
            public const string OK = "OK";
            public const string NG = "NG";
            public const string NAN = "NAN";
        }

        public class INSPECTION_SEGMENTQTYTYPE
        {
            public const string PCS = "INSPECTION_SEGMENTQTYTYPE_PCS";
            public const string PLN = "INSPECTION_SEGMENTQTYTYPE_PNL";
            public const string POINT = "INSPECTION_SEGMENTQTYTYPE_POINT";
            public const string STRIP = "INSPECTION_SEGMENTQTYTYPE_STRIP";
            public const string OK = "INSPECTION_SEGMENTQTYTYPE_OK";
        }

        public class INSPECTIONSHEETTYPE
        {
            public const string INITIAL = "INSPECTIONSHEETTYPE_INITIAL";
            public const string INITIAL_MASS = "INSPECTIONSHEETTYPE_INITIAL_MASS";
            public const string MASS = "INSPECTIONSHEETTYPE_MASS";
            public const string OK = "INSPECTIONSHEETTYPE_OK";
        }

        public class WS_STATUS
        {
            public const string TRACKIN = "WS_STATUS_TRACKIN";
            public const string TRACKOUT = "WS_STATUS_TRACKOUT";
            public const string TRACKIN_CANCEL = "WS_STATUS_TRACKIN_CANCEL";
            public const string TRACKOUT_CANCEL = "WS_STATUS_TRACKOUT_CANCEL";
            public const string MANUAL = "WS_STATUS_MANUAL";
        }

        public class JIGPM_SPECTYPE
        {
            public const string UPPER = "JIGPM_SPECTYPE_UPPER";
            public const string LOWER = "JIGPM_SPECTYPE_LOWER";
            public const string BETWEEN = "JIGPM_SPECTYPE_BETWEEN";
            public const string TARGET = "JIGPM_SPECTYPE_TARGET";
        }

        public class CHKLST_CHECKITEMTYPE
        {
            public const string CHECKITEM = "CHKLST_CHECKITEMTYPE_CHECKITEM";
            public const string QPOINT = "CHKLST_CHECKITEMTYPE_QPOINT";
            public const string SEGMENTPMCHECKITEM = "CHKLST_CHECKITEMTYPE_SEGMENTPMCHECKITEM";
        }

        public class CHKLST_SEGMENTPM_CHECKTYPE
        {
            public const string PM_A = "CHKLST_PM_A";
            public const string PM_B = "CHKLST_PM_B";
        }

        public class ProductType
        {
            public const string NORMAL = "NORMAL";
            public const string PRERUN = "PRERUN";
            public const string DUMMY = "DUMMY";
            public const string REPRERUN = "REPRERUN";
            public const string REWORK = "REWORK";
            public const string PARALLEL_NORMAL = "PARALLEL_NORMAL";
            public const string PARALLEL_PRERUN = "PARALLEL_PRERUN";
        }

        public class ReserveState
        {
            public const string RESERVEREQ = "RESERVEREQ";
            public const string RESERVED = "RESERVED";
            public const string STARTED = "STARTED";
            public const string FAIL = "FAIL";
        }

        public class EquipmentMode
        {
            public const string OFFLINE = "Offline";
            public const string REMOTE = "Remote";
            public const string LOCAL = "Local";
        }

        public class ConstItem
        {
            public const string MATERIAL_ID = "MATERIALID";
            public const string MATERIAL_GROUP = "MATKL";
            public const string MATERIAL_GROUP_NAME = "MTART";
            public const string VENDOR_ID = "VENDORID";
            public const string VENDOR_NAME = "VENDORNAME";
            public const string MATERIAL_STANDARD_QTY = "STANDARDCOSTQTY";
            public const string PACKING_NO = "PACKING_NO";
            public const string PACKING_NO1 = "PACKING_NO1";
            public const string PACKING_NO2 = "PACKING_NO2";
            public const string PACKING_NO3 = "PACKING_NO3";
            public const string PACKING_NO4 = "PACKING_NO4";
            public const string PACKING_NO5 = "PACKING_NO5";
            public const string USE_QTY = "USE_QTY";
            public const string USE_QTY1 = "USE_QTY1";
            public const string USE_QTY2 = "USE_QTY2";
            public const string USE_QTY3 = "USE_QTY3";
            public const string USE_QTY4 = "USE_QTY4";
            public const string USE_QTY5 = "USE_QTY5";
            public const string MATERIAL_MAKER = "MATNRMAKER";
            public const string MATERIAL_MAKER_ID = "MATMAKERID";
            public const string MATERIAL_MAKER_NAME = "MATNRMAKERNAME";
            public const string ZLOTNO = "ZLOTNO";
            public const string MATERIALDEFINITIONID = "MATERIALDEFINITIONID";
            public const string SPL_ORDER_EXPIRED_YN = "SPL_ORDER_EXPIRED_YN";
            public const string COL_SITEID = "SITEID";
            public const string MATERIAL_TYPE = "MATERIALTYPE";
            public const string MATERIAL_TYPE_NAME = "MATERIALTYPE_NAME";
            public const string WT_DAY = "WT_DAY";
            public const string WT_DAY_ADDYN = "WT_DAY_ADDYN";
            public const string COL_CREATOR = "CREATOR";
            public const string COL_CREATE_TIME = "CREATETIME";
            public const string COL_MODIFIER = "MODIFIER";
            public const string COL_MODIFIER2 = "MODIFIER2";
            public const string COL_MODIFIERNAME3 = "MODIFIERNAME3";
            public const string COL_MODIFY_TIME = "MODIFYTIME";
            public const string COL_MODIFY_TIME2 = "MODIFYTIME2";
            public const string COL_MODIFY_TIME3 = "MODIFYTIME3";
            public const string CIM_MATERIALLOT_ADDYN = "CIM_MATERIALLOT_ADDYN";
            public const string CIM_MATERIALLOT_ZLOTNO_REAL = "CIM_MATERIALLOT_ZLOTNO_REAL";
            public const string ZTO_MESDATE = "ZTO_MESDATE";
            public const string ZTO_MESDATE_SAPIF_YN = "ZTO_MESDATE_SAPIF_YN";
            public const string COL_MATNR = "MATNR";
            public const string COL_ZPACKING = "ZPACKING";
            public const string COL_ERFME = "ERFME";
            public const string PSR_BARCODE = "PSR_BARCODE";
            public const string PSR_BARCODE1 = "PSR_BARCODE1";
            public const string PSR_BARCODE2 = "PSR_BARCODE2";
            public const string PSR_BARCODE3 = "PSR_BARCODE3";
            public const string PSR_BARCODE4 = "PSR_BARCODE4";
            public const string PSR_BARCODE5 = "PSR_BARCODE5";
            public const string COL_BARCODE = "BARCODE";
            public const string BARCODE_SEQ = "BARCODE_SEQ";
            public const string AGITATION_TIME = "AGITATION_TIME";
            public const string AVAILABLE_TIME = "AVAILABLE_TIME";
            public const string VISCOSITY = "VISCOSITY";
            public const string INK_TYPE = "INK_TYPE";
            public const string INK_TYPE_NAME = "INK_TYPE_NAME";
            public const string PRINT_TIME = "PRINT_TIME";
            public const string PRINT_USERID = "PRINT_USERID";
            public const string RESULT = "RESULT";
            public const string MODIFIED_YN = "MODIFIED_YN";
            public const string COM_INSERT = "INSERT";
            public const string COM_UPDATE = "UPDATE";
            public const string COM_DELETE = "DELETE";
        }

        public class MATERIAL_GROUP
        {
            public const string R1A = "R1A";
            public const string R1B = "R1B";
            public const string R2BA = "R2BA";
            public const string R1D = "R1D";
            public const string R2BD = "R2BD";
            public const string R1BA = "R1BA";
            public const string R2C = "R2C";
            public const string R2BF = "R2BF";
        }

        public class CP_INFORMATION
        {
            public class Execution
            {
                public const string INSERT = "INSERT";
                public const string UPDATE = "UPDATE";
                public const string DELETE = "DELETE";
                public const string EXCEL = "EXCEL";
            }

            public class State
            {
                public const string TEMP_SAVE = "T";
                public const string APPROVAL_WAIT = "N";
                public const string APPROVAL_PROCESSING = "P";
                public const string APPROVAL_REJECT = "D";
                public const string APPROVAL_COMPLETED = "X";
            }

            public class ProcessState
            {
                public const string CONFIRM = "CONFIRM";
                public const string REFORM = "REFORM";
                public const string DELETE = "DELETE";
                public const string WAIT = "WAIT";
                public const string DELETE_WAIT = "DELETE_WAIT";
                public const string PROCESSING = "PROCESSING";
                public const string DELETE_PROCESSING = "DELETE_PROCESSING";
                public const string REJECT = "REJECT";
                public const string DELETE_REJECT = "DELETE_REJECT";
            }
        }
    }
}


