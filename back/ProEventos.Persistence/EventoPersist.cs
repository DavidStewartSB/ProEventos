using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{

    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventoPersist(ProEventosContext _context)
        {
            this._context = _context;

        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(e=> e.Lotes)
            .Include(e => e.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.ID);
            return await query.ToArrayAsync();
        }

        public Task<Evento> GetAllEventosAsync(int userId, bool includePalestrantes = false)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
           IQueryable<Evento> query = _context.Eventos
            .Include(e=> e.Lotes)
            .Include(e => e.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
            }


            query = query.OrderBy(e => e.ID)
                         .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
            
            return await query.ToArrayAsync();
        }

        public Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Evento> GetEventosByIdAsync(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(e=> e.Lotes)
            .Include(e => e.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
            }


            query = query.OrderBy(e => e.ID)
                         .Where(e => e.ID == EventoId);
            
            return await query.FirstOrDefaultAsync();
        }

        
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }


    }
}