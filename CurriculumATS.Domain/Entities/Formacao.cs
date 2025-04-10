using CurriculumATS.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CurriculumATS.Domain.Entities;

public class Formacao
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("pessoaid")]
    public string PessoaId { get; set; }
    [BsonElement("instituicao")]
    public string Instituicao { get; set; }
    [BsonElement("graduacao")]
    public TipoGraduacao Graduacao { get; set; }
    [BsonElement("curso")]
    public string Curso { get; set; }
    [BsonElement("inicio")]
    public DateTime MesAnoInicio { get; set; }
    [BsonElement("fim")]
    public DateTime? MesAnoFim { get; set; }
}
