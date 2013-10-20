using System;
using System.Net;
using System.Text;

namespace GithubWatcher.Core
{
    public static class AddAuthenticationExcetn
    {
        public static HttpWebRequest AddAuthentication(this HttpWebRequest request, string userName, string token)
        {
            var authInfo = string.Format("{0}:{1}", userName, token);
            authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
            return request;
        }
    }
}