using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CurriculumATS.Domain.Entities;

public class CertificacoesCertificados
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("pessoaid")]
    public string PessoaId { get; set; }
    [BsonElement("descricao")]
    public string Descricao { get; set; }
    [BsonElement("periodo")]
    public DateTime? MesAno { get; set; }
}
