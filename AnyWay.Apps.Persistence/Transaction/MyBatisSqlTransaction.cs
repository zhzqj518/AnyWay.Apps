using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyWay.Apps.Persistence.Transaction
{
    public class MyBatisSqlTransaction : ISqlTransaction
    {
        private ISqlMapper _sqlMapper = null;

        private MyBatisSqlTransaction() { }

        public MyBatisSqlTransaction(ISqlMapper sqlMapper)
        {
            this._sqlMapper = sqlMapper;
        }

        public bool BeginTransaction()
        {
            try
            {
                _sqlMapper.BeginTransaction();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CommitTransaction()
        {
            try
            {
                _sqlMapper.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RollBackTransaction()
        {
            try
            {
                _sqlMapper.RollBackTransaction();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
