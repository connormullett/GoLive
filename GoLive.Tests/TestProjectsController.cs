using System;
using AutoMapper;
using GoLive.Contracts;
using GoLive.Controllers;
using GoLive.Data;
using GoLive.Helpers;
using GoLive.MockServices;
using GoLive.Models.ProjectDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoLive.Tests
{
    [TestClass]
    public class TestProjectsController
    {
        private IProjectService _projectService;
        private IMapper _mapper;
        private ProjectsController _controller;

        private void SetControllerHttpContext(User user)
        {
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Items["User"] = user;
        }

        [TestInitialize]
        public void SetUp()
        {
            var profile = new AutoMapperProfile();
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(profile);
            });
            _mapper = config.CreateMapper();
            _projectService = new MockProjectService();
            _controller = new ProjectsController(_projectService, _mapper);
        }

        [TestMethod]
        public void TestValidProjectCreateReturnsOk()
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = "testUser",
                Email = "valid@email.com"
            };
            var project = new ProjectCreate
            {
                ProjectName = "testName",
                ProjectDescription = "a test description",
                ProjectExternalUrl = "https://github.com/foo/bar"
            };
            SetControllerHttpContext(user);
            var actual = _controller.CreateProject(project);

            Assert.IsInstanceOfType(actual, typeof(OkResult));
        }

        [TestMethod]
        public void TestGetProjectsShouldReturnOk()
        {
            var actual = _controller.GetProjects();
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        }
    }
}