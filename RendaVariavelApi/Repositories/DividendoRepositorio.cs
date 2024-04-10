using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RendaVariavelApi.Data;
using RendaVariavelApi.Models;
using RendaVariavelApi.Repositories.Interfaces;
using System.Data;

namespace RendaVariavelApi.Repositories
{
    public class DividendoRepositorio : IDividendoRepositorio
    {
        private readonly RendaVariavelDbContext _dbContext;
        public DividendoRepositorio(RendaVariavelDbContext rendaVariavelDBContext)
        {
            _dbContext = rendaVariavelDBContext;
        }
        public async Task<List<Dividendo>> GetAllDividendos()
        {
            return await _dbContext.DIVIDENDOS.ToListAsync();
        }

        public async Task<Dividendo> GetByTicker(string ticker)
        {
            return await _dbContext.DIVIDENDOS.FirstAsync(x => x.Ticker == ticker);
        }
        public async Task<Dividendo> Add(Dividendo dividendo)
        {
            await _dbContext.DIVIDENDOS.AddAsync(dividendo);
            await _dbContext.SaveChangesAsync();
            return dividendo;
        }
        public async Task<Dividendo> Update(Dividendo dividendo, string ticker)
        {
            Dividendo dividendoByTicker = await GetByTicker(ticker);
            if (dividendoByTicker == null) { throw new NotImplementedException($"Dividendo: {ticker} não foi encontrado"); }

            dividendoByTicker.Valor = dividendo.Valor;
            dividendoByTicker.TipoDividendo = dividendo.TipoDividendo;
            dividendoByTicker.DataPagamento = dividendo.DataPagamento;
            dividendoByTicker.DataCom = dividendo.DataCom;

            _dbContext.DIVIDENDOS.Update(dividendoByTicker);
            await _dbContext.SaveChangesAsync();

            return dividendo;
        }
        public async Task<bool> Delete(string ticker)
        {
            Dividendo dividendoByTicker = await GetByTicker(ticker);
            if (dividendoByTicker == null) { throw new NotImplementedException($"Dividendo: {ticker} não foi encontrado"); }

            _dbContext.DIVIDENDOS.Remove(dividendoByTicker);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<Dividendo>> AddBulkCopy(List<Dividendo> dividendos)
        {
            using (var context = _dbContext)
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(context.Database.GetConnectionString()))
                {
                    //sqlBulkCopy.BatchSize = 10000;
                    sqlBulkCopy.BulkCopyTimeout = 300;

                    async Task CopyDataToDatabaseAsync(DataTable data, string tableName, Dictionary<string, string> columnMapping)
                    {
                        foreach (var item in columnMapping)
                            sqlBulkCopy.ColumnMappings.Add(item.Key, item.Value);

                        sqlBulkCopy.DestinationTableName = tableName;
                        await sqlBulkCopy.WriteToServerAsync(data);
                        sqlBulkCopy.ColumnMappings.Clear();
                    }

                    await CopyDataToDatabaseAsync(DividendosAsDataTable(dividendos), "DIVIDENDOS", ColumnsMapping());

                    return dividendos;
                }
            }
        }
        private DataTable DividendosAsDataTable(List<Dividendo> cotacoes)
        {
            return CollectionHelper.ConvertTo(cotacoes);
        }
        public Dictionary<string, string> ColumnsMapping()
        {
            return new Dictionary<string, string>() {
                 {"ticker", "TICKER"},
                 {"tipoDividendo", "TIPO_DIVIDENDO"},
                 {"valor", "VALOR"},
                 {"dataCom", "DATA_COM"},
                 {"dataPagamento", "DATA_PAGAMENTO"}
            };
        }             
    }
}
