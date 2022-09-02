using AutoMapper;
using Consultorio.Models.Entities;
using ConsultorioAPI.Models.DTOs;
using ConsultorioAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IConsultaRepository _repository;
        private readonly IMapper _mapper;

        public AgendamentoController(IConsultaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ConsultaParam parametros)
        {
            var consultas = await _repository.GetConsulta(parametros);

            var consultaRetorno = _mapper.Map<IEnumerable<ConsultaDetailsDTO>>(consultas);

            return consultaRetorno.Any()
                ? Ok(consultaRetorno)
                : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0 ) return BadRequest("Consulta inválida");

            var consulta = await _repository.GetConsultaById(id);

            var consultaRetorno = _mapper.Map<ConsultaDetailsDTO>(consulta);

            return consultaRetorno != null
                ? Ok(consultaRetorno)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(ConsultaAdicionarDTO consulta)
        {
            if (consulta == null) return BadRequest("Dados inválidos");

            var consultaAdicionar = _mapper.Map<Consulta>(consulta);

            _repository.Add(consultaAdicionar);

            return await _repository.SaveChangesAsync()
            ? Ok("Consulta agendada.")
            : BadRequest("Erro ao agendar a consulta");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ConsultaAtualizarDTO consulta)
        {
            if (consulta == null) return BadRequest("Dados inválidos");

            var consultaBanco = await _repository.GetConsultaById(id);

            if (consultaBanco == null) return BadRequest("Essa consulta não existe na base de dados");

            if(consulta.DataHorario == new DateTime()) consulta.DataHorario = consultaBanco.DataHorario;
  
            if(consulta.ProfissionalId <= 0) consulta.ProfissionalId = consultaBanco.ProfissionalId;
         
            var consultaAtualizar = _mapper.Map(consulta, consultaBanco);

            _repository.Update(consultaAtualizar);

            return await _repository.SaveChangesAsync()
            ? Ok("Agendamento da consulta foi atualizada.")
            : BadRequest("Erro ao atualizar o agendamento da consulta");

        }
    }
}
