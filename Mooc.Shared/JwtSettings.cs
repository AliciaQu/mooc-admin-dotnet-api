using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Shared
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
