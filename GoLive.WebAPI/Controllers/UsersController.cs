using Microsoft.AspNetCore.Mvc;
using GoLive;
using GoLive.Contracts;
using GoLive.Models;
using AutoMapper;
using GoLive.Data;
using GoLive.Helpers;
using GoLive.Exceptions;
using System;
using System.Collections.Generic;
using GoLive.Models.UserDtos;
using GoLive.Models.Authenticate;

namespace GoLive.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly PasswordHasher _hasher;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _hasher = new PasswordHasher();
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]UserCreate model)
        {
            var user = _mapper.Map<User>(model);

            user.PasswordHash = _hasher.HashPassword(model.Password);

            if (_userService.CreateUser(user))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateRequest model)
        {
            try
            {
                var response = _userService.Authenticate(model);
                return Ok(response);
            }
            catch (PasswordMismatchException)
            {
                var response = new { message = "Password Mismatch" };
                return BadRequest(response);
            }
            catch (UserNotFoundException)
            {
                var response = new { message = "User not found" };
                return NotFound(response);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetById(Guid userId)
        {
            try
            {
                var entity = _userService.GetById(userId);
                var user = _mapper.Map<UserDto>(entity);
                return Ok(user);
            }
            catch (UserNotFoundException)
            {
                var response = new { message = "Couldn't find user" };
                return NotFound(response);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _userService.GetAll();
            var users = _mapper.Map<IEnumerable<UserListDto>>(entities);
            var response = new { data = users };
            return Ok(response);
        }
    }
}