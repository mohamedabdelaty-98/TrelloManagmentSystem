using System.Linq.Expressions;
using TrelloManagmentSystem.Models;

namespace TrelloManagmentSystem.Repositories.GenericRepositories
{
    public interface IGenericRepositories<T> where T : BaseModel
    {
        public IQueryable<T> GetAll();
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        public IQueryable<TResult> Get<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T,TResult>> selector);

        T? GetyId(int id);
        T? GetByIdWithTracking(int id);

        void Update(T entity);
        void Insert (T entity);

        void Delete (T entity);
        void HardDelete(int id);
        void SaveChanges();

    }
}
