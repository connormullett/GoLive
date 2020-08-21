using System;

namespace GoLive.Models.ProjectDtos
{
    public class ProjectListDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Guid ProjectCreatorId { get; set; }
    }
}