using CurriculumATS.Domain.Entities;

namespace CurriculumATS.Domain.Interfaces;

public interface IExperienciaProfissionalRepository : IRepository<ExperienciaProfissional>
{
    Task<List<ExperienciaProfissional>> GetByPessoaIdAsync(string pessoaId);
    Task DeleteByPessoaIdAsync(string pessoaId);
}
