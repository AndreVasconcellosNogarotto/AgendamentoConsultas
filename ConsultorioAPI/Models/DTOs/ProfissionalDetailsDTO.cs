using Consultorio.Models.Entities;
using System.Collections.Generic;

namespace ConsultorioAPI.Models.DTOs
{
    public class ProfissionalDetailsDTO
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int TotalConsultas { get; set; }
        public string[] Especialidades { get; set; }
    }
}
