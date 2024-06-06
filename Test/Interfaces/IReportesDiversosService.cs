using Test.Models;

namespace Test.Interfaces
{
    public interface IReportesDiversosService
    {
        Task<IEnumerable<ReportesDiversosModel>> GetReportesDiversosAsync();

        Task<ReportesDiversosModel> GetReporteById(int id);
    }
}
