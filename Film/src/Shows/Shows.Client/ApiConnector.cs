using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shows.Client
{
    class ApiConnector : ApiConnector<object>
    {
        
    }

    class ApiConnector<T>
    {
        private string baseUri;
        private Dictionary<string, object> args = new Dictionary<string, object>();
        private string controller;

        public ApiConnector(string baseUri = Program.ApiUri)
        {
            this.baseUri = baseUri;
        }

        public ApiConnector<T> AddParameter(string name, object value)
        {
            if (args.ContainsKey(name))
                args[name] = value;
            else
                args.Add(name, value);

            return this;
        }

        public ApiConnector<T> ForController(string controller)
        {
            this.controller = controller;

            return this;
        }

        public string ArgumentsAsUri()
        {
            if (args.Count == 0)
                return "";
            else
            {
                return "?" + String.Join("&", args.Select(x => x.Key + "=" + WebUtility.UrlEncode(x.Value.ToString())));
            }
        }

        public T Get()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(controller + "/" + ArgumentsAsUri()).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public bool Post(T data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(controller + "/" + ArgumentsAsUri(), data).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Delete()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.DeleteAsync(controller + "/" + ArgumentsAsUri()).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
