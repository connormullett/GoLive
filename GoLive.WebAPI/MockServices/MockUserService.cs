using System;
using System.Collections.Generic;
using GoLive.Contracts;
using GoLive.Data;
using GoLive.Models.Authenticate;

namespace GoLive.MockServices
{
    public class MockUserService : IUserService
    {
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateUser(User entity)
        {
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            var user = new User();
            var userList = new List<User>() { user };
            return userList;
        }

        public User GetById(Guid id)
        {
            return new User();
        }
    }
}