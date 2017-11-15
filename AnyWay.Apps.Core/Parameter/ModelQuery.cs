using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyWay.Apps.Core.Parameter
{
    public class ModelQuery
    {
        public int PageIndex { set; get; }

        public int PageSize { set; get; }

        public Object QueryParam { set; get; }
    }
}
