using System;
using System.Text;
using RestSharp;

namespace CliqFlip.Web.Mvc.RestSharp
{
    public class CliqFlipHttpFactory : IHttpFactory
    {
        private readonly string _username;
        private readonly string _password;

        public CliqFlipHttpFactory(string username, string password)
        {
            _username = username;
            _password = password;
        }

        #region IHttpFactory Members

        public IHttp Create()
        {
            string str = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _username, _password)));
            string str2 = string.Format("Basic {0}", str);

            var http = new Http();

            http.Headers.Add(new HttpHeader { Name = "Authorization", Value = str2 });

            return http;
        }

        #endregion
    }
}