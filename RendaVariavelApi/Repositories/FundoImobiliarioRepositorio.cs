using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RendaVariavelApi.Data;
using RendaVariavelApi.Models;
using RendaVariavelApi.Repositories.Interfaces;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace RendaVariavelApi.Repositories
{
    public class FundoImobiliarioRepositorio : IFundoImobiliarioRepositorio
    {
        private readonly RendaVariavelDbContext _dbContext;
        public FundoImobiliarioRepositorio(RendaVariavelDbContext rendaVariavelDBContext)
        {
            _dbContext = rendaVariavelDBContext;
        }
        public  List<FundoImobiliario> GetAllFundosImobiliarios()
        {
            return _dbContext.FUNDOS_IMOBILIARIOS.ToList();
        }
        public FundoImobiliario GetByTicker(string ticker)
        {
            return _dbContext.FUNDOS_IMOBILIARIOS.FirstOrDefault(x => x.Ticker == ticker);
        }

        public void Add(FundoImobiliario fundoImobiliario)
        {
            _dbContext.FUNDOS_IMOBILIARIOS.Add(fundoImobiliario);
        }
        public bool Save() {
            return _dbContext.SaveChanges() >= 0;
        }
        public FundoImobiliario Update(FundoImobiliario fundoImobiliario, string ticker)
        {

            //Resgata o fundo para salvar
            FundoImobiliario fundoImobiliarioByTicker = GetByTicker(ticker);
            
            fundoImobiliarioByTicker.Ticker = fundoImobiliario.Ticker;
            fundoImobiliarioByTicker.Cnpj = fundoImobiliario.Cnpj;
            fundoImobiliarioByTicker.Segmento = fundoImobiliario.Segmento;
            fundoImobiliarioByTicker.PublicoAlvo = fundoImobiliario.PublicoAlvo;
            fundoImobiliarioByTicker.Mandato = fundoImobiliario.Mandato;
            fundoImobiliarioByTicker.TipoDeFundo = fundoImobiliario.TipoDeFundo;
            fundoImobiliarioByTicker.PrazoDeDuracao = fundoImobiliario.PrazoDeDuracao;
            fundoImobiliarioByTicker.TipoDeGestao = fundoImobiliario.TipoDeGestao;
            fundoImobiliarioByTicker.TaxaDeAdministracao = fundoImobiliario.TaxaDeAdministracao;
            fundoImobiliarioByTicker.NumeroDeCotistas = fundoImobiliario.NumeroDeCotistas;
            fundoImobiliarioByTicker.CotasEmitidas = fundoImobiliario.CotasEmitidas;
            fundoImobiliarioByTicker.ValorPatrimonialPorCota = fundoImobiliario.ValorPatrimonialPorCota;
            fundoImobiliarioByTicker.ValorPatrimonial = fundoImobiliario.ValorPatrimonial;
            fundoImobiliarioByTicker.UltimoRendimento = fundoImobiliario.UltimoRendimento;

            _dbContext.FUNDOS_IMOBILIARIOS.Update(fundoImobiliarioByTicker);
            return fundoImobiliarioByTicker;
        }
        public void Delete(string ticker)
        {
            FundoImobiliario fundoImobiliarioByTicker = GetByTicker(ticker);          
            _dbContext.FUNDOS_IMOBILIARIOS.Remove(fundoImobiliarioByTicker);
        }
        public List<FundoImobiliario> AddBulkCopy(List<FundoImobiliario> fundosImobiliarios)
        {
            using (var context = _dbContext)
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(context.Database.GetConnectionString()))
                {
                    //sqlBulkCopy.BatchSize = 10000;
                    sqlBulkCopy.BulkCopyTimeout = 300;

                    void CopyDataToDatabaseAsync(DataTable data, string tableName, Dictionary<string, string> columnMapping)
                    {
                        foreach (var item in columnMapping)
                            sqlBulkCopy.ColumnMappings.Add(item.Key, item.Value);

                        sqlBulkCopy.DestinationTableName = tableName;
                        sqlBulkCopy.WriteToServer(data);
                        sqlBulkCopy.ColumnMappings.Clear();
                    }

                    CopyDataToDatabaseAsync(CollectionHelper.ConvertTo(fundosImobiliarios), "FUNDOS_IMOBILIARIOS", ColumnsMapping());

                    return fundosImobiliarios;
                }
            }
        }
        public Dictionary<string, string> ColumnsMapping()
        {
            return new Dictionary<string, string>() {
                 {"Segmento", "SEGMENTO"},
                 {"PublicoAlvo", "PUBLICO_ALVO"},
                 {"Mandato", "MANDATO"},
                 {"TipoDeFundo", "TIPO_FUNDO"},
                 {"PrazoDeDuracao", "PRAZO_DURACAO"},
                 {"TipoDeGestao", "TIPO_GESTAO"},
                 {"TaxaDeAdministracao", "TAXA_ADMINISTRACAO"},
                 {"Vacancia", "VACANCIA"},
                 {"NumeroDeCotistas", "NUMERO_COTISTAS"},
                 {"CotasEmitidas", "COTAS_EMITIDAS"},
                 {"ValorPatrimonialPorCota", "VALOR_PATRIMONIAL_POR_COTA"},
                 {"ValorPatrimonial", "VALOR_PATRIMONIAL"},
                 {"UltimoRendimento", "ULTIMO_RENDIMENTO"},
                 {"Ticker", "TICKER"},
                 {"TipoInvestimento", "TIPO_INVESTIMENTO"},
                 {"Cnpj", "CNPJ"},
                 {"RazaoSocial", "RAZAO_SOCIAL"}
            };
        }
        public List<FundoImobiliario> GetByList(List<string> tickersList)
        {
            return _dbContext.FUNDOS_IMOBILIARIOS.Where(x => tickersList.Contains(x.Ticker)).ToList();
        }
        public async Task<List<FundoImobiliario>> UpdateBulk(List<FundoImobiliario> fundosImobiliarios)
        {
            List<string> tickerList = fundosImobiliarios.Select(x => x.Ticker).ToList();

            List<FundoImobiliario> fundosImobiliariosToUpdate = _dbContext.FUNDOS_IMOBILIARIOS.Where(x => tickerList.Contains(x.Ticker)).ToList();

            return fundosImobiliariosToUpdate;

            Dictionary<string, FundoImobiliario> oldValuesMap = fundosImobiliariosToUpdate.ToDictionary(x => x.Ticker);

            foreach (FundoImobiliario newValues in fundosImobiliarios)
            {
                if (oldValuesMap.TryGetValue(newValues.Ticker, out FundoImobiliario? classOld))
                {
                    classOld.Cnpj = newValues.Cnpj;
                    classOld.Segmento = newValues.Segmento;
                    classOld.PublicoAlvo = newValues.PublicoAlvo;
                    classOld.Mandato = newValues.Mandato;
                    classOld.TipoDeFundo = newValues.TipoDeFundo;
                    classOld.PrazoDeDuracao = newValues.PrazoDeDuracao;
                    classOld.TipoDeGestao = newValues.TipoDeGestao;
                    classOld.TaxaDeAdministracao = newValues.TaxaDeAdministracao;
                    classOld.NumeroDeCotistas = newValues.NumeroDeCotistas;
                    classOld.CotasEmitidas = newValues.CotasEmitidas;
                    classOld.ValorPatrimonialPorCota = newValues.ValorPatrimonialPorCota;
                    classOld.ValorPatrimonial = newValues.ValorPatrimonial;
                    classOld.UltimoRendimento = newValues.UltimoRendimento;
                }
            }

            _dbContext.FUNDOS_IMOBILIARIOS.UpdateRange(fundosImobiliariosToUpdate);
            await _dbContext.SaveChangesAsync();

            return fundosImobiliariosToUpdate;
        }

        
    }
}
