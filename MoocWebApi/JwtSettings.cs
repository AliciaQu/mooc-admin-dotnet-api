
	
namespace MoocWebApi
	{
		public class JwtSettings
		{
			public string Issuer { get; set; }
			public string Audience { get; set; }
			public int ExpireSeconds { get; set; }
			public string ENAlgorithm { get; set; }
			public string SecurityKey { get; set; }
		}
	}



