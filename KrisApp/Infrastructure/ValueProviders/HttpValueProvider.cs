using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace KrisApp.Infrastructure.ValueProviders
{
    public class HttpValueProvider : IValueProvider
    {
        private readonly NameValueCollection _headers;
        private readonly string[] _headerKeys;

        public HttpValueProvider(NameValueCollection httpHeaders)
        {
            _headers = httpHeaders;
            _headerKeys = _headers.AllKeys;
        }

        public bool ContainsPrefix(string prefix)
        {
            return _headerKeys.Any(x => x.Replace("-", "").Equals(prefix, StringComparison.OrdinalIgnoreCase));
        }

        public ValueProviderResult GetValue(string key)
        {
            // checks, if HTTP header contains a key field with the same name as the model property
            string header = _headerKeys.FirstOrDefault(x => x.Replace("-", "").Equals(key, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(_headers[header]))
            {
                return new ValueProviderResult(_headers[header], _headers[header], CultureInfo.CurrentCulture);
            }
            else
            {
                return null;
            }
        }
    }
}