using AutoMapper;
using Consultorio.Models.Entities;
using ConsultorioAPI.Models.DTOs;
using ConsultorioAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeRepository _repository;
        private readonly IMapper _mapper;

        public EspecialidadeController(IEspecialidadeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEspecialidades()
        {
            var especialidades = await _repository.GetEspecialidades();

            return especialidades.Any() 
                ? Ok(especialidades) 
                : NotFound("Especialidade não encontrada.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEspecialidadesById(int id)
        {
            if (id <= 0) return BadRequest("Não existe essa especialidade");

            var especialidade = await _repository.GetEspecialidadeById(id);

            var especialidadeRetorno = _mapper.Map<EspecialidadeDetailsDTO>(especialidade);

            return especialidadeRetorno != null ? Ok(especialidadeRetorno) : NotFound("Especialidade não encontrada.");

        }

        [HttpPost]
        public async Task<IActionResult> Post(EspecialidadeAdicionarDTO especialidade)
        {
            if (string.IsNullOrEmpty(especialidade.Nome)) return BadRequest("Nome inválido.");

            var especialidadeAdicionar = new Especialidade
            {
                Nome = especialidade.Nome,
                Ativa = especialidade.Ativa
            };

            _repository.Add(especialidadeAdicionar);

            return await _repository
                .SaveChangesAsync() 
                ? Ok("Especialidade adicionada.") 
                : BadRequest("Erro ao adicionar especialidade");
        }

        [HttpPut("{id}/atualiza-status")]
        public async Task<IActionResult> Put(int id, bool ativo)
        {
            if (id <= 0) return BadRequest("Especialidade inválida.");

            var especialidade = await _repository.GetEspecialidadeById(id);

            if (especialidade == null) return NotFound("Especialidade não encontrada");

            string status = ativo ? "ativo" : "inativo";
            if (especialidade.Ativa == ativo) return Ok("Especialidade já está" + status);

            especialidade.Ativa = ativo;

            _repository.Update(especialidade);

            return await _repository
            .SaveChangesAsync()
            ? Ok("Especialidade atualizada com sucesso.")
            : BadRequest("Erro ao atualizar os status especialidade");
        }
    }
}
