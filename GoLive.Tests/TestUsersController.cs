using System;
using System.Threading.Tasks;
using AutoMapper;
using GoLive.Contracts;
using GoLive.Controllers;
using GoLive.Helpers;
using GoLive.MockServices;
using GoLive.Models.Authenticate;
using GoLive.Models.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoLive.Tests
{
    [TestClass]
    public class UsersControllerTests
    {
        private IUserService _userService;
        private IMapper _mapper;
        private UsersController _controller;

        [TestInitialize]
        public void SetUp()
        {
            var profile = new AutoMapperProfile();
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(profile);
            });
            _mapper = config.CreateMapper();
            _userService = new MockUserService();
            _controller = new UsersController(_userService, _mapper);
        }

        [TestMethod]
        public void TestValidCreateUserRequestShouldReturnOkResponse()
        {
            var user = new UserCreate
            {
                UserName = "testUser",
                Password = "Tester1!",
                ConfirmPassword = "Tester1!",
                Email = "connor@connor.com"
            };
            var actual = _controller.CreateUser(user);
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        }

        [TestMethod]
        public void TestPasswordMismatchCreateUserRequestShouldReturnBadRequest()
        {
            var user = new UserCreate
            {
                UserName = "testUser",
                Password = "Test1!",
                Email = "connor@connor.com",
                ConfirmPassword = "Test!1"
            };
            var actual = _controller.CreateUser(user);
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void TestInvalidPasswordRequirementsShouldReturnBadRequest()
        {
            var user = new UserCreate
            {
                UserName = "testUser",
                Password = "foo",
                Email = "connor@connor.com",
                ConfirmPassword = "foo"
            };
            var actual = _controller.CreateUser(user);
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void TestWeakPasswordShouldReturnBadRequest()
        {
            var user = new UserCreate
            {
                UserName = "testUser",
                Password = "Tes!",
                ConfirmPassword = "Tes!",
                Email = "connor@connor.com"
            };
            var actual = _controller.CreateUser(user);
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void TestInvalidEmailShouldReturnBadRequest()
        {
            var user = new UserCreate
            {
                UserName = "testUser",
                Password = "Test1!",
                ConfirmPassword = "Test1!",
                Email = "invalid"
            };
            var actual = _controller.CreateUser(user);
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void TestValidAuthenticateRequestShouldReturnOk()
        {
            var auth = new AuthenticateRequest
            {
                Username = "test",
                Password = "password"
            };
            var actual = _controller.Authenticate(auth);
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        }
    }
}
