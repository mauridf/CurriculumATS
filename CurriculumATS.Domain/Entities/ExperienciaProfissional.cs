using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CurriculumATS.Domain.Entities;

public class ExperienciaProfissional
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("pessoaid")]
    public string PessoaId { get; set; }
    [BsonElement("empresa")]
    public string NomeEmpresa { get; set; }
    [BsonElement("cargo")]
    public string Cargo {  get; set; }
    [BsonElement("inicio")]
    public DateTime MesAnoInicio { get; set; }
    [BsonElement("fim")]
    public DateTime? MesAnoFim {  get; set; }
    [BsonElement("atividades")]
    public string DsAtividadeExperiencia { get; set; }
}
