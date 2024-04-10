using RendaVariavelApi.Models;

namespace RendaVariavelApi.Repositories.Interfaces
{
    public interface IDividendoRepositorio
    {
        Task<List<Dividendo>> GetAllDividendos();
        Task<Dividendo> GetByTicker(string ticker);
        Task<Dividendo> Add(Dividendo dividendo);
        Task<List<Dividendo>> AddBulkCopy(List<Dividendo> dividendo);
        Task<Dividendo> Update(Dividendo dividendo, string ticker);
        Task<bool> Delete(string ticker);
        Dictionary<string, string> ColumnsMapping();
    }
}