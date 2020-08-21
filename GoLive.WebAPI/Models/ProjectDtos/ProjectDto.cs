using System;

namespace GoLive.Models.ProjectDtos
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectExternalUrl { get; set; }
        public Guid ProjectCreatorId { get; set; }
    }
}