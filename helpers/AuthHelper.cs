using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Helpers
{
    public class AuthHelper
    {
        private readonly IConfiguration _config;

        public AuthHelper(IConfiguration config)
        {
            _config = config;
        }

        public byte[] GetPasswordHash(string password, byte[] passwordSalt)
        {

            string passwordSaltPlusSalt = _config.GetSection("AppSettings:PasswordKey").Value + Convert.ToBase64String(passwordSalt);

            byte[] passwordHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusSalt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000000,
                numBytesRequested: 256 / 8
            );

            return passwordHash;
        }

        public string CreateToken(int userId)
        {
            Claim[] claims = new Claim[]
            {
            new Claim("userId", userId.ToString())
            };

            SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:TokenKey").Value));


            SigningCredentials credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(1),

            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);




        }


    }




}