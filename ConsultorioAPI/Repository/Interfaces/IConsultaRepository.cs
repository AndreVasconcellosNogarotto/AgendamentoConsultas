using Consultorio.Models.Entities;
using ConsultorioAPI.Models.DTOs;
using ConsultorioAPI.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultorioAPI.Repository
{
    public interface IConsultaRepository : IBaseRepository
    {
        Task<IEnumerable<Consulta>> GetConsulta(ConsultaParam parametros);
        Task<Consulta> GetConsultaById(int id);

    }
}
