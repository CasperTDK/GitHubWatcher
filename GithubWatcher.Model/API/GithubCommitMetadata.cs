using System;

namespace GithubWatcher.Model.API
{
    public class GithubCommitMetadata
    {
        public string sha { get; set; }
        public string url { get; set; }
        public GithubCommit commit { get; set; }

        public DateTime committedDate
        {
            get { return commit.committer.date; }
        }

        public override string ToString()
        {
            return string.Format("#{0}. Message: {1}. Author: {2}", sha,
                                 commit.message, commit.author);
        }
    }
}