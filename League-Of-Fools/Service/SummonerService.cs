using League_Of_Fools.Models;
using Newtonsoft.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace League_Of_Fools.Service
{
    public class SummonerService : ISummonerService
    {
        //private static readonly HttpClient client;
        private static string apiKey = "RGAPI-64a3f8aa-b98d-4e6f-885b-1a3bb896ebbf";
        private readonly IHttpClientFactory _clientFactory;
        private readonly IChampionService _championService;

        public SummonerService(IHttpClientFactory httpClientFactory, IChampionService championService)
        {

            _clientFactory = httpClientFactory;
            _championService = championService;

            //client = new HttpClient()
            //{
            //    BaseAddress = new Uri("https://americas.api.riotgames.com/")
            //};
        }

        public Task<List<SummonerModel>> GetSummonerByAccountID(string AccountID)
        {
            throw new NotImplementedException();
        }

        public async Task<SummonerModel> GetSummonerByNameAndTagLine(string SummonerName, string SummonerTagLine)
        {
            var client = _clientFactory.CreateClient("GetSummonerByNameAndTagLine");
            client.BaseAddress = new Uri("https://americas.api.riotgames.com/");
            // The first line is building the Url of the API and using the SummonerName and apiKey parameters
            // get the puuid from this request
            var url = string.Format("/riot/account/v1/accounts/by-riot-id/{0}/{1}?api_key={2}", SummonerName, SummonerTagLine, apiKey);
            var result = new SummonerModel();

            // Next, we are making an API call using the GetAsync method that sends a GET request to the specified Uri as an asynchronous operation.
            // The method returns System.Net.Http.HttpResponseMessage object that represents an HTTP response message including the status code and data.
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Next, we are calling ReadAsStringAsync method that serializes the HTTP content to a string
                var stringResponse = await response.Content.ReadAsStringAsync();

                // Finally, we are using JsonSerializer to Deserialize the JSON response string into a  objects.
                result = JsonSerializer.Deserialize<SummonerModel>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                string puuid = result.Puuid;
                result = await GetSummonerByPUUID(puuid);
            }
            else
            {
                result = null;
            }

            return result;
        }

        public async Task<SummonerModel> GetSummonerByPUUID(string puuid)
        {
            var client = _clientFactory.CreateClient("GetSummonerByPUUID");

            // The first line is building the Url of the API and using the SummonerName and apiKey parameters
            client.BaseAddress = new Uri("https://na1.api.riotgames.com");
            var url = string.Format("/lol/summoner/v4/summoners/by-puuid/{0}/?api_key={1}", puuid, apiKey);
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

                Task<List<ChampionMasteryEntry>> CMEs = GetChampionMasteryEntries(puuid);

                result.Champions = GetChampsByMastery(CMEs);
            }
            else
            {
                result = null;
            }

            return result;
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

        public async Task<List<ChampionMasteryEntry>> GetChampionMasteryEntries(string puuid)
        {
            List<ChampionMasteryEntry> result = new List<ChampionMasteryEntry>();

            using (HttpClient client = new HttpClient())
            {
                var url = string.Format("https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{0}/?api_key={1}", puuid, apiKey);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the entire JSON array as a list of ChampionMasteryEntry
                var CMEs = JsonConvert.DeserializeObject<List<ChampionMasteryEntry>>(responseBody);

                // Add the list of champions to the result
                result.AddRange(CMEs);
            }

            return result;
        }

        public async Task<List<ChampionModel>> GetChampsByMastery(Task<List<ChampionMasteryEntry>> CMEs)
        {
            // Wait for the task to complete and get the sorted list of ChampionMasteryEntry
            var masteryEntries = await CMEs;

            // Assuming _championService.GetAll() returns a list of all champions
            List<ChampionModel> allChampions = await _championService.GetAll();

            // Sort the list of champions based on the mastery entries
            var sortedChampions = allChampions
                .OrderByDescending(champion => masteryEntries.FirstOrDefault(entry => entry.ChampionId == champion.Key)?.ChampionLevel ?? 0)
                .ToList();

            return sortedChampions;
        }
    }
}

