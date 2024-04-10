using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RendaVariavelApi.Data;
using RendaVariavelApi.Models;
using RendaVariavelApi.Repositories.Interfaces;
using System.Data;

namespace RendaVariavelApi.Repositories
{
    public class CotacaoRepositorio : ICotacaoRepositorio
    {
        private readonly RendaVariavelDbContext _dbContext;
        public CotacaoRepositorio(RendaVariavelDbContext rendaVariavelDBContext)
        {
            _dbContext = rendaVariavelDBContext;
        }
        public async Task<List<Cotacao>> GetAllCotacoes()
        {
            return await _dbContext.COTACOES.ToListAsync();
        }
        public async Task<Cotacao> GetByTicker(string ticker)
        {
            return await _dbContext.COTACOES.FirstAsync(x => x.Ticker == ticker);
        }
        public async Task<Cotacao> Add(Cotacao cotacao)
        {
            await _dbContext.COTACOES.AddAsync(cotacao);
            await _dbContext.SaveChangesAsync();
            return cotacao;
        }
        public async Task<Cotacao> Update(Cotacao cotacao, string ticker)
        {
            Cotacao cotacoesByTicker = await GetByTicker(ticker);
            if (cotacoesByTicker == null) { throw new NotImplementedException($"Cotacao: {ticker} não foi encontrado"); }

            cotacoesByTicker.Valor = cotacao.Valor;
            cotacoesByTicker.Data = cotacao.Data;

            _dbContext.COTACOES.Update(cotacoesByTicker);
            await _dbContext.SaveChangesAsync();

            return cotacao;
        }
        public async Task<bool> Delete(string ticker)
        {
            Cotacao cotacoesByTicker = await GetByTicker(ticker);
            if (cotacoesByTicker == null) { throw new NotImplementedException($"Cotacao: {ticker} não foi encontrado"); }

            _dbContext.COTACOES.Remove(cotacoesByTicker);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<Cotacao>> AddBulkCopy(List<Cotacao> cotacoes)
        {
            using (var context = _dbContext)
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(context.Database.GetConnectionString()))
                {
                    sqlBulkCopy.BulkCopyTimeout = 300;

                    async Task CopyDataToDatabaseAsync(DataTable data, string tableName, Dictionary<string, string> columnMapping)
                    {
                        foreach (var item in columnMapping)
                            sqlBulkCopy.ColumnMappings.Add(item.Key, item.Value);

                        sqlBulkCopy.DestinationTableName = tableName;
                        await sqlBulkCopy.WriteToServerAsync(data);
                        sqlBulkCopy.ColumnMappings.Clear();
                    }

                     await CopyDataToDatabaseAsync(CollectionHelper.ConvertTo(cotacoes), "COTACOES", ColumnsMapping());

                    return cotacoes;
                }
            }
        }
        public Dictionary<string, string> ColumnsMapping()
        {
            return new Dictionary<string, string>() {
                 {"ticker", "TICKER"},
                 {"valor", "VALOR"},
                 {"data", "DATA_COTACAO"}
            };
        }
    }
}
