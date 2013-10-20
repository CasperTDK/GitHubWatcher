using System.Collections.Generic;
using System.Linq;
using CSharpVerbalExpressions;
using GithubWatcher.Model.API;

namespace GithubWatcher.Infrastructure.Services
{
    public class GithubBranchManager
    {
        private GithubV3ApiGateway client;

        public GithubBranchManager()
        {
            client = new GithubV3ApiGateway();
        }

        public List<GithubBranch> GetAllBranches(string githubUsername, string projectName)
        {
            var allBranches = client.GetRepoBranches(githubUsername, projectName);
            return allBranches;
        }

        public List<GithubBranch> GetReleaseBranchesList(string githubUsername, string projectName)
        {
            //todo: move to common patterns
            var releaseBranchVerbalExpression = VerbalExpressions.DefaultExpression
                                                                 .Then("release-")
                                                                 .Range(1, 9).Then(".").Range(0, 9).Range(0, 9).Then(".").Range(0, 9).Then(".").Range(0, 9) //1.39.0.0
                                                                 .EndOfLine();

            return GetAllBranches(githubUsername, projectName).Where(x => releaseBranchVerbalExpression.Test(x.name)).ToList();
        }
    }
}