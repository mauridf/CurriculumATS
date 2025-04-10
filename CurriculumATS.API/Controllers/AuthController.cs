using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using CurriculumATS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using CurriculumATS.Application.DTOs;

namespace CurriculumATS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IAuthService _authService;

    public AuthController(IPessoaRepository pessoaRepository, IAuthService authService)
    {
        _pessoaRepository = pessoaRepository;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] PessoaLoginDto dto)
    {
        var pessoa = await _pessoaRepository.GetByEmailAsync(dto.Email);
        if (pessoa == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, pessoa.SenhaHash))
        {
            return Unauthorized("Usuário ou senha inválidos.");
        }

        var token = _authService.GenerateToken(pessoa);
        return Ok(new
        {
            token,
            id = pessoa.Id.ToString() // aqui você envia o id como string
        });
    }
}
