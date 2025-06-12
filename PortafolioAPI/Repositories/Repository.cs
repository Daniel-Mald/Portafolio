using PortafolioAPI.Models.Entities;
using PortafolioAPI.Repositories.Interfaces;

namespace PortafolioAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly LabsystePortafolioContext Context;
        public Repository(LabsystePortafolioContext context)
        {
            Context = context;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public virtual T? Get(object id)
        {
            return Context.Find<T>(id);
        }
        public virtual void Insert(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }
        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }
        public virtual void Delete(object id)
        {
            var entity = Get(id);

            if (entity != null)
            {
                Delete(entity);
            }


        }
        public virtual int Update(T entity)
        {
            Context.Update(entity);
            return Context.SaveChanges();
        }
    }
}
