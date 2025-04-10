using CurriculumATS.Domain.Interfaces;
using CurriculumATS.Infrastructure.DTOs;
using CurriculumATS.Infrastructure.Interfaces;
using CurriculumATS.Domain.Enums;

namespace CurriculumATS.Application.Services;

public class CurriculoATSService : ICurriculoATSService
{
    private readonly IPessoaRepository _pessoaRepo;
    private readonly IResumoRepository _resumoRepo;
    private readonly IHabilidadeRepository _habilidadeRepo;
    private readonly IExperienciaProfissionalRepository _experienciaRepo;
    private readonly IFormacaoRepository _formacaoRepo;
    private readonly ICertificacoesCertificadosRepository _certificacaoRepo;

    public CurriculoATSService(IPessoaRepository pessoaRepo, IResumoRepository resumoRepo, IHabilidadeRepository habilidadeRepo,
        IExperienciaProfissionalRepository experienciaRepo, IFormacaoRepository formacaoRepo, ICertificacoesCertificadosRepository certificacaoRepo)
    {
        _pessoaRepo = pessoaRepo;
        _resumoRepo = resumoRepo;
        _habilidadeRepo = habilidadeRepo;
        _experienciaRepo = experienciaRepo;
        _formacaoRepo = formacaoRepo;
        _certificacaoRepo = certificacaoRepo;
    }

    public async Task<CurriculoATSDto> MontarCurriculoAsync(string pessoaId)
    {
        var pessoa = await _pessoaRepo.GetByIdAsync(pessoaId);
        if (pessoa == null) return null;

        var resumo = await _resumoRepo.GetByPessoaIdAsync(pessoaId);
        var habilidades = await _habilidadeRepo.GetByPessoaIdAsync(pessoaId);
        var experiencias = await _experienciaRepo.GetByPessoaIdAsync(pessoaId);
        var formacoes = await _formacaoRepo.GetByPessoaIdAsync(pessoaId);
        var certificacoes = await _certificacaoRepo.GetByPessoaIdAsync(pessoaId);

        var dto = new CurriculoATSDto
        {
            DadosPessoais = new DadosPessoaisDto
            {
                Nome = pessoa.Nome,
                Telefone = pessoa.Telefone,
                LinkedIn = pessoa.LinkedIn_Url,
                Portfolio = pessoa.Portfolio_Url,
                CidadeUf = $"{pessoa.Cidade}/{pessoa.UF}"
            },
            Resumo = resumo?.DsResumo,
            Habilidades = habilidades?
                .GroupBy(h => h.Tipo)
                .ToDictionary(
                    g => Enum.GetName(typeof(TipoHabilidade), g.Key),
                    g => string.Join(", ", g.Select(x => x.DsHabilidade))
                ),
            Experiencias = experiencias?.OrderByDescending(e => e.MesAnoInicio).Select(e => new ExperienciaDto
            {
                EmpresaCargo = $"{e.NomeEmpresa} - {e.Cargo}",
                Periodo = $"{e.MesAnoInicio:yyyy-MM} - {(string.IsNullOrEmpty(e.MesAnoFim?.ToString()) ? "Presente" : e.MesAnoFim?.ToString("yyyy-MM"))}",
                Atividades = e.DsAtividadeExperiencia
            }).ToList() ?? new(),
            Formacoes = formacoes?.OrderByDescending(f => f.MesAnoInicio).Select(f => new FormacaoDto
            {
                Graduacao = Enum.GetName(typeof(TipoGraduacao), f.Graduacao),
                InstituicaoCurso = $"{f.Instituicao} - {f.Curso}",
                Periodo = $"{f.MesAnoInicio:yyyy-MM} - {(string.IsNullOrEmpty(f.MesAnoFim?.ToString()) ? "Em Andamento" : f.MesAnoFim?.ToString("yyyy-MM"))}"
            }).ToList() ?? new(),
            Certificacoes = certificacoes?.Select(c => new CertificacaoDto
            {
                Descricao = c.Descricao,
                Periodo = c.MesAno.HasValue ? c.MesAno.Value.ToString("yyyy-MM") : ""
            }).ToList() ?? new()
        };

        return dto;
    }
}
