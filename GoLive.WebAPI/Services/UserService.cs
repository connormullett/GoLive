using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using GoLive.Contracts;
using GoLive.Data;
using GoLive.Exceptions;
using GoLive.Helpers;
using GoLive.Models;
using GoLive.Models.Authenticate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GoLive.Services
{
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        private DataContext _context;
        private readonly PasswordHasher _hasher;

        public UserService(IOptions<AppSettings> appSettings, DataContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _hasher = new PasswordHasher();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == model.Username);

            if (user == null) throw new UserNotFoundException();

            if (!_hasher.ValidatePassword(user.PasswordHash, model.Password))
                throw new PasswordMismatchException();

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public string CreateUser(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return GenerateJwtToken(entity);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;   
        }

        public User GetById(Guid id)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == id);
            if (user == null) throw new UserNotFoundException();
            return user;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}