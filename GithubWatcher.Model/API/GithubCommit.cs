namespace GithubWatcher.Model.API
{
    public class GithubCommit
    {
        public GitHubAuthor author { get; set; }
        public GitHubAuthor committer { get; set; }
        public string message { get; set; }

       
    }
}