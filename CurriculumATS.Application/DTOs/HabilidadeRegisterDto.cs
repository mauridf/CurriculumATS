using CurriculumATS.Domain.Enums;

namespace CurriculumATS.Application.DTOs;

public class HabilidadeRegisterDto
{
    public string PessoaId { get; set; }
    public TipoHabilidade Tipo { get; set; }
    public string DsHabilidade { get; set; }
}
