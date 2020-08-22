using System;
using System.Collections.Generic;
using System.Linq;
using GoLive.Contracts;
using GoLive.Data;
using GoLive.Exceptions;
using GoLive.Helpers;
using Microsoft.Extensions.Options;

namespace GoLive.Services
{
    public class ProjectService : IProjectService
    {

        private readonly AppSettings _appSettings;
        private DataContext _context;

        public ProjectService(IOptions<AppSettings> appSettings, DataContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public bool CreateProject(Project entity)
        {
            _context.Projects.Add(entity);
            return _context.SaveChanges() == 1;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _context.Projects;
        }

        public Project GetProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.ProjectId == id);
            if (project == null) throw new ProjectNotFoundException();
            else return project;
        }

        public bool Subscribe(Guid userId, int projectId)
        {
            var projectSub = new ProjectSubscription
            {
                UserId = userId,
                ProjectId = projectId
            };

            _context.ProjectSubscriptions.Add(projectSub);
            return _context.SaveChanges() == 1;
        }

        public void Unsubscribe(Guid userId, int projectId)
        {
            var entity = _context.ProjectSubscriptions.SingleOrDefault(
                x => x.UserId == userId && x.ProjectId == projectId
            );
            _context.ProjectSubscriptions.Remove(entity);
            _context.SaveChanges();
        }
    }
}