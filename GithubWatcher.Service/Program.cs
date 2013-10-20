using System;
using System.Linq;
using System.Reflection;
using GithubWatcher.Infrastructure;
using GithubWatcher.Infrastructure.Services;
using GithubWatcher.Model.API;
using ServiceStack.Text;
using log4net;

namespace GithubWatcher.Service
{
    internal class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            //PrintStuff();

            DoStuff();

            Console.ReadLine();
        }

        private static void PrintStuff()
        {
            var client = new GithubV3ApiGateway();
            var branchManager = new GithubBranchManager();
            var commitManager = new GithubCommitManager();
            var latestReleasebranch = branchManager.GetReleaseBranchesList("d60", "DanskeCommodities").OrderBy(x => x.name).First();
            //Console.WriteLine("\n-- GetRepoBranches(ServiceStack,ServiceStack.Text): \n" + branchManager.GetReleaseBranchesList("d60", "DanskeCommodities").Dump());
            Console.WriteLine("\n-- GetUserRepo(ServiceStack,ServiceStack.Text): \n" + commitManager.GetCommitsForRepo("d60", "DanskeCommodities").Take(2).Dump());
            //Console.WriteLine("\n-- GetUserRepo(ServiceStack,ServiceStack.Text): \n" + client.GetOrgRepos("d60", "DanskeCommodities").Dump());
            Console.WriteLine("\n-- GetUserRepo(ServiceStack,ServiceStack.Text): \n" + commitManager.GetCommitsForRepo("d60", "DanskeCommodities", latestReleasebranch.name).Take(2).Dump());
        }

        private static void DoStuff()
        {
            var branchManager = new GithubBranchManager();
            var commitManager = new GithubCommitManager();

            var latestReleasebranch = branchManager.GetReleaseBranchesList("d60", "DanskeCommodities").OrderByDescending(x => x.name).First();
            var releaseCommites = commitManager.GetCommitsForRepo("d60", "DanskeCommodities", latestReleasebranch.name);
            var firstCommit = releaseCommites.Min(x => x.committedDate);
            var masterCommits = commitManager.GetCommitsForRepoUntil("d60", "DanskeCommodities", "master", firstCommit);

            foreach (var githubCommitMetadata in releaseCommites)
            {
                var commitExistsInBase = false;
                foreach (var masterCommit in masterCommits)
                {
                    var commitFoundInmaster = masterCommit.sha == githubCommitMetadata.sha;
                    if (commitFoundInmaster)
                    {
                        commitExistsInBase = true;
                        break;
                    }
                }
                if (!commitExistsInBase)
                {
                    Console.WriteLine("Could not find commit {0}", githubCommitMetadata);
                }
            }
        }
    }
}