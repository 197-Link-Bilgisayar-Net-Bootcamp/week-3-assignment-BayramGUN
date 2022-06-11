using Microsoft.EntityFrameworkCore;

namespace NLayer.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task Add(T entity);
    }
}