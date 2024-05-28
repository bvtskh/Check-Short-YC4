using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckShort_YC4.Entities;
using CheckShort_YC4.Sigleton;

namespace CheckShort_YC4.Business
{
    public class Validator
    {
        private static string CheckNull(string boardNo)
        {
            string msg = "";
            if (string.IsNullOrEmpty(boardNo))
            {
                msg = "Barcode không được để trống!";

            }
            return msg;
        }
        public static IEnumerable<string> SubBoardErr(string boardNo)
        {
            var order = SingletonHelper.PVSInstance.GetOrderItem(UmesHelper.WO.ORDER_NO);
            var model = SingletonHelper.PVSInstance.GetModelInfo(order.PRODUCT_ID);
            var index = Convert.ToInt32(boardNo.Substring((int)model.Content_Index, (int)model.Content_Length));
            if (index % (int)model.Pcb != 1)
            {
                yield return "Barcode phụ không đúng thứ tự";
            }
            yield break;
        }
        public static string CheckFirstBoard(string boardNo)
        {
            string msg = "";
            var OrderEntity = SingletonHelper.PVSInstance.GetWorkOrdersByOrderNo(UmesHelper.WO.ORDER_NO);
            var model = SingletonHelper.PVSInstance.GetModelInfo(OrderEntity.PRODUCT_ID);
            var index = Convert.ToInt32(boardNo.Substring((int)model.Content_Index, (int)model.Content_Length));
            int pcb = (int)model.Pcb;
            if (index % pcb != 1 && pcb != 1)
            {
                msg = "Barcode không đúng thứ tự";
            }

            if (Runtime.Board.IsSubBoard)
            {
                if (Runtime.Board.SubBoard != boardNo)
                {
                    msg = "Barcode khác với Barcode phụ";
                }
            }
            return msg;
        }
        public static string CheckPO(string boardNo)
        {
            string msg = "";
            if (UmesHelper.WO.CUSTOMER_ID.IsYaskawa())
            {
                var PoInBarcode = boardNo.Left(10);
                string cusPO = Ultils.MakePO(UmesHelper.PO);
                if (PoInBarcode != cusPO)
                {
                    msg = $"Board [{boardNo}] không thuộc PO [{UmesHelper.PO}]";
                }
            }
            return msg;
        }
        public static IEnumerable<string> GetError(string boardNo, string rev)
        {
            if (string.IsNullOrEmpty(boardNo))
            {
                yield return "Barcode không được để trống!";
            }
            if (boardNo.Length != UmesHelper.RuleItem.LENGTH)
            {
                yield return $"Độ dài của barcode [{boardNo}] không đúng với cài đặt [{UmesHelper.RuleItem.LENGTH}] ký tự, vui lòng kiểm tra và bắn lại!";
            }
            if (!boardNo.Contains(UmesHelper.RuleItem.CONTENT))
            {
                yield return $"Barcode [{boardNo}] không đúng với Model đang chọn. Barcode phải bắt đầu với ký tự [{UmesHelper.RuleItem.CONTENT}], vui lòng kiểm tra và thử lại!";
            }
            var orderItem = SingletonHelper.PVSInstance.GetWorkOrderItemByBoardNo(boardNo);
            if (orderItem != null)
            {
                yield return $"Barcode [{boardNo}] đã được khởi tạo\n{orderItem.INITIATE_TIME} !";
            }
            var replace = SingletonHelper.PVSInstance.GetReplacingLog(boardNo);
            if (replace != null)
            {
                yield return "Barcode đã được khởi tạo và thay thế !";
            }
            if (UmesHelper.WO.CUSTOMER_ID.IsYaskawa())
            {
                var PoInBarcode = boardNo.Left(10);
                string cusPO = Ultils.MakePO(UmesHelper.PO);
                if (PoInBarcode != cusPO)
                {
                    yield return $"Board [{boardNo}] không thuộc PO [{UmesHelper.PO}]";
                }
            }
            if (!string.IsNullOrEmpty(rev))
            {
                if (UmesHelper.Product.Is_Indentity == true)
                {
                    if (boardNo.Left((int)UmesHelper.Product.Content_Index) != rev.Left((int)UmesHelper.Product.Content_Index))
                    {
                        yield return $"Board phải bắt đầu [{rev.Left((int)UmesHelper.Product.Content_Index)}]";
                    }
                }
            }
            yield break;
        }
        public static IEnumerable<string> GetErrorSubMain(string boardNo)
        {
            string msg = "";
            msg = CheckNull(boardNo);
            if (!string.IsNullOrEmpty(msg)) yield return msg;
            var orderItem = SingletonHelper.PVSInstance.GetWorkOrderItemByBoardNo(boardNo);
            if (orderItem == null)
            {
                msg = $"Barcode {boardNo} không tồn tại trên Wip";
                yield return msg;
            }
            if (orderItem.ORDER_NO != UmesHelper.WO.ORDER_NO)
            {
                msg = $"Barcode {boardNo} không thuộc Order: {orderItem.ORDER_NO}";
                yield return msg;
            }
            //var order = SingletonHelper.PVSInstance.GetWorkOrdersByOrderNo(UmesHelper.WO.ORDER_NO);
            // var model = SingletonHelper.PVSInstance.GetModelInfo(order.PRODUCT_ID);
            var index = Convert.ToInt32(boardNo.Substring((int)UmesHelper.Product.Content_Index, (int)UmesHelper.Product.Content_Length));
            if (index % (int)UmesHelper.Product.Pcb != 1)
            {
                msg = "Barcode không đúng thứ tự";
                yield return msg;
            }
            var boardStart = SingletonHelper.GetLaserBoard(UmesHelper.WO.ORDER_NO);
            if (!string.IsNullOrEmpty(boardStart))
            {
                var indexboardStart = Convert.ToInt32(boardStart.Substring((int)UmesHelper.Product.Content_Index, (int)UmesHelper.Product.Content_Length));
                indexboardStart += (int)UmesHelper.Product.Pcb;
                if (indexboardStart != index)
                {
                    msg = "Barcode không đúng thứ tự";
                    yield return msg;
                }
            }
            yield break;
        }
        public static bool MapModel9D3(Generic.GenericList<string> lst)
        {
            if (lst[0].Length != lst[1].Length)
            {
                return false;
            }
            if (lst[0].Left(12) != lst[1].Left(12))
            {
                return false;
            }
            // return lst[0].Left(12) == lst[1].Left(12);
            return lst[0].Right(10) == lst[1].Right(10);
        }
    }
}
