using System.Linq.Expressions;

namespace EcommerceApp.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null, 
            string? includeProperties = null,
            int pageNumber =1, // Default to page 1 if not provided
            int pageSize = 10  // Default to 10 items per page if not provided
            );
        T Get(Expression<Func<T, bool>> filter, 
            string? includeProperties = null, 
            bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
