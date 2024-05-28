using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckShort_YC4.Business;

namespace CheckShort_YC4.Sigleton
{
    public class SingletonHelper
    {
        private static volatile PVSWebServiceReference.PVSWebServiceSoapClient _pvs_service = null;
        private static volatile ERPWebService.ERPWebServiceSoapClient _erp_service = null;
        private static volatile YaskawaService.YaskawaWebServiceSoapClient _yaskawa_service = null;
        private static readonly object sync = new object();
        private SingletonHelper()
        {

        }
        public static PVSWebServiceReference.PVSWebServiceSoapClient PVSInstance
        {
            get
            {
                if (_pvs_service == null)
                {
                    lock (sync)
                    {
                        if (_pvs_service == null)
                        {
                            _pvs_service = new PVSWebServiceReference.PVSWebServiceSoapClient();
                        }
                    }
                }
                return _pvs_service;
            }
        }
        public static ERPWebService.ERPWebServiceSoapClient ERPInstance
        {
            get
            {
                if (_erp_service == null)
                {
                    lock (sync)
                    {
                        if (_erp_service == null)
                        {
                            _erp_service = new ERPWebService.ERPWebServiceSoapClient();
                        }
                    }
                }
                return _erp_service;
            }
        }
        public static YaskawaService.YaskawaWebServiceSoapClient YasInstance
        {
            get
            {
                if (_yaskawa_service == null)
                {
                    lock (sync)
                    {
                        if (_yaskawa_service == null)
                        {
                            _yaskawa_service = new YaskawaService.YaskawaWebServiceSoapClient();
                        }
                    }
                }
                return _yaskawa_service;
            }
        }
        public static string GetProduct(string orderNo)
        {
            string productId = string.Empty;
            var order = SingletonHelper.PVSInstance.GetWorkOrdersByOrderNo(orderNo);
            if (order != null)
            {
                productId = order.PRODUCT_ID;
            }
            return productId;
        }
        public static List<string> GetListBoardNo(string boardNo)
        {
            List<string> listBoard = new List<string>();
            //var productId = SingletonHelper.GetProduct(orderNo);
            //var modelInfo = SingletonHelper.PVSInstance.GetModelInfo(productId);

            var boardHeader = boardNo.Left((int)UmesHelper.Product.Content_Index);
            var start = Convert.ToInt32(boardNo.Substring((int)UmesHelper.Product.Content_Index, (int)UmesHelper.Product.Content_Length));
            listBoard.Add(boardNo);
            if ("USB" == UmesHelper.Product.Group_Id)
            {
                for (int i = 1; i < UmesHelper.Product.Pcb; i++)
                {
                    var boardMid = (i + start).ToString($"D{(int)UmesHelper.Product.Content_Length}");
                    var right = boardNo.Length - boardHeader.Length - boardMid.Length;
                    var boarZen = right == 0
                        ? $"{boardHeader}{boardMid}"
                        : $"{boardHeader}{boardMid}{boardNo.Right(right)}";
                    listBoard.Add(boarZen);
                }
            }
            return listBoard;
        }
        public static string GetLaserBoard(string orderNo)
        {
            var boardStart = SingletonHelper.ERPInstance.LaserGetBoard(orderNo);
            //if (string.IsNullOrEmpty(boardStart))
            //{
            //    var order = SingletonHelper.PVSInstance.GetWorkOrdersByOrderNo(orderNo);
            //    boardStart = SingletonHelper.PVSInstance.GetFirstBoard(order.ID.ToString()).BOARD_NO;
            //}
            return boardStart;
        }
        public static void LaserSave(ERPWebService.LaserInitEntity entity)
        {
            var boardExist = SingletonHelper.ERPInstance.LaserGetBoard(entity.ORDER_NO);
            var key = string.IsNullOrEmpty(boardExist) ? "" : entity.ORDER_NO;
            SingletonHelper.ERPInstance.LaserSave(key, entity);
        }
        public static string GetLaser(string orderNo)
        {
            var boardExist = SingletonHelper.ERPInstance.LaserGetBoard(orderNo);
            return boardExist != null ? boardExist : "";
        }
        public static void SaveTestLog(ERPWebService.TestLogEntity entity)
        {
            SingletonHelper.ERPInstance.SaveTestLog(entity);
        }
    }
}
