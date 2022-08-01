using System.Collections.Generic;
using System.Threading.Tasks;
using PMB.Models.PositiveModels;

namespace PMB.Integration.PositiveBet.Abstract
{
    public interface IPositiveClient
    {
        public Task<bool> Login(string login, string password);
        
        public Task<List<Fork>> GetForks();

        public Task<BetData> GetBetData(Fork fork);
    }
}