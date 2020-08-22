using AutoMapper;
using GoLive.Data;
using GoLive.Models.ProjectDtos;
using GoLive.Models.UserDtos;

namespace GoLive.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserCreate>();
            CreateMap<UserCreate, User>();
            CreateMap<User, UserListDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserUpdate, User>();

            CreateMap<Project, ProjectListDto>();
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectCreate, Project>();
        }
    }
}