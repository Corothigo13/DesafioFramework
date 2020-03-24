using Models.AbsModels;
using Models.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IBaseService<TEntity> : IDisposable where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);

        TEntity GetById(int? Id);

        void Add(TEntity obj);

        void Update(TEntity obj);

        void Delete(int Id);

        int SaveChanges();

    }
}
