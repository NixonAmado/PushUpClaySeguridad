using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistencia.Data;


namespace Application.Repository;

    public class TurnoRepository : GenericRepository<Turno>, ITurno
    {
        private readonly DbAppContext _context;

        public TurnoRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<Turno> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Turnos as IQueryable<Turno>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Nombre.ToLower() == search.ToLower());
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