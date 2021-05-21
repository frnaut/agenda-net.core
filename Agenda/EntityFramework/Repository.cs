using Agenda.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.EtityFramework
{
    public class Repository<T>  where T: BaseClass
    {
        private Context _context;
        public Repository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        
        public async Task<T> GetByIdAsync(int Id) => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == Id);

        public T GetById(int Id) => _context.Set<T>().FirstOrDefault(x => x.Id == Id);
        public async Task<T> CreateAsync(T model)
        {
            var newModel = await _context.Set<T>().AddAsync(model);
            return newModel.Entity;
        }

        public void Update(T model)
        {
            _context.Set<T>().Update(model);
        }

        public void Delete(int id)
        {
            var model = GetById(id);
            if (model != null)
                _context.Set<T>().Remove(model);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
