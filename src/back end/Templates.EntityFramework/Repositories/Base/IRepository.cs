using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.EntityFrameworkCore.Repositories
{
    public interface IRepository<TEntity, TKey> 
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Count
        /// <summary>
        /// 获取所有实体的数量
        /// </summary>
        /// <returns>实体数量</returns>
        int Count();

        /// <summary>
        /// 获取满足条件的实体的数量
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>实体数量</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取所有实体的数量
        /// </summary>
        /// <returns>实体数量</returns>
        Task<int> CountAsync();

        /// <summary>
        /// 获取满足条件的实体的数量
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>实体数量</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取所有实体的数量
        /// </summary>
        /// <returns>实体数量</returns>
        long LongCount();

        /// <summary>
        /// 获取满足条件的实体的数量
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>实体数量</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取所有实体的数量
        /// </summary>
        /// <returns>实体数量</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// 获取满足条件的实体的数量
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>实体数量</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Get
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        TEntity Get(TKey id);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>实体集合</returns>
        IQueryable<TEntity> Get();

        /// <summary>
        /// 获取满足条件的实体
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>实体集合</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        Task<TEntity> GetAsync(TKey id);
        #endregion

        #region Insert
        /// <summary>
        /// 插入新实体
        /// </summary>
        /// <param name="entity">要插入的实体</param>
        /// <returns>实体</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 插入新实体集合
        /// </summary>
        /// <param name="entities">要插入的实体集合</param>
        /// <returns>插入的实体个数</returns>
        int Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 插入新实体
        /// </summary>
        /// <param name="entity">要插入的实体</param>
        /// <returns>实体</returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 插入新实体集合
        /// </summary>
        /// <param name="entities">要插入的实体集合</param>
        /// <returns>插入的实体个数</returns>
        Task<int> InsertAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Update
        /// <summary>
        /// 更新已存在的实体
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        /// <returns>实体</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新已存在的实体集合
        /// </summary>
        /// <param name="entities">要更新的实体集合</param>
        /// <returns>更新的实体个数</returns>
        int Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新已存在的实体
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        /// <returns>实体</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新已存在的实体集合
        /// </summary>
        /// <param name="entities">要更新的实体集合</param>
        /// <returns>更新的实体个数</returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Delete
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns>删除的实体个数</returns>
        int Delete(TEntity entity);

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="id">要删除的实体主键</param>
        /// <returns>删除的实体个数</returns>
        int Delete(TKey id);

        /// <summary>
        /// 删除实体集合
        /// </summary>
        /// <param name="entities">要删除的实体集合</param>
        /// <returns>删除的实体个数</returns>
        int Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 根据条件删除实体集合
        /// 如果条件覆盖实体数量太多，可能会导致性能问题
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>删除的实体个数</returns>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns>删除的实体个数</returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="id">要删除的实体主键</param>
        /// <returns>删除的实体个数</returns>
        Task<int> DeleteAsync(TKey id);

        /// <summary>
        /// 删除实体集合
        /// </summary>
        /// <param name="entities">要删除的实体集合</param>
        /// <returns>删除的实体个数</returns>
        Task<int> DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 根据条件删除实体集合
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>删除的实体个数</returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate); 
        #endregion
    }

}
