using Microsoft.AspNetCore.Mvc;
using RendaVariavelApi.Models;
using RendaVariavelApi.Repositories.Interfaces;
using System.Net;

namespace RendaVariavelApi.Controllers
{
    [Route("api/v1/[controller]")]
    //[Route("api/v1/[controller]")]
    [ApiController]
    public class FundosImobiliariosController : ControllerBase
    {
        private readonly IFundoImobiliarioRepositorio _fundosImobiliariosRepositorio;
        public FundosImobiliariosController(IFundoImobiliarioRepositorio fundosImobiliariosRepositorio)
        {
            _fundosImobiliariosRepositorio = fundosImobiliariosRepositorio;
        }

        [HttpGet]
        public ActionResult<List<FundoImobiliario>> GetAllFundosImobiliarios()
        {
            List<FundoImobiliario> fundosImobiliarios = _fundosImobiliariosRepositorio.GetAllFundosImobiliarios();
            return Ok(new Result<List<FundoImobiliario>> { title = "Sucess", detail = $"Todos os fundos", status = 200, result = fundosImobiliarios });
        }

        [HttpGet("{ticker}")]
        public ActionResult GetByTicker(string ticker)
        {

            if (!ModelState.IsValid)
            {
                return Problem($"Bad request", statusCode: (int)HttpStatusCode.BadRequest);
            }

            FundoImobiliario fundoImobiliario = _fundosImobiliariosRepositorio.GetByTicker(ticker);

            if (fundoImobiliario is null)
            {
                return Problem($"Ticker {ticker} não encontrado", statusCode: (int)HttpStatusCode.NotFound);
            }

            return Ok(new Result<FundoImobiliario> { title = "Sucess", detail = $"Fundo {fundoImobiliario.Ticker}", status = 200, result = fundoImobiliario });
        }

        [HttpPost]
        public ActionResult<FundoImobiliario> Add([FromBody] FundoImobiliario fundoImobiliario)
        {
            if (!ModelState.IsValid)
            {
                return Problem($"Bad request", statusCode: (int)HttpStatusCode.BadRequest);
            }

            FundoImobiliario fundo = _fundosImobiliariosRepositorio.GetByTicker(fundoImobiliario.Ticker);

            if (fundo is not null)
            {
                return Problem($"Fundo imobiliario {fundoImobiliario.Ticker} já cadastrado", statusCode: (int)HttpStatusCode.Conflict);
            }

            _fundosImobiliariosRepositorio.Add(fundoImobiliario);

            if (!_fundosImobiliariosRepositorio.Save())
            {
                return Problem($"Erro ao cadastrar {fundoImobiliario.Ticker}", statusCode: (int)HttpStatusCode.InternalServerError);
            }

            return Ok(new Result<FundoImobiliario> { title = "Sucess", detail = $"Fundo {fundoImobiliario.Ticker} criado", status = 200, result = fundoImobiliario });
        }

        [HttpPut("{ticker}")]
        public ActionResult<FundoImobiliario> Upadate([FromBody] FundoImobiliario fundoImobiliario, string ticker)
        {
            if (!ModelState.IsValid)
            {
                return Problem($"Bad request", statusCode: (int)HttpStatusCode.BadRequest);
            }

            FundoImobiliario fundoOldTicker = _fundosImobiliariosRepositorio.GetByTicker(ticker);

            if (fundoOldTicker is null)
            {
                return Problem($"Ticker {ticker} não encontrado", statusCode: (int)HttpStatusCode.NotFound); 
            }

            //VERIFICA SE O TICKER DO FUNDO QUE VAI SER ATUALIZADO JA EXISTE E É DIFERENTE DO TICKER BUSCADO
            FundoImobiliario fundoNewTicker = _fundosImobiliariosRepositorio.GetByTicker(fundoImobiliario.Ticker);

            if (fundoNewTicker is not null && ticker != fundoImobiliario.Ticker)
            {
                return Problem($"Não é possível alterar o ticker {ticker} para {fundoImobiliario.Ticker}, pois {fundoImobiliario.Ticker} já está cadastrado", statusCode: (int)HttpStatusCode.Conflict);
            }

            _fundosImobiliariosRepositorio.Update(fundoImobiliario, ticker);

            if (!_fundosImobiliariosRepositorio.Save())
            {
                return Problem($"Erro ao salvar alterações do {fundoImobiliario.Ticker}", statusCode: (int)HttpStatusCode.InternalServerError);
            }

            return Ok(new Result<FundoImobiliario>  { title = "Sucess", detail = $"{ticker} atualizado", status = 200, result = fundoImobiliario });
        }

        [HttpDelete("{ticker}")]
        public ActionResult<FundoImobiliario> Delete(string ticker)
        {
            FundoImobiliario fundo = _fundosImobiliariosRepositorio.GetByTicker(ticker);

            if (fundo is null)
            {
                return Problem($"Ticker {ticker} não encontrado", statusCode: (int)HttpStatusCode.NotFound); // return NotFound();
            }

            _fundosImobiliariosRepositorio.Delete(ticker);

            if (!_fundosImobiliariosRepositorio.Save())
            {
                return Problem($"Erro ao deletar {ticker}", statusCode: (int)HttpStatusCode.InternalServerError);
            }

            return Ok(new Result<bool> { title = "Sucess", detail = $"{ticker} deletado", status = 200, result = true });
        }

        [HttpPost("BulkCopy")]
        public ActionResult<FundoImobiliario> AddBulkCopy([FromBody] List<FundoImobiliario> fundosImobiliarios)
        {
            List<string> tickerList = fundosImobiliarios.Select(x => x.Ticker).ToList();
            List<FundoImobiliario> alreadyInserted = _fundosImobiliariosRepositorio.GetByList(tickerList);
            List<FundoImobiliario> toInsert = fundosImobiliarios.Where(x => !alreadyInserted.Any(y => y.Ticker == x.Ticker)).ToList();
            _fundosImobiliariosRepositorio.AddBulkCopy(toInsert);
            return Ok(new Result<List<FundoImobiliario>> { title = "Sucess", detail = $"Adicionados", status = 200, result = toInsert });
        }

    }
}
