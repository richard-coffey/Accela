using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Accela.DataAccess.DataModels;
using System.Runtime.Serialization.Json;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestProject
{
    public class EndpointTests
    {
        [Fact]
        public async Task Test_APICall_PeopleListAsync()
        {
            var http = new HttpClient();
            Uri requestUri = new Uri("https://localhost:44389/home");

            HttpResponseMessage httpResponse = await http.GetAsync(requestUri);
            string responseString = await httpResponse.Content.ReadAsStringAsync();
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(responseString);

            Assert.NotNull(people);
        }
    }
}
