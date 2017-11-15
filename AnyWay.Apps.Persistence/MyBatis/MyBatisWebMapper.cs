using AnyWay.Apps.Core.Persistence;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using System;
using System.Configuration;

namespace AnyWay.Apps.Persistence.MyBatis
{
    /// <summary>
    /// 单例模式：初始化ISqlMapper
    /// </summary>
    public class MyBatisWebMapper : DataMapper
    {
        private static volatile ISqlMapper _mapper = null;
        private static object mapperLock = new object();

        protected static void Configure(object obj)
        {
            _mapper = null;
        }

        protected static void InitMapper()
        {
            ConfigureHandler handler = new ConfigureHandler(Configure);
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            string strDbType = ConfigurationManager.AppSettings["DbType"];
            string strSqlMapper = ConfigurationManager.AppSettings["MyBatisCfg"];
            _mapper = builder.ConfigureAndWatch(strSqlMapper, handler);
        }

        public static ISqlMapper Instance()
        {
            if (_mapper == null)
            {
                lock (mapperLock)
                {
                    if (_mapper == null)
                    {
                        InitMapper();
                    }
                }
            }
            return _mapper;
        }
    }
}
