using System;
using System.Collections.Generic;
using System.Linq;
using GithubWatcher.Model.API;

namespace GithubWatcher.Infrastructure.Services
{
    public class GithubCommitManager
    {
        private GithubV3ApiGateway client;

        public GithubCommitManager()
        {
            client = new GithubV3ApiGateway();
        }
       
        public List<GithubCommitMetadata> GetCommitsForRepo(string githubUsername, string projectName, string branchOrSha = "master")
        {
            var parameters = string.Format("?sha={0}", branchOrSha);
            return client.GetJson<List<GithubCommitMetadata>>("repos/{0}/{1}/commits" + parameters, githubUsername, projectName);
        }



        public List<GithubCommitMetadata> GetCommitsForRepoUntil(string githubUsername, string projectName, string branch, DateTime endtime)
        {
            var list = new List<GithubCommitMetadata>();
            var latestCommit = client.GetJson<List<GithubCommitMetadata>>("repos/{0}/{1}/commits", githubUsername, projectName).First();
            var hitTheEnd = false;
            while (!hitTheEnd)
            {
                var parameters = string.Format("?sha={0}&per_page=300", latestCommit.sha);
                var data = client.GetJson<List<GithubCommitMetadata>>("repos/{0}/{1}/commits" + parameters, githubUsername, projectName);
                foreach (var githubCommitMetadata in data)
                {
                    if (githubCommitMetadata.committedDate >= endtime)
                    {
                        list.Add(githubCommitMetadata);
                    }
                    else
                    {
                        hitTheEnd = true;
                        break;
                    }
                }

                latestCommit = data.Last();
            }
            return list;
        }
    }
}