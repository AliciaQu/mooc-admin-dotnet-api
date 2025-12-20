using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mooc.Core.Utils;

public static class JWTUtil
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="secret"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="enAlgorithm"></param>
    /// <param name="expireSeconds"></param>
    /// <param name="playBody"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public  static string GenerateToken(string secret ,string issuer,string audience,string enAlgorithm,int expireSeconds, Dictionary<string,string> playBody)
    {
        var claims=new List<Claim>();

        foreach (var item in playBody)
        {
            claims.Add(new Claim(item.Key, item.Value));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        SigningCredentials creds ;
        if (enAlgorithm == "HS256")
            creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        else if (enAlgorithm == "ES256")
            creds = new SigningCredentials(key, SecurityAlgorithms.EcdsaSha256);
        else
            throw new NotSupportedException(enAlgorithm);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTimeOffset.Now.LocalDateTime.AddSeconds(expireSeconds),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}
