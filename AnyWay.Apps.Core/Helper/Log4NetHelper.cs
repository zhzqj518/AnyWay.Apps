using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyWay.Apps.Core.Helper
{
    public class Log4NetHelper
    {
        public static void SystemExceptionLog(string strFormat, params object[] param)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger("SystemException");
                string strMsg = string.Format(strFormat, param);
                log.Info(strMsg);
            }
            catch (Exception ex)
            {
            }
        }

        public static void SystemInfoLog(string strFormat, params object[] param)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger("SystemInfo");
                string strMsg = string.Format(strFormat, param);
                log.Info(strMsg);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
