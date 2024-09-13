using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrelloManagmentSystem.Data;
using TrelloManagmentSystem.Models;

namespace TrelloManagmentSystem.Repositories.GenericRepositories
{
    public class GenericRepositories<T>: IGenericRepositories<T> where T : BaseModel
    {
        private readonly Context context;

        public GenericRepositories(Context context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAll()=> context.Set<T>().Where(x => !x.IsDeleted);

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
            => GetAll().Where(predicate);

        public IQueryable<TResult> Get<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
            =>Get(predicate).Select(selector);

        public T? GetyId(int id)
            => GetAll().FirstOrDefault(x => x.Id == id);

        public T? GetByIdWithTracking(int id)
            =>context.Set<T>().AsTracking().FirstOrDefault(x => !x.IsDeleted && x.Id == id);


        public void HardDelete(int id)
            => context.Set<T>().Where(x => x.Id == id).ExecuteDelete();
        
        public void SaveChanges()
            => context.SaveChanges();


        public void Insert(T entity)
            => context.Set<T>().Add(entity);

        public void Update(T entity)
         => context.Set<T>().Update(entity);

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }
    }
}
