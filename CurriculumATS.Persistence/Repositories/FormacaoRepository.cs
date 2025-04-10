using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using MongoDB.Driver;

namespace CurriculumATS.Persistence.Repositories;

public class FormacaoRepository : Repository<Formacao>, IFormacaoRepository
{
    private readonly IMongoCollection<Formacao> _collection;

    public FormacaoRepository(IMongoDatabase database) : base(database, nameof(Formacao))
    {
        _collection = database.GetCollection<Formacao>(nameof(Formacao));
    }

    public async Task<List<Formacao>> GetByPessoaIdAsync(string pessoaId)
    {
        return await _collection
             .Find(r => r.PessoaId == pessoaId)
             .ToListAsync();
    }

    public async Task DeleteByPessoaIdAsync(string pessoaId)
    {
        var filter = Builders<Formacao>.Filter.Eq(r => r.PessoaId, pessoaId);
        await _collection.DeleteManyAsync(filter);
    }
}
