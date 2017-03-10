using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace RememBeer.Tests.Utils.MockedClasses
{
    public class MockedHttpRequest : HttpRequestBase
    {
        private readonly NameValueCollection queryString;

        public MockedHttpRequest()
        {
            this.queryString = new NameValueCollection();
            this.Keys = new Dictionary<string, string>();
        }

        public MockedHttpRequest(IDictionary<string, string> keys)
        {
            this.Keys = keys;
        }

        public IDictionary<string, string> Keys { get; set; }

        public override string this[string key] => this.Keys[key];

        public override NameValueCollection QueryString => this.queryString;
    }
}
