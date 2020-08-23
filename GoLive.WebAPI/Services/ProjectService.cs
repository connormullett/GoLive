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

        public void AddOwner(Guid userId, int projectId)
        {
            var projectOwn = new ProjectOwner
            {
                UserId = userId,
                ProjectId = projectId
            };
            _context.ProjectOwners.Add(projectOwn);
            _context.SaveChanges();
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

        public IEnumerable<User> GetOwners(int projectId)
        {
            var owners = _context.Users.Where(x => x.OwnedProjects
                .Any(o => o.ProjectId == projectId));
            return owners.ToArray();
        }

        public Project GetProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.ProjectId == id);
            if (project == null) throw new ProjectNotFoundException();
            else return project;
        }

        public void RemoveOwner(Guid userId, int projectId)
        {
            var entity = _context.ProjectOwners.SingleOrDefault(
                x => x.UserId == userId && x.ProjectId == projectId
            );
            _context.ProjectOwners.Remove(entity);
            _context.SaveChanges();
        }

        public void Subscribe(Guid userId, int projectId)
        {
            var projectSub = new ProjectSubscription
            {
                UserId = userId,
                ProjectId = projectId
            };

            _context.ProjectSubscriptions.Add(projectSub);
            _context.SaveChanges();
        }

        public void Unsubscribe(Guid userId, int projectId)
        {
            var entity = _context.ProjectSubscriptions.SingleOrDefault(
                x => x.UserId == userId && x.ProjectId == projectId
            );
            _context.ProjectSubscriptions.Remove(entity);
            _context.SaveChanges();
        }

        public void UpdateProject(int projectId, Project model)
        {
            var entity = _context.Projects.SingleOrDefault(x => x.ProjectId == model.ProjectId);

            if (model.ProjectName != null)
                entity.ProjectName = model.ProjectName;
            if (model.ProjectDescription != null)
                entity.ProjectDescription = model.ProjectDescription;
            if (model.ProjectExternalUrl != null)
                entity.ProjectExternalUrl = model.ProjectExternalUrl;
            
            _context.SaveChanges();
        }
    }
}