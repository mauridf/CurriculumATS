using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CurriculumATS.Domain.Entities;

public class Resumo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("pessoaid")]
    public string PessoaId { get; set; }
    [BsonElement("resumo")]
    public string DsResumo {  get; set; }
}
