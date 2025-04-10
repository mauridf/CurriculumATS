using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CurriculumATS.Application.DTOs;

namespace CurriculumATS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IResumoRepository _resumoRepository;
    private readonly IHabilidadeRepository _habilidadeRepository;
    private readonly IFormacaoRepository _formacaoRepository;
    private readonly IExperienciaProfissionalRepository _experienciaRepository;
    private readonly ICertificacoesCertificadosRepository _certificadosRepository;

    public PessoaController(
        IPessoaRepository pessoaRepository,
        IResumoRepository resumoRepository,
        IHabilidadeRepository habilidadeRepository,
        IFormacaoRepository formacaoRepository,
        IExperienciaProfissionalRepository experienciaRepository,
        ICertificacoesCertificadosRepository certificadosRepository)
    {
        _pessoaRepository = pessoaRepository;
        _resumoRepository = resumoRepository;
        _habilidadeRepository = habilidadeRepository;
        _formacaoRepository = formacaoRepository;
        _experienciaRepository = experienciaRepository;
        _certificadosRepository = certificadosRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PessoaRegisterDto dto)
    {
        var existing = await _pessoaRepository.GetByEmailAsync(dto.Email);
        if (existing != null) return BadRequest("E-mail já cadastrado.");

        var pessoa = new Pessoa
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Telefone = dto.Telefone,
            LinkedIn_Url = dto.LinkedIn_Url,
            Portfolio_Url = dto.Portfolio_Url,
            Cidade = dto.Cidade,
            UF = dto.UF
        };

        await _pessoaRepository.AddAsync(pessoa);
        return Ok("Usuário cadastrado com sucesso!");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pessoas = await _pessoaRepository.GetAllAsync();
        return Ok(pessoas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var pessoa = await _pessoaRepository.GetByIdAsync(id);
        if (pessoa == null) return NotFound("Pessoa não encontrada.");
        return Ok(pessoa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] PessoaRegisterDto dto)
    {
        var pessoa = await _pessoaRepository.GetByIdAsync(id);
        if (pessoa == null) return NotFound("Pessoa não encontrada.");

        pessoa.Nome = dto.Nome;
        pessoa.Email = dto.Email;
        pessoa.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        pessoa.Telefone = dto.Telefone;
        pessoa.LinkedIn_Url = dto.LinkedIn_Url;
        pessoa.Portfolio_Url = dto.Portfolio_Url;
        pessoa.Cidade = dto.Cidade;
        pessoa.UF = dto.UF;

        await _pessoaRepository.UpdateAsync(id, pessoa);
        return Ok("Pessoa atualizada com sucesso.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var pessoa = await _pessoaRepository.GetByIdAsync(id);
        if (pessoa == null) return NotFound("Pessoa não encontrada.");

        await _resumoRepository.DeleteByPessoaIdAsync(id);
        await _habilidadeRepository.DeleteByPessoaIdAsync(id);
        await _formacaoRepository.DeleteByPessoaIdAsync(id);
        await _experienciaRepository.DeleteByPessoaIdAsync(id);
        await _certificadosRepository.DeleteByPessoaIdAsync(id);
        await _pessoaRepository.DeleteAsync(id);

        return Ok("Pessoa e dados relacionados excluídos com sucesso.");
    }
}