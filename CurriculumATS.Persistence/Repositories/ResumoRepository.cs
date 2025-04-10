using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using MongoDB.Driver;

namespace CurriculumATS.Persistence.Repositories;

public class ResumoRepository : Repository<Resumo>, IResumoRepository
{
    private readonly IMongoCollection<Resumo> _collection;

    public ResumoRepository(IMongoDatabase database) : base(database, nameof(Resumo))
    {
        _collection = database.GetCollection<Resumo>(nameof(Resumo));
    }

    public async Task<Resumo> GetByPessoaIdAsync(string pessoaId)
    {
        return await _collection
             .Find(r => r.PessoaId == pessoaId)
             .FirstOrDefaultAsync();
    }

    public async Task DeleteByPessoaIdAsync(string pessoaId)
    {
        var filter = Builders<Resumo>.Filter.Eq(r => r.PessoaId, pessoaId);
        await _collection.DeleteManyAsync(filter);
    }
}
