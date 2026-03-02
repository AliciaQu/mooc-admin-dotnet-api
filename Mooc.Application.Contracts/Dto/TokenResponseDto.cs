using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Application.Contracts.Dto
{
	internal record TokenResponseDto
	{
		  
        public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}

}
