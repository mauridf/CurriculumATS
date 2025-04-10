using CurriculumATS.Application.DTOs;
using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificacoesCertificadosController : ControllerBase
    {
        private readonly ICertificacoesCertificadosRepository _certRepository;

        public CertificacoesCertificadosController(ICertificacoesCertificadosRepository certRepository)
        {
            _certRepository = certRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CertificacaoCertificadoRegisterDto dto)
        {
            var cert = new CertificacoesCertificados
            {
                PessoaId = dto.PessoaId,
                Descricao = dto.Descricao,
                MesAno = dto.MesAno
            };

            await _certRepository.AddAsync(cert);
            return Ok("Certificação/Certificado do Usuário cadastrado com sucesso!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var certs = await _certRepository.GetAllAsync();
            return Ok(certs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var cert = await _certRepository.GetByIdAsync(id);
            if (cert == null) return NotFound("certificação/Certificado não encontrado.");
            return Ok(cert);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<IActionResult> GetByPessoaId(string pessoaId)
        {
            var certs = await _certRepository.GetByPessoaIdAsync(pessoaId);
            if (certs == null || !certs.Any())
                return NotFound("Certificação/Certificado não encontrados.");

            return Ok(certs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CertificacaoCertificadoRegisterDto dto)
        {
            var cert = await _certRepository.GetByIdAsync(id);
            if (cert == null) return NotFound("Certificação/Certificado não encontrado.");

            cert.PessoaId = dto.PessoaId;
            cert.Descricao = dto.Descricao;
            cert.MesAno = dto.MesAno;

            await _certRepository.UpdateAsync(id, cert);
            return Ok("Certificação/Certificado atualizado com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var cert = await _certRepository.GetByIdAsync(id);
            if (cert == null) return NotFound("Certificação/Certificado não encontrado.");

            await _certRepository.DeleteAsync(id);

            return Ok("Certificão/Certificado excluído com sucesso.");
        }
    }
}
