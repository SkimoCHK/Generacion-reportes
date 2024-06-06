using Dapper;
using System.Data.SqlClient;
using Test.Interfaces;
using Test.Models;

namespace Test.Services
{
    public class ReportesDiversosService : IReportesDiversosService
    {
        Task<ReportesDiversosModel> IReportesDiversosService.GetReporteById(int id)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<ReportesDiversosModel>> IReportesDiversosService.GetReportesDiversosAsync()
        {
            IEnumerable<ReportesDiversosModel> reportes = new List<ReportesDiversosModel>();
            var cn = new Conexion();
            var query = "SELECT * FROM dbo.ReportesDiversos";

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                await conexion.OpenAsync();
                reportes = await conexion.QueryAsync<ReportesDiversosModel>(query);

            }
            return reportes;

        }
    }
}
