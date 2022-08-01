using System.Linq;
using System.Threading.Tasks;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Bll.Mappers;
using PMB.Dal.Models;
using PMB.Dal.Repositories;
using DeleteForksModel = PMB.Dal.Models.DeleteForksModel;

namespace PMB.Dal.Bll.Services
{
    public class ForksService
    {
        private readonly ForkRepository _forkRepository;

        public ForksService(ForkRepository forkRepository)
        {
            _forkRepository = forkRepository;
        }

        public async Task<int> DeleteWithBets(Dtos.DeleteForksModel model)
        {
            return await _forkRepository.DeleteWithBets(new DeleteForksModel
            {
                LifetimeBefore = model.LifetimeBefore
            });
        }

        public async Task<ForkDto[]> GetForksByQuery(GetForksByQueryModel model)
        {
            var result = await _forkRepository.SelectAsync(new SelectForksQueryModel
            {
                Bookmakers = model.Bookmakers,
                Sports = model.Sports,
                BetTypes = model.BetTypes,
                CridIds = model.CridIds
            });

            return result?.Select(x => x.Convert()).ToArray();
        }
    }
}