using System;
using System.Net;
using System.Text;
using RestSharp;

namespace CliqFlip.Web.Mvc.RestSharp
{
    public class CliqFlipHttpFactory : IHttpFactory
    {
        private readonly CredentialCache _credCache;

        public CliqFlipHttpFactory(string username, string password, Uri root)
        {
            const string basic = "Basic";
            var networkCredential = new NetworkCredential(username, password);
            _credCache = new CredentialCache { { root, basic, networkCredential } };
        }

        #region IHttpFactory Members

        public IHttp Create()
        {
            var http = new Http { Credentials = _credCache };

            return http;
        }

        #endregion
    }
}