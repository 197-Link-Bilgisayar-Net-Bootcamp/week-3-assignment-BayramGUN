using Microsoft.EntityFrameworkCore;
using NLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public virtual async Task Delete(int id)
        {
            T? entity = await _context.Set<T>().FindAsync(id);
            if(entity is null)
                return;       
            _context.Remove(entity);
        }
    }
}
