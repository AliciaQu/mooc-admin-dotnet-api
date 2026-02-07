using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Application.Contracts.Dto
{
	public record RegistrationDto
	{
		
		public int Id { get; set; }=int.MaxValue;
		[Required]
		[StringLength(30,MinimumLength =3)]
		public string UserName { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;


		[Required]
	
		
		public long Phonenumber { get; set; }
		[Required]
		
		public string Gender { get; set; }
		[Required]

		public int Age { get; set; }
		[Required]
		
		public string Password { get; set; }
	}
}