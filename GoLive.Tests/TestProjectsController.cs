using AutoMapper;
using GoLive.Contracts;
using GoLive.Controllers;
using GoLive.Helpers;
using GoLive.MockServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoLive.Tests
{
    [TestClass]
    public class TestProjectsController
    {
        private IProjectService _projectService;
        private IMapper _mapper;
        private ProjectsController _controller;

        [TestInitialize]
        public void SetUp(IProjectService userService, IMapper mapper)
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
            // TODO: need mock middleware
        }
    }
}