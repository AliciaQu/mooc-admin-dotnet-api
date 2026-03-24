using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Mooc.Model.Entity
	{
		public class RefreshToken
		{
			public int Id { get; set; }
			public string Token { get; set; }
			public DateTime ExpiryDate { get; set; }
			public bool IsUsed { get; set; }
			public int UserId { get; set; }
			public User User { get; set; }  // navigation property to your existing User
		}
	
}

