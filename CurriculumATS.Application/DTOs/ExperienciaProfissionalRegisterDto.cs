using CurriculumATS.Application.Settings;
using System.Text.Json.Serialization;

namespace CurriculumATS.Application.DTOs;

public class ExperienciaProfissionalRegisterDto
{
    public string PessoaId { get; set; }
    public string NomeEmpresa { get; set; }
    public string Cargo { get; set; }
    [JsonConverter(typeof(MesAnoConverter))]
    public DateTime MesAnoInicio { get; set; }

    [JsonConverter(typeof(MesAnoConverter))]
    public DateTime? MesAnoFim { get; set; }
    public string DsAtividadeExperiencia { get; set; }
}
