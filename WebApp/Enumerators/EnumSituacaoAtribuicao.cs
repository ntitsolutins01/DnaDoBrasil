using System.ComponentModel;

namespace Infraero.Relprev.CrossCutting.Enumerators
{
    public enum EnumSituacaoAtribuicao
    {
        [Description("Ocorrência Atribuída")]
        OcorrenciaAtribuida = 0,
        [Description("Ocorrência Removida")]
        OcorrenciaRemovida = 1,
        //[Description("Parecer Não Respondido")]
        //NaoRespondido = 2,
        //[Description("Parecer Técnico Recusado")]
        //Recusado = 3,
        //[Description("Parecer Técnico Aceito")]
        //Aceito = 4
    }
}
