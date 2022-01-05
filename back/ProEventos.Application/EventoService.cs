using System.Threading.Tasks;
using System;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoServices
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _eventoPersist = eventoPersist;
            _geralPersist = geralPersist;

        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if(await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventosByIdAsync(model.ID, false);
                }
                    return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
               
            
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventosByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.ID = evento.ID;

                _geralPersist.Update(model);

                if(await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventosByIdAsync(model.ID, false);
                }

                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventosByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("O Evento para o delete não foi encontrado");

              

                _geralPersist.Delete<Evento>(evento);

                return await _geralPersist.SaveChangesAsync();
            
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var  eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if(eventos == null) return null;
                return eventos;
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var  eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if(eventos == null) return null;
                return eventos;
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        }

        public async Task<Evento> GetEventosByAsync(int EventoId, bool includePalestrantes = false)
        {
            throw new System.NotImplementedException();
        }

        
    }
}