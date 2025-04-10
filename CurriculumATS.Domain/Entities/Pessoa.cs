using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CurriculumATS.Domain.Entities;

public class Pessoa
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("nome")]
    public string Nome { get; set; }
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("senha")]
    public string SenhaHash { get; set; }
    [BsonElement("telefone")]
    public string Telefone { get; set; }
    [BsonElement("linkedin")]
    public string LinkedIn_Url { get; set; }
    [BsonElement("portifolio")]
    public string Portfolio_Url { get; set; }
    [BsonElement("cidade")]
    public string Cidade { get; set; }
    [BsonElement("uf")]
    public string UF { get; set; }
}

