using DataAccess;
using Interface;
using Microsoft.EntityFrameworkCore;
using Models.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : Entity
    {
        protected ApplicationDBContext DBContext;
        protected DbSet<TEntity> DbEntity;

        protected BaseService(ApplicationDBContext context)
        {
            DBContext = context;
            DbEntity = DBContext.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> GetAll()
        {
            return DbEntity.AsNoTracking().AsQueryable();
        }   

        public virtual IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return DbEntity.AsNoTracking().Where(filter);
        }

        public virtual TEntity GetById(int? Id)
        {
            return DbEntity.AsNoTracking().FirstOrDefault(t => t.Id == Id);
        }

        public virtual void Add(TEntity obj)
        {
            DbEntity.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            DbEntity.Update(obj);
        }

        public void Delete(int Id)
        {
            DbEntity.Remove(DbEntity.Find(Id));
        }

        public int SaveChanges()
        {
            return DBContext.SaveChanges();
        }

        public void Dispose()
        {
            DBContext.Dispose();
        }
    }
}
