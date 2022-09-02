using AutoMapper;
using Consultorio.Models.Entities;
using ConsultorioAPI.Models.DTOs;
using ConsultorioAPI.Repository.Interfaces;
using ConsultorioAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository _repository;
        private readonly IMapper _mapper;

        public PacienteController(IPacienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaciente()
        {
            var pacientes = await _repository.GetPaciente();

            return pacientes
               .Any() ? Ok(pacientes) : BadRequest("Paciente não encontrado");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await _repository
                .GetPacienteId(id);

            var pacienteRetorno = _mapper.Map<PacienteDetailsDTO>(paciente);

            return pacienteRetorno != null ? Ok(pacienteRetorno) : BadRequest("Paciente não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Post(PacienteAdicionarDTO paciente)
        {
            if (paciente == null) return BadRequest("Dados inválidos");

            var pacienteAdicionar = _mapper.Map<Paciente>(paciente);

            _repository.Add(pacienteAdicionar);

            return await _repository
                .SaveChangesAsync() ? Ok("Paciente adicionado com sucesso.") 
                :BadRequest("Erro ao salvar o paciente.");

        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PacienteAtualizacaoDTO paciente)
        {
            if (id <= 0) return BadRequest("Usuário não encontrado");

            var pacienteBanco = await _repository.GetPacienteId(id);

            var pacienteAtualizar = _mapper.Map(paciente, pacienteBanco);

            _repository.Update(pacienteAtualizar);

            return await _repository
                .SaveChangesAsync() ? Ok("Paciente atualizado com sucesso.")
                :BadRequest("Erro ao atualizar o paciente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Usuário não encontrado");

            var pacienteExcluir = await _repository.GetPacienteId(id);

            if (pacienteExcluir == null) return NotFound("Paciente não encontrado.");

            _repository.Delete(pacienteExcluir);

            return await _repository
                .SaveChangesAsync() ? Ok("Paciente deletado com sucesso.")
                : BadRequest("Erro ao deletar o paciente.");
        }

    }
}