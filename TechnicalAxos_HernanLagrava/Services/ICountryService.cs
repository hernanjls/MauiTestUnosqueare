using TechnicalAxos_HernanLagrava.Models;

namespace TechnicalAxos_HernanLagrava.Services
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetListAsync(int skip = 0);
        int GetSizeList();
    }
 }
