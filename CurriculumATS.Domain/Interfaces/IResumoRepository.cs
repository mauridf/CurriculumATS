using CurriculumATS.Domain.Entities;

namespace CurriculumATS.Domain.Interfaces;

public interface IResumoRepository : IRepository<Resumo>
{
    Task<Resumo> GetByPessoaIdAsync(string pessoaId);
    Task DeleteByPessoaIdAsync(string pessoaId);
}
