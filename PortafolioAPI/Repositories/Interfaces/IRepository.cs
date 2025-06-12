namespace PortafolioAPI.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Delete(object id);
        void Delete(T entity);
        T? Get(object id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        int Update(T entity);
    }
}