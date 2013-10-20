using System.Collections.Generic;
using System.IO;
using GithubWatcher.Core;
using GithubWatcher.Model.API;
using ServiceStack.Text;

namespace GithubWatcher.Infrastructure.Services
{
    public class GithubV3ApiGateway
    {
        public const string GithubApiBaseUrl = "https://api.github.com/";

        public T GetJson<T>(string route, params object[] routeArgs)
        {
            var uriComponents = route.Fmt(routeArgs);
            var path = GithubApiBaseUrl.AppendUrlPathsRaw(uriComponents);
            var token = File.ReadAllText("token.txt");
            var responseJSon = path.GetJsonFromUrl(req => req.AddAuthentication("CasperTDK", token));
            return responseJSon
                .FromJson<T>();
        }

        public List<GithubBranch> GetRepoBranches(string githubUsername, string projectName)
        {
            return GetJson<List<GithubBranch>>("repos/{0}/{1}/branches", githubUsername, projectName);
        }

        public GithubRepo GetUserRepo(string githubUsername, string projectName)
        {
            return GetJson<GithubRepo>("repos/{0}/{1}", githubUsername, projectName);
        }

        public List<GithubRepo> GetOrgRepos(string githubUsername, string projectName)
        {
            return GetJson<List<GithubRepo>>("orgs/{0}/repos", githubUsername, projectName);
        }

    
    }
}