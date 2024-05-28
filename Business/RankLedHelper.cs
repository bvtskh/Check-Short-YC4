using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckShort_YC4.Business;
using CheckShort_YC4.Entities;
using CheckShort_YC4.Sigleton;

namespace CheckShort_YC4.DAL
{
    public class RankLedHelper
    {
        public static bool CheckRankLed(string boardNo)
        {
            var flag = true;
            try
            {
                if (!string.IsNullOrEmpty(UmesHelper.RankledContent))
                {
                    var col = UmesHelper.RankledContent.Split('|');
                    var content = boardNo.Substring(Convert.ToInt32(col[1]) - 1, Convert.ToInt32(col[2]));
                    flag = col[0] == content;
                }
            }
            catch
            {

            }
            return flag;
        }

        public static string FindRankContent(string orderNo)
        {
            try
            {
                return SingletonHelper.ERPInstance.FindRankContent(orderNo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Err";
            }      
        }
    }
}
