using applicationChallenge.Data;
using applicationChallenge.Helpers;
using applicationChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace applicationChallenge.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ShopContext _shopContext;

        public UserService(IOptions<AppSettings> appSettings, ShopContext shopContext)
        {
            _appSettings = appSettings.Value;
            _shopContext = shopContext;
        }

        public User Authenticate(string email, string password)
        {
            var user = _shopContext.Users.SingleOrDefault(x => x.Email == email && x.Password == password);

            // return null if no user found
            if (user == null)
                return null;

            // auth succesful -> generate JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim("Email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);


            user.Password = null;

            return user;
        }
    }
}
