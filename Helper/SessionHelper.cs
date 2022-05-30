using Microsoft.AspNetCore.Http;
using System;

namespace CompanyAgreement.Helper
{
    public class SessionHelper
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public void Set(string key, object value)
        {
            value = !String.IsNullOrEmpty(value.ToString()) ? value : "";
            HttpContextAccessor.HttpContext.Session.SetString(key, value + "");
        }
        public void Set(string key, int value)
        {
            HttpContextAccessor.HttpContext.Session.SetInt32(key, value );
        }
        public string Get(string key)
        {
            return HttpContextAccessor.HttpContext.Session.GetString(key);
        }
        public int? Getid(string key)
        {
            return HttpContextAccessor.HttpContext.Session.GetInt32(key);
        }
    }
}
