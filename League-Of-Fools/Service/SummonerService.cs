using League_Of_Fools.Models;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace League_Of_Fools.Service
{
    public class SummonerService : ISummonerService
    {
        //private static readonly HttpClient client;
        private static string apiKey = "RGAPI-620ed0c1-1ff7-48c8-9ed5-9c0763db21fe";
        private readonly IHttpClientFactory _clientFactory;
        private readonly IChampionService _championService;

        public SummonerService(IHttpClientFactory httpClientFactory, IChampionService championService)
        {
            // Using HttpClientFactory so that I can change the base BaseAddress of the client
            // Because the base address will change to allow us to select the correct host to execute our request to
            _clientFactory = httpClientFactory;

            // I will use the championService to get all of the champions to re-order them by mastery score
            _championService = championService;
        }

        public Task<List<SummonerModel>> GetSummonerByAccountID(string AccountID)
        {
            throw new NotImplementedException();
        }

        public async Task<SummonerModel> GetSummonerByNameAndTagLine(SummonerModel summoner)
        {
            // Creating an instance of a HttpClient with a local name of GetSummonerByNameAndTagLine
            var client = _clientFactory.CreateClient("GetSummonerByNameAndTagLine");

            // I have to set the RegionalRoutingValue to lowercase becouse thats how the base host address is set up 
            string region = summoner.RegionalRoutingValue.ToLower();
            client.BaseAddress = new Uri(string.Format("https://{0}.api.riotgames.com/", region));

            // The first line is building the Url of the API and using the SummonerName and apiKey parameters
            var url = string.Format("/riot/account/v1/accounts/by-riot-id/{0}/{1}?api_key={2}", summoner.GameName, summoner.TagLine, apiKey);

            var result = new SummonerModel();

            // Making an API call using the GetAsync method which sends a request as an asynchronous operation.
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // We are calling ReadAsStringAsync method that takes the HTTP content and converts it to a string
                var stringResponse = await response.Content.ReadAsStringAsync();

                // Finally, we are using JsonSerializer to Deserialize the JSON response string into a  objects.
                result = JsonSerializer.Deserialize<SummonerModel>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                // Setting the PlatformRoutingValue to the one we got from the user
                // Because we will need it to change the base address
                result.PlatformRoutingValue = summoner.PlatformRoutingValue;

                // This will get the rest of the info I need with the puuid I gained from the responce
                result = await GetSummonerByPUUID(result);
            }
            else
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// This gets info like account level and game name and icon id
        /// </summary>
        /// <param name="summoner"></param>
        /// <returns></returns>
        public async Task<SummonerModel> GetSummonerByPUUID(SummonerModel summoner)
        {
            // Creating an instance of a HttpClient with a local name of GetSummonerByPUUID
            var client = _clientFactory.CreateClient("GetSummonerByPUUID");

            // The first line is building the base address of the API and using the PlatformRoutingValue to specify the region of the account
            string platform = summoner.PlatformRoutingValue.ToLower();
            // example: https://euw1.api.riotgames.com
            client.BaseAddress = new Uri(string.Format("https://{0}.api.riotgames.com",platform));

            // building the rest of the url
            var url = string.Format("/lol/summoner/v4/summoners/by-puuid/{0}/?api_key={1}", summoner.Puuid, apiKey);
            var result = new SummonerModel();

            // get the response
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<SummonerModel>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                Task<List<ChampionMasteryEntry>> CMEs = GetChampionMasteryEntries(summoner);

                // seting the ChampionMasteryEntry for the summoner to get the info in the view
                result.CMEs = CMEs;

                // this will sort the champ list psecifically to the users mastery score
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

        public async Task<List<ChampionMasteryEntry>> GetChampionMasteryEntries(SummonerModel summoner)
        {
            List<ChampionMasteryEntry> result = new List<ChampionMasteryEntry>();

            using (HttpClient client = new HttpClient())
            {
                var url = string.Format("https://{0}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{1}/?api_key={2}",summoner.PlatformRoutingValue.ToLower(), summoner.Puuid, apiKey);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the entire JSON array as a list of ChampionMasteryEntry
                var CMEs = JsonConvert.DeserializeObject<List<ChampionMasteryEntry>>(responseBody);

                // Add the list of champions to the result
                result.AddRange((IEnumerable<ChampionMasteryEntry>)CMEs);
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
                .OrderByDescending(champion => masteryEntries.FirstOrDefault(entry => entry.ChampionId == champion.Key)?.ChampionPoints ?? 0)
                .ToList();

            return sortedChampions;
        }
    }
}

