using AutoMapper;
using GoLive.Contracts;
using GoLive.Controllers;
using GoLive.Helpers;
using GoLive.MockServices;
using GoLive.Models.Authenticate;
using GoLive.Models.UserDtos;
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
        public void TestValidCreateUserRequestShouldReturnAuthenticateResponse()
        {
            var user = new UserCreate
            {
                UserName = "test",
                Password = "Test1!",
                ConfirmPassword = "Test1!"
            };
            var response = _controller.CreateUser(user);
            Assert.IsInstanceOfType(response, typeof(AuthenticateResponse));
        }

        [TestMethod]
        public void TestPasswordMismatchCreateUserRequestShouldReturnBadRequest()
        {

        }

        [TestMethod]
        public void TestInvalidPasswordRequirementsShouldReturnBadRequest()
        {

        }
    }
}
