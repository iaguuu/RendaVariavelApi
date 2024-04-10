using RendaVariavelApi.Models;

namespace RendaVariavelApi.Repositories.Interfaces
{
    public interface IFundoImobiliarioRepositorio
    {
        List<FundoImobiliario> GetAllFundosImobiliarios();
        FundoImobiliario GetByTicker(string ticker);
        void Add(FundoImobiliario fundoImobiliario);
        List<FundoImobiliario> AddBulkCopy(List<FundoImobiliario> fundosImobiliarios);
        FundoImobiliario Update(FundoImobiliario fundoImobiliario, string ticker);
        // Task<List<FundoImobiliario>> UpdateBulk(List<FundoImobiliario> fundosImobiliarios);
        void Delete(string ticker);
        bool Save();
        Dictionary<string, string> ColumnsMapping();
        List<FundoImobiliario> GetByList(List<string> tickersList);
    }
}