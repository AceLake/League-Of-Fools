using League_Of_Fools.Models;

namespace League_Of_Fools.Service
{
    public interface IChampionService
    {
        public Task<List<ChampionModel>> GetAll();
    }
}
