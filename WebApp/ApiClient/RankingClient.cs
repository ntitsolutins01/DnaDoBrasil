using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceRanking = "Rankings";

		#region Main Methods

		public Task<long> CreateRanking (RankingModel.CreateUpdateRankingCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceRanking }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateRanking (int id, RankingModel.CreateUpdateRankingCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceRanking }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteRanking (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceRanking }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public RankingDto GetRankingById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceRanking}/Ranking/{id}"));
            return Get<RankingDto>(requestUrl);
        }
        public List<RankingDto> GetRankingsAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceRanking}"));
            return Get<List<RankingDto>>(requestUrl);
        }

        #endregion
    }
}