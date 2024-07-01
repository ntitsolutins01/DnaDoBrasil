using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
	public class AulaModel
	{
		
        

        public class CreateUpdateAulaCommand
		{
			public int Id { get; set; }
           
            public bool Status { get; set; } = true;
           
        }
	}

}
