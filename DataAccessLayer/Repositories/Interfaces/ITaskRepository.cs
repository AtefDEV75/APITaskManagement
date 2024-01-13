
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.Contracts
{
    public interface ITaskRepository<TModel> where TModel : class
    {
        IQueryable<TModel> FindAll();
        IQueryable<TModel> FindByCondition(Expression<Func<TModel, bool>> expression);
        void Create(TModel model);
        void Update(TModel model);
        void Delete(TModel model);

    }
}
