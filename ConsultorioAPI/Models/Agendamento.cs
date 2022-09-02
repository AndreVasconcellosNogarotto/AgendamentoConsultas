using System;
using System.ComponentModel.DataAnnotations;

namespace ConsultorioAPI.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NomePaciente { get; set; }
        public int Idade { get; set; }
        public DateTime Horario { get; set; }
    }
}
