using System;

namespace ConsultorioAPI.Models.DTOs
{
    public class ConsultaAtualizarDTO
    {
        public DateTime DataHorario { get; set; }
        public int Status { get; set; }
        public decimal Preco { get; set; }
        public int ProfissionalId { get; set; }
    }
}
