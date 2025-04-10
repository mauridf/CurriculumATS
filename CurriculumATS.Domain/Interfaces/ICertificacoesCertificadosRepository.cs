using CurriculumATS.Domain.Entities;

namespace CurriculumATS.Domain.Interfaces;

public interface ICertificacoesCertificadosRepository : IRepository<CertificacoesCertificados>
{
    Task<List<CertificacoesCertificados>> GetByPessoaIdAsync(string pessoaId);
    Task DeleteByPessoaIdAsync(string pessoaId);
}
