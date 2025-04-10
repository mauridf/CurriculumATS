using CurriculumATS.Domain.Interfaces;
using CurriculumATS.Infrastructure.DTOs;
using CurriculumATS.Infrastructure.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CurriculumATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculoATSController : ControllerBase
    {
        private readonly ICurriculoATSService _curriculoService;
        private readonly IConverter _pdfConverter;

        public CurriculoATSController(ICurriculoATSService curriculoService, IConverter pdfConverter)
        {
            _curriculoService = curriculoService;
            _pdfConverter = pdfConverter;
        }

        [HttpGet("montar-curriculo/{pessoaId}")]
        public async Task<IActionResult> MontarCurriculo(string pessoaId)
        {
            var curriculo = await _curriculoService.MontarCurriculoAsync(pessoaId);
            if (curriculo == null) return NotFound("Currículo não encontrado.");
            return Ok(curriculo);
        }

        [HttpGet("html/{pessoaId}")]
        public async Task<IActionResult> GerarHtmlCurriculo(string pessoaId)
        {
            var dto = await _curriculoService.MontarCurriculoAsync(pessoaId);
            if (dto == null)
                return NotFound("Currículo não encontrado.");

            var html = GerarHtml(dto);

            // Retorna o HTML renderizado no navegador
            return Content(html, "text/html", Encoding.UTF8);
        }

        [HttpGet("html-salvar/{pessoaId}")]
        public async Task<IActionResult> GerarHtmlESalvar(string pessoaId)
        {
            var dto = await _curriculoService.MontarCurriculoAsync(pessoaId);
            if (dto == null)
                return NotFound("Currículo não encontrado.");

            var html = GerarHtml(dto);

            // Cria a pasta se não existir
            var wwwRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "curriculos");
            if (!Directory.Exists(wwwRoot))
                Directory.CreateDirectory(wwwRoot);

            // Define o nome do arquivo
            var nomeArquivo = $"curriculo_{dto.DadosPessoais.Nome}_{DateTime.Now:yyyyMMddHHmmss}.html";
            var caminhoCompleto = Path.Combine(wwwRoot, nomeArquivo);

            // Salva o HTML como arquivo
            await System.IO.File.WriteAllTextAsync(caminhoCompleto, html, Encoding.UTF8);

            // Monta a URL pública para o arquivo
            var urlBase = $"{Request.Scheme}://{Request.Host}";
            var urlArquivo = $"{urlBase}/curriculos/{nomeArquivo}";

            return Ok(new { url = urlArquivo });
        }

        private string GerarHtml(CurriculoATSDto dto)
        {
            var sb = new StringBuilder();
            sb.Append($@"
            <!DOCTYPE html>
            <html lang='pt-BR'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Currículo - {dto.DadosPessoais.Nome}</title>
                <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css' rel='stylesheet'>
                <link href='https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;600&display=swap' rel='stylesheet'>
                <style>
                    body {{
                        font-family: 'Open Sans', sans-serif;
                        font-size: 14px;
                        margin: 20px;
                        color: #212529;
                    }}
                    h1, h2 {{
                        color: #2c3e50;
                        font-weight: 600;
                        margin-top: 20px;
                        margin-bottom: 10px;
                    }}
                    p, li {{
                        line-height: 1.6;
                    }}
                    .section {{
                        margin-bottom: 30px;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>{dto.DadosPessoais.Nome}</h1>
                    <p><strong>Telefone:</strong> {dto.DadosPessoais.Telefone}</p>
                    <p><strong>LinkedIn:</strong> {dto.DadosPessoais.LinkedIn}</p>
                    <p><strong>Portfólio:</strong> {dto.DadosPessoais.Portfolio}</p>
                    <p><strong>Cidade/UF:</strong> {dto.DadosPessoais.CidadeUf}</p>

                    <div class='section'>
                        <h2>Resumo</h2>
                        <p>{dto.Resumo}</p>
                    </div>

                    <div class='section'>
                        <h2>Habilidades</h2>");
                    foreach (var categoria in dto.Habilidades)
                    {
                        sb.Append($"<p><strong>{categoria.Key}:</strong> {categoria.Value}</p>");
                    }
                    sb.Append($@"
                    </div>

                    <div class='section'>
                        <h2>Experiências Profissionais</h2>
                        <ul class='list-unstyled'>");
                    foreach (var exp in dto.Experiencias)
                    {
                        sb.Append($@"
                            <li class='mb-3'>
                                <strong>{exp.EmpresaCargo}</strong><br />
                                <em>{exp.Periodo}</em><br />
                                <p>{exp.Atividades}</p>
                            </li>");
                    }
                    sb.Append($@"
                        </ul>
                    </div>

                    <div class='section'>
                        <h2>Formações</h2>
                        <ul class='list-unstyled'>");
                    foreach (var form in dto.Formacoes)
                    {
                        sb.Append($@"
                            <li class='mb-2'>
                                <strong>{form.InstituicaoCurso}</strong><br />
                                {form.Graduacao} - <em>{form.Periodo}</em>
                            </li>");
                    }
                    sb.Append($@"
                        </ul>
                    </div>

                    <div class='section'>
                        <h2>Certificações</h2>
                        <ul class='list-unstyled'>");
                    foreach (var cert in dto.Certificacoes)
                    {
                        sb.Append($@"
                            <li class='mb-2'>
                                <strong>{cert.Descricao}</strong><br />
                                <em>{cert.Periodo}</em>
                            </li>");
                    }
                    sb.Append($@"
                        </ul>
                    </div>
                </div>
            </body>
            </html>");
            return sb.ToString();
        }

    }
}
