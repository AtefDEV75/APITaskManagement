
using System.Linq.Expressions;
using DataAccessLayer.DataContext;
using DataAccessLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Repositories
{
    public class TaskRepository<TModel> : ITaskRepository<TModel> where TModel : class
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(TModel model)
        {
            _context.Set<TModel>().Add(model);
            _context.SaveChanges();
        }


        public void Update(TModel model)
        {
            _context.Set<TModel>().Update(model);
            _context.SaveChanges();
        }

        public void Delete(TModel model)
        {
            _context.Set<TModel>().Remove(model);
            _context.SaveChanges();
        }

        public IQueryable<TModel> FindAll()
        {
            return _context.Set<TModel>().AsNoTracking();
        }

        public IQueryable<TModel> FindByCondition(Expression<Func<TModel,bool>> expression)
        {
            return _context.Set<TModel>().Where(expression).AsNoTracking();
        }
    }
}
