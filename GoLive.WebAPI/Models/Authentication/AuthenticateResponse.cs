using System;
using GoLive.Data;

namespace GoLive.Models.Authenticate
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.UserId;
            Username = user.UserName;
            Token = token;
        }
    }
}