using AutoMapper;
using Consultorio.Models.Entities;
using ConsultorioAPI.Context;
using ConsultorioAPI.Models.DTOs;
using ConsultorioAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioAPI.Repository
{
    public class EspecialidadeRepository : BaseRepository, IEspecialidadeRepository
    {
        private readonly ConsultorioContext _context;
     
        public EspecialidadeRepository(ConsultorioContext context) : base(context)
        {
            _context = context;            
        }
        public async Task<IEnumerable<EspecialidadeDTO>> GetEspecialidades()
        {
            return await _context.Especialidades
                 .Select(x => new EspecialidadeDTO { Id = x.Id, Nome = x.Nome, Ativa = x.Ativa })
                 .ToListAsync();
        }

        public async Task<Especialidade> GetEspecialidadeById(int id)
        {
            return await _context.Especialidades
                 .Include(x => x.Profissionais)
                 .Where(x => x.Id == id)
                 .FirstOrDefaultAsync();
        }       
    }
}
