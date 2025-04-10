using CurriculumATS.Domain.Entities;

namespace CurriculumATS.Domain.Interfaces;

public interface IHabilidadeRepository : IRepository<Habilidades>
{
    Task<List<Habilidades>> GetByPessoaIdAsync(string pessoaId);
    Task DeleteByPessoaIdAsync(string pessoaId);
}
