using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using MongoDB.Driver;

namespace CurriculumATS.Persistence.Repositories;

public class HabilidadeRepository : Repository<Habilidades>, IHabilidadeRepository
{
    private readonly IMongoCollection<Habilidades> _collection;

    public HabilidadeRepository(IMongoDatabase database) : base(database, nameof(Habilidades))
    {
        _collection = database.GetCollection<Habilidades>(nameof(Habilidades));
    }

    public async Task<List<Habilidades>> GetByPessoaIdAsync(string pessoaId)
    {
        return await _collection
            .Find(r => r.PessoaId == pessoaId)
            .ToListAsync();
    }

    public async Task DeleteByPessoaIdAsync(string pessoaId)
    {
        var filter = Builders<Habilidades>.Filter.Eq(r => r.PessoaId, pessoaId);
        await _collection.DeleteManyAsync(filter);
    }
}
