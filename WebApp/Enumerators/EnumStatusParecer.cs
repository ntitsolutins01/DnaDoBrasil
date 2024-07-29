using System.ComponentModel;

namespace Infraero.Relprev.CrossCutting.Enumerators
{
    public enum EnumStatusParecer
    {
        [Description("Ocorrência atendida")]
        Atendida = 0,
        [Description("Devolvido")]
        Devolvido = 1,
        [Description("Parecer aceito")]
        ParecerAaceito = 2,
        [Description("Parecer completado")]
        ParecerCompletado = 3

    }
}
