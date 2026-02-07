using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Model.User
{
    public class User
    {
		public int Id { get; set; };
	
		public string UserName { get; set; } = string.Empty;

	
		public string Email { get; set; } = string.Empty;


		


		public long Phonenumber { get; set; }
		

		public string Gender { get; set; }
		

		public int Age { get; set; }
	

		public string Password { get; set; }
        public string PassWord { get; set; }
    }
}
