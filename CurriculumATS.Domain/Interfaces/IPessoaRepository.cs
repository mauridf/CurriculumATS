using CurriculumATS.Domain.Entities;

namespace CurriculumATS.Domain.Interfaces;

public interface IPessoaRepository : IRepository<Pessoa>
{
    Task<Pessoa> GetByEmailAsync(string email);
}
