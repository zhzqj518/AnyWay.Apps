using AnyWay.Apps.Core.Model;
using AnyWay.Apps.Core.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyWay.Apps.Core.Repository
{
    public interface IRepository<T> : IDisposable where T : BaseModel
    {
        IList<T> GetList(ModelQuery modelQuery);

        int GetListCnt(ModelQuery modelQuery);

        T GetById(T entity);

        bool Insert(T entity);

        bool Update(T entity);

        bool Delete(T entity);
    }
}
