using CurriculumATS.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CurriculumATS.Domain.Entities;

public class Habilidades
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("pessoaid")]
    public string PessoaId { get; set; }
    [BsonElement("tipo")]
    public TipoHabilidade Tipo {  get; set; }
    [BsonElement("habilidade")]
    public string DsHabilidade { get; set; }
}
