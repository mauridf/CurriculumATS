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
    public class HabilidadesController : ControllerBase
    {
        private readonly IHabilidadeRepository _habilidadeRepository;

        public HabilidadesController(IHabilidadeRepository habilidadeRepository)
        {
            _habilidadeRepository = habilidadeRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] HabilidadeRegisterDto dto)
        {
            var habilidade = new Habilidades
            {
                PessoaId = dto.PessoaId,
                Tipo = dto.Tipo,
                DsHabilidade = dto.DsHabilidade
            };

            await _habilidadeRepository.AddAsync(habilidade);
            return Ok("Habilidade do Usuário cadastrada com sucesso!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var habilidades = await _habilidadeRepository.GetAllAsync();
            return Ok(habilidades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var habilidade = await _habilidadeRepository.GetByIdAsync(id);
            if (habilidade == null) return NotFound("Habilidade não encontrada.");
            return Ok(habilidade);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<IActionResult> GetByPessoaId(string pessoaId)
        {
            var habilidades = await _habilidadeRepository.GetByPessoaIdAsync(pessoaId);
            if (habilidades == null || !habilidades.Any())
                return NotFound("Habilidades não encontradas.");

            return Ok(habilidades);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] HabilidadeRegisterDto dto)
        {
            var habilidade = await _habilidadeRepository.GetByIdAsync(id);
            if (habilidade == null) return NotFound("Habilidade não encontrada.");

            habilidade.PessoaId = dto.PessoaId;
            habilidade.Tipo = dto.Tipo;
            habilidade.DsHabilidade = dto.DsHabilidade;

            await _habilidadeRepository.UpdateAsync(id, habilidade);
            return Ok("Habilidade atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var habilidade = await _habilidadeRepository.GetByIdAsync(id);
            if (habilidade == null) return NotFound("Habilidade não encontrada.");

            await _habilidadeRepository.DeleteAsync(id);

            return Ok("Habilidade excluída com sucesso.");
        }
    }
}
