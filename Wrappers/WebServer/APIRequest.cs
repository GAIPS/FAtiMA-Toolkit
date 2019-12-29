using System.Net;
using System;
using System.IO;

namespace WebServer
{
    public enum HTTPMethod
    {
        GET,
        POST,
        DELETE
    }

    public class APIRequest
    {
        public string ErrorMessage { get; set; }
        public APIResource Resource { get; set; }
        public HTTPMethod Method { get; set; }
        public string ScenarioName { get; set; }
        public int ScenarioInstance { get; set; }
        public string CharacterName { get; set; }
        public string RequestBody { get; set; }
        public string Key { get; set; }

        public APIRequest(HttpListenerRequest request)
        {
            int requestSize = request.Url.Segments.Length;

            this.Key = request.Headers.Get("key");

            if (requestSize == 1)
            {
                this.ErrorMessage = APIErrors.ERROR_INVALID_URL_SIZE;
                return;
            }

            Enum.TryParse(request.HttpMethod, true, out HTTPMethod m);
            Method = m;
            this.Resource = APIResource.FromString(GetResourceName(m, request.Url.Segments));
            if (this.Resource == null)
            {
                this.ErrorMessage = "Unkown Resource!";
                return;
            }

            if (this.Method == HTTPMethod.DELETE) requestSize--; //special case
            if (requestSize != this.Resource.URLSegmentSize+1)
            {
                this.ErrorMessage = APIErrors.ERROR_INVALID_URL_SIZE;
                return;
            }

            switch (Resource.Type)
            {
                case APIResourceType.SCENARIOS:
                    if (this.Method == HTTPMethod.DELETE) this.ScenarioName = request.Url.Segments[2].ToLower();
                    break;
                case APIResourceType.KEY:
                    this.ScenarioName = request.Url.Segments[2].ToLower().Trim('/');
                    break;
                case APIResourceType.INSTANCES:
                    this.ScenarioName = request.Url.Segments[2].ToLower().Trim('/');
                    if (this.Method == HTTPMethod.DELETE) this.ScenarioInstance = int.Parse(request.Url.Segments[4]);
                    break;
              
                case APIResourceType.CHARACTERS:
                case APIResourceType.TICK:
                case APIResourceType.ACTIONS:
                    this.ScenarioName = request.Url.Segments[2].ToLower().Trim('/');
                    this.ScenarioInstance = int.Parse(request.Url.Segments[4].Trim('/'));
                    break;
                case APIResourceType.DECISIONS:
                case APIResourceType.BELIEFS:
                case APIResourceType.MEMORIES:
                case APIResourceType.PERCEPTIONS:
                    this.ScenarioName = request.Url.Segments[2].ToLower().Trim('/');
                    this.ScenarioInstance = int.Parse(request.Url.Segments[4].Trim('/'));
                    this.CharacterName = request.Url.Segments[6].Trim('/');
                    break;
            }

            if (Method == HTTPMethod.POST && request.HasEntityBody)
            {
                using (Stream body = request.InputStream) // here we have data
                {
                    using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
                    {
                        this.RequestBody = reader.ReadToEnd();
                    }
                }
            }
        }

        private string GetResourceName(HTTPMethod method, string[] urlSegments )
        {
            switch (method)
            {
                case HTTPMethod.GET:
                case HTTPMethod.POST: return urlSegments[urlSegments.Length - 1].ToLower();
                case HTTPMethod.DELETE: return urlSegments[urlSegments.Length - 2].ToLower().Trim('/');
                default : return null;
            }
        }
    }

}
