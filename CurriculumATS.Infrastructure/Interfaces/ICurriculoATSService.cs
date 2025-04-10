using CurriculumATS.Infrastructure.DTOs;

namespace CurriculumATS.Infrastructure.Interfaces;

public interface ICurriculoATSService
{
    Task<CurriculoATSDto> MontarCurriculoAsync(string pessoaId);
}
