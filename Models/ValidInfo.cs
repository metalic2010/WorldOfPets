using System.Net;
using System.Web;

namespace WorldOfPets.Models
{
    public class ValidInfo
    {
        public static bool boolIsIP (string ip)
        {
            try
            {
                if (ip.Contains('.'))
                {
                    IPAddress iP = IPAddress.Parse(ip);
                    return true;
                }
                else { return false; }
            }
            catch { return false; }
        }

        /// <summary>
        /// Функция принимает на входе просто строку и кодирует её в формат URL
        /// </summary>
        /// <param name="str">Стркоа для кодирования в url формат</param>
        /// <returns>Возвращает закодированную строку</returns>
        public static string CodeURL (string str)
        {
            string StrURL = string.Empty;
            StrURL = HttpUtility.UrlEncode(str);
            return StrURL;
        }
    }
}
