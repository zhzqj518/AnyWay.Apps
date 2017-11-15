using AnyWay.Apps.Core.Model;
using AnyWay.Apps.Core.Parameter;
using AnyWay.Apps.Core.Repository;
using AnyWay.Apps.Persistence.MyBatis;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyWay.Apps.Persistence.MyBatis
{
    public class RepositoryMyBatis<T> : IRepository<T>, IDisposable where T : BaseModel
    {
        private ISqlMapper _dataMapper = null;

        private RepositoryMyBatis() { }

        public RepositoryMyBatis(ISqlMapper _params)
        {
            this._dataMapper = _params;
        }

        public IList<T> GetList(ModelQuery modelQuery)
        {
            String stmtid = string.Concat(typeof(T).Name, ".", "GetList");
            if (modelQuery.PageSize == 0)
            {
                return _dataMapper.QueryForList<T>(stmtid, modelQuery.QueryParam);
            }
            else
            {
                return _dataMapper.QueryForList<T>(stmtid, modelQuery.QueryParam, modelQuery.PageIndex * modelQuery.PageSize, modelQuery.PageSize);
            }
        }

        public int GetListCnt(ModelQuery modelQuery)
        {
            String stmtid = string.Concat(typeof(T).Name, ".", "GetListCnt");
            return _dataMapper.QueryForObject<int>(stmtid, modelQuery.QueryParam);
        }

        public T GetById(T entity)
        {
            String stmtid = string.Concat(typeof(T).Name, ".", "GetById");
            return _dataMapper.QueryForObject<T>(stmtid, entity);
        }

        public bool Insert(T entity)
        {
            String stmtid = string.Concat(typeof(T).Name, ".", "Insert");
            _dataMapper.Insert(stmtid, entity);
            return true;
        }

        public bool Update(T entity)
        {
            String stmtid = string.Concat(typeof(T).Name, ".", "Update");
            _dataMapper.Update(stmtid, entity);
            return true;
        }

        public bool Delete(T entity)
        {
            String stmtid = string.Concat(typeof(T).Name, ".", "Delete");
            _dataMapper.Delete(stmtid, entity);
            return true;
        }

        public virtual void Dispose()
        {

        }
    }
}
