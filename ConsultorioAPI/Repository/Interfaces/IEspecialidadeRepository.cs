using Consultorio.Models.Entities;
using ConsultorioAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultorioAPI.Repository.Interfaces
{
    public interface IEspecialidadeRepository : IBaseRepository
    {
        Task<IEnumerable<EspecialidadeDTO>> GetEspecialidades();
        Task<Especialidade> GetEspecialidadeById(int id);
    }
}
