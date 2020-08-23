using System;
using System.Collections.Generic;
using GoLive.Contracts;
using GoLive.Data;

namespace GoLive.MockServices
{
    public class MockProjectService : IProjectService
    {
        public void AddOwner(Guid userId, int projectId)
        {
            
        }

        public bool CreateProject(Project entity)
        {
            return true;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            var project = new Project();
            var projectList = new List<Project>() { project };
            return projectList;
        }

        public Project GetProject(int id)
        {
            return new Project();
        }

        public void RemoveOwner(Guid userId, int projectId)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(Guid userId, int projectId)
        {
            
        }

        public void Unsubscribe(Guid userId, int projectId)
        {
            
        }
    }
}