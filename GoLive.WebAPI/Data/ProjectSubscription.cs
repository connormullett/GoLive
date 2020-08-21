using System;

namespace GoLive.Data
{
    public class ProjectSubscription
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}