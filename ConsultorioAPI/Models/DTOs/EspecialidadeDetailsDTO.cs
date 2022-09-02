using Consultorio.Models.Entities;
using System.Collections.Generic;

namespace ConsultorioAPI.Models.DTOs
{
    public class EspecialidadeDetailsDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativa { get; set; }
        public List<ProfissionalDTO> Profissionais { get; set; }
    }
}
