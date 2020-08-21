using System;
using System.Collections.Generic;

namespace GoLive.Data
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectExternalUrl { get; set; }
        public Guid ProjectCreatorId { get; set; }
        public User ProjectCreator { get; set; }

        public ICollection<ProjectSubscription> Subscribers { get; set; }
        public ICollection<ProjectOwner> Owners { get; set; }
    }
}