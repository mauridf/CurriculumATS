using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CurriculumATS.Application.DTOs;
using CurriculumATS.Persistence.Repositories;

namespace CurriculumATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumoController : ControllerBase
    {
        private readonly IResumoRepository _resumoRepository;

        public ResumoController(IResumoRepository resumoRepository)
        {
            _resumoRepository = resumoRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ResumoRegisterDTO dto)
        {
            var resumo = new Resumo
            {
                PessoaId = dto.PessoaId,
                DsResumo = dto.DsResumo
            };

            await _resumoRepository.AddAsync(resumo);
            return Ok("Resumo do Usuário cadastrado com sucesso!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resumos = await _resumoRepository.GetAllAsync();
            return Ok(resumos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var resumo = await _resumoRepository.GetByIdAsync(id);
            if (resumo == null) return NotFound("Resumo não encontrado.");
            return Ok(resumo);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<IActionResult> GetByPessoaId(string pessoaId)
        {
            var resumo = await _resumoRepository.GetByPessoaIdAsync(pessoaId);
            if (resumo == null) return NotFound("Resumo não encontrado.");
            return Ok(resumo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ResumoRegisterDTO dto)
        {
            var resumo = await _resumoRepository.GetByIdAsync(id);
            if (resumo == null) return NotFound("Resumo não encontrado.");

            resumo.PessoaId = dto.PessoaId;
            resumo.DsResumo = dto.DsResumo;

            await _resumoRepository.UpdateAsync(id, resumo);
            return Ok("Resumo atualizado com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var resumo = await _resumoRepository.GetByIdAsync(id);
            if (resumo == null) return NotFound("Resumo não encontrado.");

            await _resumoRepository.DeleteAsync(id);

            return Ok("Resumo excluído com sucesso.");
        }
    }
}
