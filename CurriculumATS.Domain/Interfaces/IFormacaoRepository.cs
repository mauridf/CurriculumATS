using CurriculumATS.Domain.Entities;

namespace CurriculumATS.Domain.Interfaces;

public interface IFormacaoRepository : IRepository<Formacao>
{
    Task<List<Formacao>> GetByPessoaIdAsync(string pessoaId);
    Task DeleteByPessoaIdAsync(string pessoaId);
}
