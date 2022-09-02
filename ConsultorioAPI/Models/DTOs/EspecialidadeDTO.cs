using Consultorio.Models.Entities;
using System.Collections.Generic;

namespace ConsultorioAPI.Models.DTOs
{
    public class EspecialidadeDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativa { get; set; }       
    }
}
