using CurriculumATS.Domain.Enums;
using CurriculumATS.Application.Settings;
using System.Text.Json.Serialization;

namespace CurriculumATS.Application.DTOs;

public class FormacaoRegisterDto
{
    public string PessoaId { get; set; }
    public string Instituicao { get; set; }
    public TipoGraduacao Graduacao { get; set; }
    public string Curso { get; set; }
    [JsonConverter(typeof(MesAnoConverter))]
    public DateTime MesAnoInicio { get; set; }

    [JsonConverter(typeof(MesAnoConverter))]
    public DateTime? MesAnoFim { get; set; }
}
