using System.ComponentModel;

namespace RendaVariavelApi.Enums
{
    public enum DividendoType
    {
        [Description("JUROS SOBRE CAPITAL PROPIO")]
        JCP = 1,
        [Description("DIVIDENDOS")]
        DIVIDENDOS = 2,
        [Description("AMORTIZACAO")]
        AMORTIZACAO = 3
    }
}
