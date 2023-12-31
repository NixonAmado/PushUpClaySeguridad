using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistencia.Data;


namespace Application.Repository;

    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
    {
        private readonly DbAppContext _context;

        public DepartamentoRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<Departamento> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Departamentos as IQueryable<Departamento>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.NombreDep.ToLower() == search.ToLower());
                }
    
                query = query.OrderBy(p => p.Id);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }
    }