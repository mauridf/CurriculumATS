using CurriculumATS.Application.DTOs;
using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormacaoController : ControllerBase
    {
        private readonly IFormacaoRepository _formacaoRepository;

        public FormacaoController(IFormacaoRepository formacaoRepository)
        {
            _formacaoRepository = formacaoRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] FormacaoRegisterDto dto)
        {
            var formacao = new Formacao
            {
                PessoaId = dto.PessoaId,
                Instituicao = dto.Instituicao,
                Graduacao = dto.Graduacao,
                Curso = dto.Curso,
                MesAnoInicio = dto.MesAnoInicio,
                MesAnoFim = dto.MesAnoFim
            };

            await _formacaoRepository.AddAsync(formacao);
            return Ok("Formação do Usuário cadastrada com sucesso!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var formacoes = await _formacaoRepository.GetAllAsync();
            return Ok(formacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var formacao = await _formacaoRepository.GetByIdAsync(id);
            if (formacao == null) return NotFound("Formação não encontrada.");
            return Ok(formacao);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<IActionResult> GetByPessoaId(string pessoaId)
        {
            var formacoes = await _formacaoRepository.GetByPessoaIdAsync(pessoaId);
            if (formacoes == null || !formacoes.Any())
                return NotFound("Formações não encontradas.");

            return Ok(formacoes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] FormacaoRegisterDto dto)
        {
            var formacao = await _formacaoRepository.GetByIdAsync(id);
            if (formacao == null) return NotFound("Formações não encontrada.");

            formacao.PessoaId = dto.PessoaId;
            formacao.Instituicao = dto.Instituicao;
            formacao.Graduacao = dto.Graduacao;
            formacao.Curso = dto.Curso;
            formacao.MesAnoInicio = dto.MesAnoInicio;
            formacao.MesAnoFim = dto.MesAnoFim;

            await _formacaoRepository.UpdateAsync(id, formacao);
            return Ok("Formação atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var formacao = await _formacaoRepository.GetByIdAsync(id);
            if (formacao == null) return NotFound("Formação não encontrada.");

            await _formacaoRepository.DeleteAsync(id);

            return Ok("Formação excluída com sucesso.");
        }
    }
}
