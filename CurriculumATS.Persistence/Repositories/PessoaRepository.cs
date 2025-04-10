using CurriculumATS.Domain.Entities;
using CurriculumATS.Domain.Interfaces;
using MongoDB.Driver;

namespace CurriculumATS.Persistence.Repositories;

public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
{
    private readonly IMongoCollection<Pessoa> _collection;

    public PessoaRepository(IMongoDatabase database) : base(database, "Pessoas")
    {
        _collection = database.GetCollection<Pessoa>("Pessoas");
    }

    public async Task<Pessoa> GetByEmailAsync(string email) => await _collection.Find(p => p.Email == email).FirstOrDefaultAsync();
}
