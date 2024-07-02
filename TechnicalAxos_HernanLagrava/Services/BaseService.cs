using Newtonsoft.Json;
using System.Diagnostics;

namespace TechnicalAxos_HernanLagrava.Services
{
    /// <summary>
    /// This class provides base functionality for other service classes.
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// An instance of <see cref="HttpClient"/>.
        /// </summary>
        protected readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        public BaseService()
        {
            _httpClient = new HttpClient();

        }

        /// <summary>
        /// Sends a GET request to the specified endpoint and returns the response as an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the response object.</typeparam>
        /// <param name="endpoint">The endpoint to send the GET request to.</param>
        /// <returns>A task of type <typeparamref name="T"/>.</returns>
        protected async Task<T> GetAsync<T>(string endpoint)
        {

            try
            {
                var response = await _httpClient.GetAsync($"{Constants.ServiceBaseUrl}/{endpoint}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content); 


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Cannot get data: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "Ok");
                return default;
            }
        }

     
    }
}
