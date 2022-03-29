using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace MomentoApplication
{
    public class JwtSandbox
    {

        static void Main(string[] args)
        {
            RSAParameters privateKey;
            using (var csp = new RSACryptoServiceProvider()
            {
                PersistKeyInCsp = false
            })
            {
                privateKey = csp.ExportParameters(includePrivateParameters: true);
            }
            var securityKey = new RsaSecurityKey(privateKey);
            var credentials = new SigningCredentials(securityKey, "RS256");
            var JWTHeader = new JwtHeader(credentials);

            var payload = new JwtPayload
                {
                    { "foo", "bar"},
                };

            var jwtToken = new JwtSecurityToken(JWTHeader, payload);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            Console.WriteLine($"This is my JWT: {encodedJwt}");
        }
    }
}