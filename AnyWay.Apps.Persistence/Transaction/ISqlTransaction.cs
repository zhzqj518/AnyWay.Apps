using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyWay.Apps.Persistence.Transaction
{
    /// <summary>
    /// 数据库事物
    /// </summary>
    public interface ISqlTransaction
    {
        /// <summary>
        /// 开始事物
        /// </summary>
        /// <returns></returns>
        bool BeginTransaction();

        /// <summary>
        /// 提交事物
        /// </summary>
        /// <returns></returns>
        bool CommitTransaction();

        /// <summary>
        /// 回滚事物
        /// </summary>
        /// <returns></returns>
        bool RollBackTransaction();
    }
}
