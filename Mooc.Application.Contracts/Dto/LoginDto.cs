using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Application.Contracts.Dto
{
    public record LoginDto
    {
        public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;


	}
}
