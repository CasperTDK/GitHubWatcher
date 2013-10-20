using System;

namespace GithubWatcher.Model.API
{
    public class GitHubAuthor
    {
        public DateTime date { get; set; }
        public string Name { get; set; }
        public string email { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, email);
        }
    }
}