using Dapper;
using System.Data.SqlClient;
using Test.Interfaces;
using Test.Models;

namespace Test.Services
{
    public class ClienteServices : IClientesService
    {
        public async Task<IEnumerable<ClientesModel>> GetClientesAsync()
        {
            IEnumerable<ClientesModel> clientes = new List<ClientesModel>();

            var cn = new Conexion();
            var sql = "SELECT * FROM Clientes";
            using(var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                await conexion.OpenAsync();
                clientes = await conexion.QueryAsync<ClientesModel>(sql);

            }
            return clientes;
        }

        public async Task<ClientesModel> GetClienteById(int id)
        {
            var cn = new Conexion();
            var sql = "SELECT * FROM Clientes WHERE ClienteID = @Id";
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                await conexion.OpenAsync();
                return await conexion.QueryFirstOrDefaultAsync<ClientesModel>(sql, new { Id = id });
            }
        }

    }
}
