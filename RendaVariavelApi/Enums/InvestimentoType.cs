using System.ComponentModel;

namespace RendaVariavelApi.Enums
{
    public enum InvestimentoType
    {
        [Description("FUNDO IMOBILIARIO")]
        FII,
        [Description("FUNDO DE INVESTIMENTO EM INFRAESTRUTURA ")]
        FII_INFRA,
        [Description("FUNDO DE INVESTIMENTO EM CADEIAS AGROINDÚSTRIAIS")]
        FII_AGRO,
        [Description("ACOES")]
        ACOES,
        [Description("BDRS")]
        BDRS,
        [Description("NAO DEFINIDO")]
        NAO_DEFINIDO
    }
}
