using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Contexto;

namespace ProEventos.Persistence
{

    public class PalestrantPersist : IPalestrantPersist
    {
        private readonly ProEventosContext _context;
        public PalestrantPersist(ProEventosContext _context)
        {
            this._context = _context;

        }
       
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
             .Include(p => p.RedesSociais);
            if(includeEventos)
            {
                query = query
                         .Include(p => p.PalestrantesEventos)
                         .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(p => p.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
             .Include(p => p.RedesSociais);
            if(includeEventos)
            {
                query = query
                         .Include(p => p.PalestrantesEventos)
                         .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(p => p.Id)
                            .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();
        }

       

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
             .Include(p => p.RedesSociais);
            
            if(includeEventos)
            {
                query = query
                         .Include(p => p.PalestrantesEventos)
                         .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(p => p.Id)
                        .Where(p => p.Id == palestranteId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }


    }
}