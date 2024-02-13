using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MovieBooking.Data.BaseRepository
{
    public class EntityRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {

        private readonly AppDbContext _context;

        public EntityRepository(AppDbContext context){
            _context=context;
        }
        public async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);

           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)

        {   var entity=await _context.Set<T>().FirstOrDefaultAsync(n=>n.Id==id);
            EntityEntry entityEntry=_context.Entry<T>(entity);

           entityEntry.State=EntityState.Deleted;
           await _context.SaveChangesAsync();
           
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result=await _context.Set<T>().ToListAsync();
                
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
          IQueryable <T> query =_context.Set<T>();

          query =includeProperties.Aggregate(query,(current,includeProperty)=>current.Include(includeProperty));

          return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result=await _context.Set<T>().FirstOrDefaultAsync(n=>n.Id ==id);
            return result;
        }
 

        public  async Task UpdateAsync(int id, T entity)
        {
           EntityEntry entityEntry=_context.Entry<T>(entity);

           entityEntry.State=EntityState.Modified;
           await _context.SaveChangesAsync();

           
        }
    }
}