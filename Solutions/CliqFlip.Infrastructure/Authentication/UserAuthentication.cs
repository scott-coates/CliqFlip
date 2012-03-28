using System;
using System.Web;
using System.Web.Security;
using CliqFlip.Infrastructure.Authentication.Interfaces;

namespace CliqFlip.Infrastructure.Authentication
{
	public class UserAuthentication : IUserAuthentication
	{
		#region IUserAuthentication Members

		public void Login(string username, bool stayLoggedIn, string role)
		{
			//TODO: Set Secure = true when we use ssl;
			var authTicket = new FormsAuthenticationTicket
				(1, //version
				 username, // user name
				 DateTime.UtcNow, //creation
				 DateTime.UtcNow.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), //Expiration
				 stayLoggedIn, //Persistent
				 role ?? ""); // role - this cannot be null or the ticket will not be encrypted

			// Now encrypt the ticket.
			string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

			// Create a cookie and add the encrypted ticket to the
			// cookie as data.
			var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
			{
				HttpOnly = true, 
				Path = FormsAuthentication.FormsCookiePath, 
				Secure = FormsAuthentication.RequireSSL
			};

			if (FormsAuthentication.CookieDomain != null)
			{
				authCookie.Domain = FormsAuthentication.CookieDomain;
			}

			if (authTicket.IsPersistent)
			{
				authCookie.Expires = authTicket.Expiration;
			}

			// Add the cookie to the outgoing cookies collection.
			HttpContext.Current.Response.Cookies.Add(authCookie);
		}

		public void Logout()
		{
			FormsAuthentication.SignOut();
		}

		#endregion
	}
}