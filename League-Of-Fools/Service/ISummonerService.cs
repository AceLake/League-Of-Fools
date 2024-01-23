using League_Of_Fools.Models;

namespace League_Of_Fools.Service
{
    public interface ISummonerService
    {
        Task<List<SummonerModel>> GetSummonerByRsoPUUID(string SummonerID);
        Task<List<SummonerModel>> GetSummonerByAccountID(string AccountID);
        Task<SummonerModel> GetSummonerByName(string SummonerName);
        Task<List<SummonerModel>> GetSummonerByPUUID(string SummonerID);
        Task<List<SummonerModel>> GetSummonerByToken(string Bearertoken);
        Task<List<SummonerModel>> GetSummonerBySummonerID(string SummonerID);
    }
}
