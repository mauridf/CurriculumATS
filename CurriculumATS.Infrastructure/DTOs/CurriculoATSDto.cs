namespace CurriculumATS.Infrastructure.DTOs;

public  class CurriculoATSDto
{
    public DadosPessoaisDto DadosPessoais { get; set; }
    public string Resumo { get; set; }
    public Dictionary<string, string> Habilidades { get; set; }
    public List<ExperienciaDto> Experiencias { get; set; }
    public List<FormacaoDto> Formacoes { get; set; }
    public List<CertificacaoDto> Certificacoes { get; set; }
}

public class DadosPessoaisDto
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string LinkedIn { get; set; }
    public string Portfolio { get; set; }
    public string CidadeUf { get; set; }
}

public class ExperienciaDto
{
    public string EmpresaCargo { get; set; }
    public string Periodo { get; set; }
    public string Atividades { get; set; }
}

public class FormacaoDto
{
    public string Graduacao { get; set; }
    public string InstituicaoCurso { get; set; }
    public string Periodo { get; set; }
}

public class CertificacaoDto
{
    public string Descricao { get; set; }
    public string Periodo { get; set; }
}
