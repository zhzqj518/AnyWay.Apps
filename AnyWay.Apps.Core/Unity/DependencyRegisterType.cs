using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Unity;

namespace AnyWay.Apps.Core.Unity
{
    public class DependencyRegisterType
    {
        //系统注入
        public static void Container(ref UnityContainer container,string strConfigFile)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = strConfigFile };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            container.LoadConfiguration(unitySection);

            //
            //container.RegisterType<IUserRespository, UserRespository>();

            //container.RegisterType<ISysLogBizManager, SysLogBizManager>();
            //container.RegisterType<ISysLogRespository, SysLogRespository>();
        }
    }
}