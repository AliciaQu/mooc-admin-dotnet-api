using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Application.Contracts.Dto
{
	public record RegistrationDto
	{
        private string userNmae;

        public RegistrationDto(int Id, string UserNmae, string Email, int PhoneNumber, string Gender, int Age, string PassWord)
        {
            this.Id = Id;
            userNmae = UserNmae;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.Gender = Gender;
            this.Age = Age;
            this.PassWord = PassWord;
        }

        public int Id { get; set; }
		public string UserName { get; set; } = "";
		public string Email { get; set; } = "";
		public int PhoneNumber { get; set; }
		public string Gender { get; set; } = "";
		public int Age { get; set; }
		public string PassWord { get; set; } = "";

	}

}