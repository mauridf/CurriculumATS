using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using MongoDB.Driver;

namespace CurriculumATS.Persistence.Repositories;

public class CertificacoesCertificadosRepository : Repository<CertificacoesCertificados>, ICertificacoesCertificadosRepository
{
    private readonly IMongoCollection<CertificacoesCertificados> _collection;

    public CertificacoesCertificadosRepository(IMongoDatabase database) : base(database, nameof(CertificacoesCertificados))
    {
        _collection = database.GetCollection<CertificacoesCertificados>(nameof(CertificacoesCertificados));
    }

    public async Task<List<CertificacoesCertificados>> GetByPessoaIdAsync(string pessoaId)
    {
        return await _collection
             .Find(r => r.PessoaId == pessoaId)
             .ToListAsync();
    }

    public async Task DeleteByPessoaIdAsync(string pessoaId)
    {
        var filter = Builders<CertificacoesCertificados>.Filter.Eq(r => r.PessoaId, pessoaId);
        await _collection.DeleteManyAsync(filter);
    }
}
