using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using IBatisNet.DataMapper;
using AnyWay.Apps.Persistence.MyBatis;

namespace AnyWay.Apps.Persistence.Transaction
{
    public class SqlTransactionFactory
    {
        public static ISqlTransaction GetInstance(SqlTransactionType sqlTranType)
        {
            switch (sqlTranType)
            {
                case SqlTransactionType.DB:
                    {
                        ISqlMapper _dataMapper = MyBatisWebMapper.Instance();
                        return new MyBatisSqlTransaction(_dataMapper);
                    }
                default:
                    {
                        throw new Exception(string.Format("不支持该<{0}>类型事物！", sqlTranType.ToString()));
                    }
            }
        }
    }

    public enum SqlTransactionType
    {
        DB
    }
}
