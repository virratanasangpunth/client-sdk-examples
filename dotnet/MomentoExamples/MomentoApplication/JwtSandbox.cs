using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace MomentoApplication
{
    public class JwtSandbox
    {
        const string JWK_STRING = "{\"p\":\"76Y6AUAMsxeHogy9A9F5VTo9M_gR0Q2XFuQy0H-VXVYvPb-AR1h2cQJHF5UI3B0qKqY7mwKPjrDrcurojm-Uujj4JfBRU7Ls1BXdmSGsOgstYMGUcM8D9uu7z3WySlcm5lpuV_tLxo4iBeWeSIXIIKOEK86nKygVjmJnV081CCU\",\"kty\":\"RSA\",\"q\":\"221FTADZJJqtZRuxqcBSUYji8KHRzJeMXNKInlWWal0AY7_2Wq838ue2KCt6zCkO7kKKzjVASnVRNqtYdD4ng84vr8EfkGXMqSde9wSM7HWQMpg7odZBxBrAc4vkE5N9bzJPDMjpDuj9isvfWu_2zNIL3OeqDhbtueH3wmWYpWs\",\"d\":\"Rcb0uDZLx9L02B0GNK1SfW0hk4d-Y1cWhKzJcWHvskZ7Oy0QXuOfcKAtqQRVEuE2v3_LEr7PH4FNIT106jhFKGmTIKWWW5DfntSKJ3F3z2xi4zdtdHvnJH_VbJGtQToU7z2xEuxSSAczC8hqqibXZ7IzNA68ZbsQuvdA58dziGqKPgi1kp45lh_2H23zSmmJKws7sAVJBgeEv7WJ08y3vKb8Wu724WU3Rr89UC7AYalhftFqtfI7TZlf-QO0RrBd283atc3cFTAl275oiCmB6iGYEOtvZ5bM3-AMjGZsaK2rVyQUrDSJjr9nrN_3Xin13RncRN53M3TdSUh0d4RkZQ\",\"e\":\"AQAB\",\"use\":\"sig\",\"kid\":\"someid\",\"qi\":\"sxvARWx5OW4mnw0EpfGvOkF6cqiTqlkchYsEjGMrFTxiIcsgPXP59czWKgc2c4rnXu9gKk3--LE4zJ0fG9SwbNSAcrNUG_hcVMMOamI1ZV8bjZ2gw8MGV8pAsCvE1uUISnhYWEWIzy77AqYrMiFYELfw_9sX_LqovdDguGMbahM\",\"dp\":\"Uh9KXUdPkseLaRKoaviLdZNBid-Ga3cWwXdtmlk-G__-rFBFay17WPqI1iHyKUutOXRBlLrp6uHhrT2md7jw5GfFUWrq053K3lqn12Pcmv9di3yKm6W1HBZoA423jRYUdHekvG2W5_Go4xBujzeY_uZJ0qE8U-Omj2ZVN2wjJik\",\"alg\":\"RS256\",\"dq\":\"1UXg692LMOPSkwqW5UpT-IHXKlHuTDAksnfDaDoCCAG7Bjknpr7V35fN2j9gjJ1_sJ8msQTbJ0PeGwfx4pIi8B5xBR_toPkBMoxc5jUbEB24eSS0GTnaFFR4KBaZKVnaukCoyuFgfSiCfuxnfMjuFeg2tZDKHgW1LE3w9ZawBl8\",\"n\":\"zWl-RLgZ92spnzuqw8ptZmyk4ey86vHcM00e1kG1BDR5-qgCTJ5qiuTZGEfHXnxze5T48KFJIevw9x-nOQg-VVIXy1eSZz36ExQ1p6XKwfVjzWhGvea1hNIcmmCaNs8RM2EzQ7qU-cF36zQOW4QgP_QqjMKvm-e4FaoK1SQX_FniZy6M14RoJBzTOIJNySOTLJ-aME21IUW6e3OZHnYfX6_-_DFUw_sktV6J0TNHaP_foOYHPOTU0Wdqyev0XGi_WXUgYcmAkPB9fycezYxF38rsMYf_9bGwo0rz1XtmLg0HSkGsCp-3PjmcI2FLvvEU2yYVxajsOVDUZEpIuGJAdw\"}";

        public static JsonWebKey jwkToSecurityKey(string jwkString)
        {
            return new JsonWebKey(jwkString);
        }

        public static SecurityKey randoRsaSecurityKey()
        {
            RSAParameters privateKey;
            using (var csp = new RSACryptoServiceProvider()
            {
                PersistKeyInCsp = false
            })
            {
                privateKey = csp.ExportParameters(includePrivateParameters: true);
            }
            return new RsaSecurityKey(privateKey);
        }

        static void Main(string[] args)
        {
            var securityKey = jwkToSecurityKey(JWK_STRING);
            var credentials = new SigningCredentials(securityKey, securityKey.Alg);
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