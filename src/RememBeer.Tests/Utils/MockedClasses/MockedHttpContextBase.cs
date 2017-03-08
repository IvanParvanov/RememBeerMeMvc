using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;

namespace RememBeer.Tests.Utils.MockedClasses
{
    public class MockedHttpContextBase : HttpContextBase
    {
        private readonly HttpResponseBase response;
        private readonly HttpRequestBase request;
        private readonly IDictionary items;

        public MockedHttpContextBase()
        {
            this.response = new MockedHttpResponse();
            this.request = new MockedHttpRequest();
            this.items = new Dictionary<string, IDictionary<string, object>>()
                         {
                             { "owin.Environment", new Dictionary<string, object>() }
                         };
        }

        public MockedHttpContextBase(MockedHttpResponse response)
        {
            this.response = response;
            this.request = new MockedHttpRequest();
            this.items = new Dictionary<string, IDictionary<string, object>>()
                         {
                             { "owin.Environment", new Dictionary<string, object>() }
                         };
        }

        //public MockedHttpContextBase(MockedHttpRequest request)
        //{
        //    this.response = new MockedHttpResponse();
        //    this.request = request;
        //}

        //public MockedHttpContextBase(HttpResponseBase response, HttpRequestBase request)
        //{
        //    this.response = response;
        //    this.request = request;
        //}

        public override HttpResponseBase Response => this.response;

        public override HttpRequestBase Request => this.request;

        public override IDictionary Items => this.items;

        public override IPrincipal User { get; set; }
    }
}
