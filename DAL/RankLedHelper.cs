using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CANON_Initialize_Boards.Sigleton;

namespace CANON_Initialize_Boards.DAL
{
    public class RankLedHelper
    {
        public static bool CheckRankLed(string rankControl, string boardNo)
        {
            var flag = true;
            try
            {
                if (!string.IsNullOrEmpty(rankControl))
                {
                    var col = rankControl.Split('|');
                    var content = boardNo.Substring(Convert.ToInt32(col[1]) - 1, Convert.ToInt32(col[2]));
                    flag = col[0] == content;
                }
            }
            catch
            {

            }
            return flag;
        }

        public static string GetRank(string orderNo)
        {
            string value = "";
            using (RankLedConn context = new RankLedConn())
            {
                var exist = context.Tbl_RankControl.FirstOrDefault(r => r.WOSMTA == orderNo || r.WOSMTB == orderNo);
                if (exist != null)
                {
                    value = exist.WH_LazerStatus;
                }
            }
            return value;
        }
        public static string FindRankContent(string orderNo)
        {
            return SingletonHelper.ERPInstance.FindRankContent(orderNo);
        }
    }
}
