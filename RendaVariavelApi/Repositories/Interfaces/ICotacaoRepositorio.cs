using RendaVariavelApi.Models;

namespace RendaVariavelApi.Repositories.Interfaces
{
    public interface ICotacaoRepositorio
    {
        Task<List<Cotacao>> GetAllCotacoes();
        Task<Cotacao> GetByTicker(string ticker);
        Task<Cotacao> Add(Cotacao cotacao);
        Task<List<Cotacao>> AddBulkCopy(List<Cotacao> cotacoes);
        Task<Cotacao> Update(Cotacao cotacao, string ticker);
        Task<bool> Delete(string ticker);
        Dictionary<string, string> ColumnsMapping();
    }
}