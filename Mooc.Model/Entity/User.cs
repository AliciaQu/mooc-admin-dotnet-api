using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Model.Entity
{
    public class User
    {
		public int Id {  get; set; }
		public string UserName {  get; set; } = String.Empty;
		 public string Email {  get; set; } =String.Empty;
		public long PhoneNumber {  get; set; }
		public string Gender {  get; set; }
		public int Age {  get; set; }
		public string PassWord {  get; set; }


	}
}
