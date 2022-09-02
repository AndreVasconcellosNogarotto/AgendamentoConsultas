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
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IMapper _mapper;

        public ProfissionalController(IProfissionalRepository profissionalRepository, IMapper mapper)
        {
            _profissionalRepository = profissionalRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfissionalAsync()
        {
            var profissionais = await _profissionalRepository.GetProfissionais();

            return profissionais.Any() ?
                Ok(profissionais) : NotFound("Profissional não encontrado");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfissionalByIdAsync(int id)
        {
            if (id <= 0) return BadRequest("Profissional inválido.");

            var profissional = await _profissionalRepository.GetProfissionalByIdAsync(id);

            var profissionalRetorno = _mapper.Map<ProfissionalDetailsDTO>(profissional);

            return profissional != null ?
                Ok(profissionalRetorno) : NotFound("Profissional não existe na base de dados");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProfissionalAdicionarDTO profissional)
        {
            if (string.IsNullOrEmpty(profissional.Nome)) return BadRequest("Dados inválidos");

            var profissionalAdicionar = _mapper.Map<Profissional>(profissional);

            _profissionalRepository.Add(profissionalAdicionar);

            return await _profissionalRepository
                .SaveChangesAsync() ? Ok("Profissional adicionado com sucesso") : BadRequest("Erro ao salvar o profissional");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProfissionalAtualizacaoDTO profissional)
        {
            if (id <= 0) return BadRequest("Profissional não encontrado");

            var profissionalBanco = await _profissionalRepository.GetProfissionalByIdAsync(id);

            if (profissionalBanco == null) return NotFound("Profissional não encontrado na base de dados");

            var profissionalAtualizar = _mapper.Map(profissional, profissionalBanco);

            _profissionalRepository.Update(profissionalAtualizar);  

            return await _profissionalRepository
                .SaveChangesAsync() ? Ok("Profissional atualizado com sucesso")
                :BadRequest("Erro ao atualizar o profissional");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Profissional não encontrado");

            var profissionalExcluir = await _profissionalRepository.GetProfissionalByIdAsync(id);

            if (profissionalExcluir == null) return NotFound("Profissional não encontrado na base de dados");

            _profissionalRepository.Delete(profissionalExcluir);

            return await _profissionalRepository
                .SaveChangesAsync() ? Ok("Profissional deletado com sucessso") 
                : BadRequest("Erro ao deletar o profissional");
        }

        [HttpPost("adicionar-profissional")]
        public async Task<IActionResult> PostProfissionalEspecilidade(ProfissionalEspecilidadeAdicionarDTO profissional)
        {
            int profissionalId = profissional.ProfissionalId;
            int especialidadeId = profissional.EspecialidadeId;

            if (profissionalId <= 0 || especialidadeId <= 0) return BadRequest("Não existe esses dados na banco");

            var profissionalEspecialidade = await _profissionalRepository.GetProfissionalEspecilidade(profissionalId, especialidadeId);

            if (profissionalEspecialidade != null) return Ok("Especialidade já cadastrada.");

            var especialidadeAdicionar = new ProfissionalEspecialidade
            {
                EspecialidadeId = especialidadeId,
                ProfissionalId = profissionalId
            };

            _profissionalRepository.Add(especialidadeAdicionar);

            return await _profissionalRepository
                .SaveChangesAsync() ? Ok("Especialidade adicionada com sucessso")
                : BadRequest("Erro ao adicionar a especialidade");
        }

        [HttpDelete("{profissionalId}/deletar-especialidade/{especialidadeId}")]
        public async Task<IActionResult> DeleteProfissionalEspecialidade(int profissionalId, int especialidadeId)
        {
            if (profissionalId <= 0 || especialidadeId <= 0) return BadRequest("Dados inválidos.");

            var profissionalEspecialidade = await _profissionalRepository.GetProfissionalEspecilidade(profissionalId, especialidadeId);

            if (profissionalEspecialidade == null) return BadRequest("Especialidade não cadastrada.");

            _profissionalRepository.Delete(profissionalEspecialidade);

            return await _profissionalRepository
              .SaveChangesAsync() ? Ok("Especialidade deletada do profissional com sucessso")
              : BadRequest("Erro ao deletar a especialidade do profissional");
        }
        
    }
}
