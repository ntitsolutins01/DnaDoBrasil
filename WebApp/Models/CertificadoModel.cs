using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
	public class CertificadoModel
	{
		public CertificadoDto Certificado { get; set; }
		public List<CertificadoDto> MetricasImc { get; set; }

		public class CreateUpdateCertificadoCommand
		{
			public int Id { get; set; }
			public string? NomeCertificado { get; set; }
			public bool Status { get; set; } = true;
		}
	}

}
