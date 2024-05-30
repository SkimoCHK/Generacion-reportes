using Test.Models;

namespace Test.Interfaces
{
    public interface IClientesService
    {
        Task<IEnumerable<ClientesModel>> GetClientesAsync();

        Task<ClientesModel> GetClienteById(int id);

    }
}
