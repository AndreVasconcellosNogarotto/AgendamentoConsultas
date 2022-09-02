using Consultorio.Models.Entities;
using ConsultorioAPI.Context;
using ConsultorioAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioAPI.Repository
{
    public class ConsultaRepository : BaseRepository, IConsultaRepository
    {
        private readonly ConsultorioContext _context;

        public ConsultaRepository(ConsultorioContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Consulta>> GetConsulta(ConsultaParam parametros)
        {
            var consulta = _context.Consultas
                .Include(x => x.Paciente)
                .Include(x => x.Profissional)
                .Include(x => x.Especialidade).AsQueryable();
                
            DateTime dataVazia = new DateTime();

            if(parametros.DataInicio != dataVazia) consulta = consulta.Where(x => x.DataHorario >= parametros.DataInicio);
            
            if(parametros.DataFim != dataVazia) consulta = consulta.Where(x => x.DataHorario >= parametros.DataFim);

            if (!string.IsNullOrEmpty(parametros.NomeEspecialidade))
            {
                string nomeEspecialidade = parametros.NomeEspecialidade.ToLower().Trim();
                consulta = consulta.Where(x => x.Especialidade.Nome.ToLower().Contains(nomeEspecialidade));
            }

            return await consulta.ToListAsync();
        }

        public async Task<Consulta> GetConsultaById(int id)
        {
            return await _context.Consultas
               .Include(x => x.Paciente)
               .Include(x => x.Profissional)
               .Include(x => x.Especialidade)
               .Where(x => x.Id ==id)
               .FirstOrDefaultAsync();
        }
    }
}
