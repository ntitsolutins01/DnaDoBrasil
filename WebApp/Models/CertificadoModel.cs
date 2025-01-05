using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class CertificadoModel
    {
        public CertificadoModel()
        {
            Certificados = new List<CertificadoDto>();
        }
        public CertificadoDto Certificado { get; set; }
        public List<CertificadoDto> Certificados { get; set; }
        public string TipoCursosId { get; set; }
        public SelectList ListTipoCursos { get; set; }
        public string CursoId { get; set; }
        public SelectList ListCursos { get; set; }
        public class CreateUpdateCertificadoCommand
        {
            public int Id { get; set; }
            public int CursoId { get; set; }
            public byte[] ImagemFrente { get; set; }
            public byte[]? ImagemVerso { get; set; }
            public string HtmlFrente { get; set; }
            public string HtmlVerso { get; set; }
            public bool Status { get; set; } = true;
        }
    }
}