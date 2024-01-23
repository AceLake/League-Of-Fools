using League_Of_Fools.Models;
using System.Text.Json;

namespace League_Of_Fools.Service
{
    public class SummonerService : ISummonerService
    {
        private static readonly HttpClient client;
        private static string apiKey = "RGAPI-2599a28f-3ad3-46c3-8a61-8ccbeea93dfd";

        static SummonerService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://na1.api.riotgames.com")
            };
        }

        public Task<List<SummonerModel>> GetSummonerByAccountID(string AccountID)
        {
            throw new NotImplementedException();
        }

        public async Task<SummonerModel> GetSummonerByName(string SummonerName)
        {
            // The first line is building the Url of the API and using the SummonerName and apiKey parameters
            var url = string.Format("/lol/summoner/v4/summoners/by-name/{0}?api_key={1}", SummonerName, apiKey);
            var result = new SummonerModel();

            // Next, we are making an API call using the GetAsync method that sends a GET request to the specified Uri as an asynchronous operation.
            // The method returns System.Net.Http.HttpResponseMessage object that represents an HTTP response message including the status code and data.
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Next, we are calling ReadAsStringAsync method that serializes the HTTP content to a string
                var stringResponse = await response.Content.ReadAsStringAsync();

                // Finally, we are using JsonSerializer to Deserialize the JSON response string into a List of HolidayModel objects.
                result = JsonSerializer.Deserialize<SummonerModel>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                result = null;
            }

            return result;
        }

        public Task<List<SummonerModel>> GetSummonerByPUUID(string SummonerID)
        {
            throw new NotImplementedException();
        }

        public Task<List<SummonerModel>> GetSummonerByRsoPUUID(string SummonerID)
        {
            throw new NotImplementedException();
        }

        public Task<List<SummonerModel>> GetSummonerBySummonerID(string SummonerID)
        {
            throw new NotImplementedException();
        }

        public Task<List<SummonerModel>> GetSummonerByToken(string Bearertoken)
        {
            throw new NotImplementedException();
        }
    }
}

