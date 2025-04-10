using CurriculumATS.Application.Settings;
using System.Text.Json.Serialization;

namespace CurriculumATS.Application.DTOs;

public class CertificacaoCertificadoRegisterDto
{
    public string PessoaId { get; set; }
    public string Descricao { get; set; }
    [JsonConverter(typeof(MesAnoConverter))]
    public DateTime? MesAno { get; set; }
}
