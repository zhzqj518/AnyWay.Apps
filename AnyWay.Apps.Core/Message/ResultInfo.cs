
using System.Collections.Generic;
namespace AnyWay.Apps.Core.Message
{
    public class ResultInfo
    {
        public bool isSuccess { get; set; }

        public EnumErrorType enumErrorType { get; set; }

        public List<string> ErrorMsgList { get; set; }

        public string DisplayErrorMsg { get; set; }
    }

    public enum EnumErrorType
    {
        None,
        Exception,
        DBSave
    }
}