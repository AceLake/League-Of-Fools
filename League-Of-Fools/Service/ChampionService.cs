using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using League_Of_Fools.Models;
using Newtonsoft.Json;

namespace League_Of_Fools.Service
{
    public class ChampionService : IChampionService
    {
        public async Task<List<ChampionModel>> GetAll()
        {
            List<ChampionModel> champs = new List<ChampionModel>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://ddragon.leagueoflegends.com/cdn/13.24.1/data/en_US/champion.json");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the entire JSON object
                var rootObject = JsonConvert.DeserializeObject<ChampListRootObject>(responseBody);

                // Extract the list of champions
                foreach (var champion in rootObject.Data.Values)
                {
                    champs.Add(champion);
                }
            }
            Console.WriteLine(champs);
            return champs;
        }

        public async Task<ChampionModel> GetChampionById(string Id)
        {
            ChampionModel champ = new ChampionModel();

            using (HttpClient client = new HttpClient())
            {
                string URL = ($"https://ddragon.leagueoflegends.com/cdn/13.24.1/data/en_US/champion/{Id}.json");
                Console.WriteLine(URL);
                HttpResponseMessage response = await client.GetAsync(URL);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the entire JSON object
                var rootObject = JsonConvert.DeserializeObject<ChampRootObject>(responseBody);

                // Extract the list of champions
                foreach (var champion in rootObject.Data.Values)
                {
                    champ = champion;
                }
            }
            Console.WriteLine(champ);
            return champ;
        }
    }
}
