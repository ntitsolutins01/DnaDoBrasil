using System.ComponentModel;

namespace WebApp.Enumerators
{
    public enum EnumPerfil
    {
        [Description("Parceiro")]
        Parceiro = 8,
        [Description("Aluno")]
        Aluno = 6,
        [Description("Profissional")]
        Profissional = 7,
        [Description("Professor")]
        Professor = 13,
        [Description("Coordenador")]
        Coordenador = 14
    }
}
