using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckShort_YC4.DAL;
using CheckShort_YC4.Sigleton;

namespace CheckShort_YC4.Business
{
    public class UmesHelper
    {
        private static string _MsgErr;
        private static bool _Check_Sample;
        private static PVSWebServiceReference.BARCODE_RULE_ITEMSEntity _RuleItem = new PVSWebServiceReference.BARCODE_RULE_ITEMSEntity();
        private static string _PO;
        private static string _RankledContent = string.Empty;
        private static PVSWebServiceReference.WORK_ORDERSEntity _WO = new PVSWebServiceReference.WORK_ORDERSEntity();
        private static PVSWebServiceReference.Base_ModelsEntity _Product;
        public static string MsgErr
        {
            get
            {
                return _MsgErr;
            }
        }
        public static PVSWebServiceReference.BARCODE_RULE_ITEMSEntity RuleItem
        {
            get
            {
                return _RuleItem;
            }
        }
        public static string PO
        {
            get
            {
                return _PO;
            }
        }
        public static bool CheckSample
        {
            get
            {
                return _Check_Sample;
            }
        }
        public static PVSWebServiceReference.Base_ModelsEntity Product
        {
            get
            {
                return _Product;
            }
        }
        public static string RankledContent
        {
            get
            {
                return _RankledContent;
            }
        }
        public static PVSWebServiceReference.WORK_ORDERSEntity WO
        {
            get
            {
                return _WO;
            }
        }
        public static void FindOrderno(string orderNo)
        {
            _MsgErr = string.Empty;
            _PO = string.Empty;
            _WO = SingletonHelper.PVSInstance.GetWorkOrdersByOrderNo(orderNo);
            if (_WO == null)
            {
                //ShowMessage("FAIL", "NG", $"OrderNo [{orderNo}] không tồn tại.\nVui lòng kiểm tra lại !");
                _MsgErr = $"OrderNo [{orderNo}] không tồn tại.\nVui lòng kiểm tra lại !";
                return;
            }
            string ruleNo = SingletonHelper.PVSInstance.GetWorkOrderProcedureByOrderId(_WO.ID.ToString()).FirstOrDefault(r => r.INDEX == 1).RULE_NO;
            _RuleItem = SingletonHelper.PVSInstance.GetBarodeRuleItemsByRuleNo(ruleNo);
            if (_RuleItem == null)
            {
                //ShowMessage("FAIL", "NG", $"Rule [{ruleNo}] chưa được thiết lập trên WIP trong phần Barcode Rule Items.\nVui lòng thiết lập sau đó hãy thử lại!");
                _MsgErr = $"Rule [{ruleNo}] chưa được thiết lập trên WIP trong phần Barcode Rule Items.\nVui lòng thiết lập sau đó hãy thử lại!";
                return;
            }
            if (_WO.CUSTOMER_ID.IsYaskawa())
            {
                var labelInfo = SingletonHelper.YasInstance.GetEntityYaskawaLabelDetailByOrderNo(orderNo);
                if (labelInfo == null)
                {
                    _MsgErr = $"PO chưa được thiết lập.\nVui lòng thiết lập sau đó hãy thử lại!";
                    return;
                }
                _PO = labelInfo.CustPO;
            }
            _Product = SingletonHelper.PVSInstance.GetModelInfo(_WO.PRODUCT_ID);
            if (_Product == null)
            {
                //  _MsgErr = $"Liên hệ IT(3143) để thiết lập Model!";
                SingletonHelper.PVSInstance.SaveModelInfo(new PVSWebServiceReference.Base_ModelsEntity()
                {
                    Product_Id = _WO.PRODUCT_ID,
                    Pcb = 1,
                    Check_First = false,
                    Component_Id = _WO.PRODUCT_ID,
                    Customer = _WO.CUSTOMER_ID,
                    Pcb_Label = 1,
                    Content = "",
                    Is_SubBoard = false,
                    Is_Hexa = false,
                    Group_Id = "",
                    Is_Wip = true,
                    Is_Aql = false,

                }, "");
                // return;
            }
            _RankledContent = RankLedHelper.FindRankContent(orderNo);
        }
        public static bool WoFinish
        {
            get
            {
                return _WO.IS_FINISHED || _WO.INITIATED_QUANTITY == WO.QUANTITY;
            }
        }
    }
}
