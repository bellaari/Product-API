using Web.Models.Interface;

namespace Web.Models.Interface
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepository<T> Entity {get;}
        void Save();
    }
}
