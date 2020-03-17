using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Livraria.WebApplication.Helper
{
    public class LivrariaApi<T>
    {
        private readonly string _apiServico;

        public LivrariaApi(string apiServico)
        {
            _apiServico = apiServico;
        }

        public HttpClient ObterClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:62592/")
            };
            return client;
        }

        public async Task<IList<T>> GetAsync()
        {
            var client = ObterClient();
            var res = await client.GetAsync(_apiServico);

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<T>>(results);
            }
            else
                return new List<T>();
        }

        public async Task<T> GetAsync(int id)
        {
            var client = ObterClient();
            var res = await client.GetAsync($"{ _apiServico}/{id}");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(results);
            }
            else
                return default(T);
        }

        public HttpResponseMessage PostAsJsonAsync(T entidade)
        {
            var client = ObterClient();
            var postTask = client.PostAsJsonAsync(_apiServico, entidade);
            postTask.Wait();

            return postTask.Result;
        }

        public HttpResponseMessage PutAsJsonAsync(int id, T entidade)
        {
            var client = ObterClient();
            var postTask = client.PutAsJsonAsync($"{_apiServico}/{id}", entidade);
            postTask.Wait();

            return postTask.Result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var client = ObterClient();
            return await client.DeleteAsync($"{ _apiServico}/{id}");
        }
    }
}
