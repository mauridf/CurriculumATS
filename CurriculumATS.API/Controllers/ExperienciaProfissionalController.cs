using CurriculumATS.Application.DTOs;
using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using CurriculumATS.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienciaProfissionalController : ControllerBase
    {
        private readonly IExperienciaProfissionalRepository _experienciaRepository;

        public ExperienciaProfissionalController(IExperienciaProfissionalRepository experienciaRepository)
        {
            _experienciaRepository = experienciaRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ExperienciaProfissionalRegisterDto dto)
        {
            var experiencia = new ExperienciaProfissional
            {
                PessoaId = dto.PessoaId,
                NomeEmpresa = dto.NomeEmpresa,
                Cargo = dto.Cargo,
                MesAnoInicio = dto.MesAnoInicio,
                MesAnoFim = dto.MesAnoFim,
                DsAtividadeExperiencia = dto.DsAtividadeExperiencia
            };

            await _experienciaRepository.AddAsync(experiencia);
            return Ok("Experiência Profissional do Usuário cadastrada com sucesso!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var experiencias = await _experienciaRepository.GetAllAsync();
            return Ok(experiencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var experiencia = await _experienciaRepository.GetByIdAsync(id);
            if (experiencia == null) return NotFound("Experiência Profissional não encontrada.");
            return Ok(experiencia);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<IActionResult> GetByPessoaId(string pessoaId)
        {
            var experiencias = await _experienciaRepository.GetByPessoaIdAsync(pessoaId);
            if (experiencias == null || !experiencias.Any())
                return NotFound("Experiências Profissionais não encontradas.");

            return Ok(experiencias);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ExperienciaProfissionalRegisterDto dto)
        {
            var experiencia = await _experienciaRepository.GetByIdAsync(id);
            if (experiencia == null) return NotFound("Experiência Profissional não encontrada.");

            experiencia.PessoaId = dto.PessoaId;
            experiencia.NomeEmpresa = dto.NomeEmpresa;
            experiencia.Cargo = dto.Cargo;
            experiencia.MesAnoInicio = dto.MesAnoInicio;
            experiencia.MesAnoFim = dto.MesAnoFim;
            experiencia.DsAtividadeExperiencia = dto.DsAtividadeExperiencia;

            await _experienciaRepository.UpdateAsync(id, experiencia);
            return Ok("Experiência Profissional atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var experiencia = await _experienciaRepository.GetByIdAsync(id);
            if (experiencia == null) return NotFound("Experiência Profissional não encontrada.");

            await _experienciaRepository.DeleteAsync(id);

            return Ok("Experiência Profissional excluída com sucesso.");
        }
    }
}
