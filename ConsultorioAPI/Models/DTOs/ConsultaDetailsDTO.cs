using System;

namespace ConsultorioAPI.Models.DTOs
{
    public class ConsultaDetailsDTO
    {
        public int Id { get; set; }
        public DateTime DataHorario { get; set; }
        public int Status { get; set; }
        public decimal Preco { get; set; }
        public EspecialidadeDTO Especialidade { get; set; }
        public ProfissionalDTO Profissional { get; set; }
        public PacienteDTO Paciente { get; set; }
    }
}
