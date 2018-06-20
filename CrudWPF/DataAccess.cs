using CrudWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrudWPF
{
    public class DataAccess
    {
        // Author data access 

        //Get
        public async Task<List<Author>> GetAuthorEntityAsync(string path)
        {
            List<Author> authors;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50098/");

                string product = "";

                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsStringAsync();
                }
                authors = JsonConvert.DeserializeObject<List<Author>>(product);

            }
            return authors;
        }

        //Create
        public async Task<bool> PostAuthorEntityAsync(string path, Author auth)
        {
            HttpResponseMessage call;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50098/");

                var myContent = JsonConvert.SerializeObject(auth);

                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

                call = await client.PostAsync(path, stringContent);
            }
            return call.IsSuccessStatusCode;
        }

        //Update
        public async Task<bool> PutAuthorEntityAsync(string path, Author auth)
        {
            HttpResponseMessage call;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50098/");

                var myContent = JsonConvert.SerializeObject(auth);

                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

               call = await client.PutAsync(path, stringContent);
            }
            return call.IsSuccessStatusCode;
        }

        //Delete
        public  async Task<bool> DeleteAuthorEntityAsync(string path)
        {
            HttpResponseMessage call;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50098/");

                call = await client.DeleteAsync(path);

            }
            return call.IsSuccessStatusCode;
        }
    }
}
