using System;
using System.Collections.Generic;
using GoLive.Data;
using GoLive.Models.Authenticate;
using GoLive.Models.UserDtos;

namespace GoLive.Contracts
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        Data.User GetById(Guid id);
        string CreateUser(User entity);
        bool UpdateUser(Guid userId, User user);
    }
}