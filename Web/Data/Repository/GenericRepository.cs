using Microsoft.EntityFrameworkCore;
using Web.Models.Interface;

namespace Web.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        private readonly DataContext _context;
        private DbSet<T> table = null;

        public GenericRepository(DataContext context) 
        {
            _context = context;
            table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            T exesting = GetById(id);
            table.Remove(exesting);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Update(T entity)
        {
            table.Update(entity);
        }
    }
}
