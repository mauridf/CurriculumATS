using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using MongoDB.Driver;

namespace CurriculumATS.Persistence.Repositories;

public class ExperienciaProfissionalRepository : Repository<ExperienciaProfissional>, IExperienciaProfissionalRepository
{
    private readonly IMongoCollection<ExperienciaProfissional> _collection;

    public ExperienciaProfissionalRepository(IMongoDatabase database) : base(database, nameof(ExperienciaProfissional))
    {
        _collection = database.GetCollection<ExperienciaProfissional>(nameof(ExperienciaProfissional));
    }

    public async Task<List<ExperienciaProfissional>> GetByPessoaIdAsync(string pessoaId)
    {
        return await _collection
             .Find(r => r.PessoaId == pessoaId)
             .ToListAsync();
    }

    public async Task DeleteByPessoaIdAsync(string pessoaId)
    {
        var filter = Builders<ExperienciaProfissional>.Filter.Eq(r => r.PessoaId, pessoaId);
        await _collection.DeleteManyAsync(filter);
    }
}
