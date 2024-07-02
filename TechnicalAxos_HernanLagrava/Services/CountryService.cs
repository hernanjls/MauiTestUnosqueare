using TechnicalAxos_HernanLagrava.Models;

namespace TechnicalAxos_HernanLagrava.Services
{
    public class CountryService : BaseService, ICountryService
    {

        private static List<CountryModel> AllCountryList = new();

        public CountryService()
        {
        }
        public async Task<List<CountryModel>> GetListAsync(int skip = 0)
        {
            
            if(skip == 0)
            {
                var list = await GetAsync<IEnumerable<CountryModel>>("all");
                if(list.Any())
                {
                    AllCountryList = list.ToList().OrderBy(x => x.Name?.Common).ToList(); 
                    return AllCountryList.Take(Constants.PageListSize).ToList(); 
                }
            }
           
            //Sleep for Simulation when next data is loaded
            await Task.Delay(1000);
           

            if(AllCountryList.Count > 0)
            {
                return skip == 0 ?
                AllCountryList.Take(Constants.PageListSize).ToList() :
                AllCountryList.Skip(skip).Take(Constants.PageListSize).ToList();
            }
            
            return AllCountryList;
            
        }

        public int GetSizeList()
        {
            return AllCountryList.Count;
        }

    }
 }
