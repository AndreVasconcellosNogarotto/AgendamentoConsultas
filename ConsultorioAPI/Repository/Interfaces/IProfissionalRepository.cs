using Consultorio.Models.Entities;
using ConsultorioAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultorioAPI.Repository.Interfaces
{
    public interface IProfissionalRepository : IBaseRepository
    {
        Task<IEnumerable<ProfissionalDTO>> GetProfissionais();
        Task<Profissional> GetProfissionalByIdAsync(int id);
        Task<ProfissionalEspecialidade> GetProfissionalEspecilidade(int profissionalId, int especialidadeId);
    }
}
