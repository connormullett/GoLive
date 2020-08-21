using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GoLive.Data
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public ICollection<ProjectSubscription> SubscribedProjects { get; set; }
        public ICollection<ProjectOwner> OwnedProjects { get; set; }
    }
}