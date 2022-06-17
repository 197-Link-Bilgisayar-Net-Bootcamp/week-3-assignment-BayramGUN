using Microsoft.EntityFrameworkCore;

namespace NLayer.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task Delete(int id);
    }
}