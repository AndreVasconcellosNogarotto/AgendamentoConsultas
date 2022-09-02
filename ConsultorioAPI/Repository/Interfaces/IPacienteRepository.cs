using Consultorio.Models.Entities;
using ConsultorioAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultorioAPI.Repository.Interfaces
{
    public interface IPacienteRepository : IBaseRepository
    {
        Task<IEnumerable<PacienteDTO>> GetPaciente();
        Task<Paciente> GetPacienteId(int id);
    }
}
