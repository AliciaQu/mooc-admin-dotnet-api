using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mooc.Model.Entity;

namespace Mooc.Application.Contracts.Dto
{
	public record RegistrationDto
	{

		public int Id { get; set; } = int.MaxValue;



		[Required]
		[StringLength(30, MinimumLength = 3)]
		public string UserName { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;


		[Required]
		public string Phone { get; set; }




		[Required]
		public Gender Gender { get; set; }
		[Required]

		public int Dob { get; set; }
		[Required]

		public string Password { get; set; }
	}


	public record RegisterOutputDto
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string UserName { get; set; }
		public string Phone { get; set; }
		public Gender Gender { get; set; }
		public int Dob { get; set; }





		//}
	}
}