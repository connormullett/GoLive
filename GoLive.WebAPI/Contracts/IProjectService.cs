using System;
using System.Collections.Generic;
using GoLive.Data;

namespace GoLive.Contracts
{
    public interface IProjectService
    {
        bool CreateProject(Project entity);
        Project GetProject(int id);
        IEnumerable<Project> GetAllProjects();
        bool Subscribe(Guid userId, int projectId);
        void Unsubscribe(Guid userId, int projectId);
    }
}