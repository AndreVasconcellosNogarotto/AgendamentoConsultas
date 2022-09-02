using Consultorio.Models.Entities;
using System.Collections.Generic;

namespace ConsultorioAPI.Models.DTOs
{
    public class PacienteDetailsDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public List<ConsultaDTO> Consultas { get; set; }
    }
}
