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
    public class PacienteRepository : BaseRepository, IPacienteRepository
    {
        private readonly ConsultorioContext _context;

        public PacienteRepository(ConsultorioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PacienteDTO>> GetPaciente()
        {
            return await _context.Pacientes
                .Select(x => new PacienteDTO { Id = x.Id, Nome = x.Nome })
                .ToListAsync();
        }

        public async Task<Paciente> GetPacienteId(int id)
        {
            return await _context.Pacientes
                .Include(x => x.Consultas)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
