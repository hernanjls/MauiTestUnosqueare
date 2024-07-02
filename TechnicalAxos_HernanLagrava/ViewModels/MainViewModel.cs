using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using TechnicalAxos_HernanLagrava.Models;
using TechnicalAxos_HernanLagrava.Services;

namespace TechnicalAxos_HernanLagrava.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly ICountryService? _service;
        private readonly ICustomAppInfo? _appInfo;
        private readonly HttpClient? _httpClient;
        private readonly IMainThreadInvoker? _mainThreadInvoker;

        [ObservableProperty]
        private string? bundleId;

        [ObservableProperty]
        private bool loadingImage = false;

        [ObservableProperty]
        private bool loadingList = false;

        [ObservableProperty]
        private bool loadingNext = false;

        [ObservableProperty]
        private bool visibleRefresh = false;

        [ObservableProperty]
        private ImageSource? sourceImage;

        [ObservableProperty]
        ObservableRangeCollection<CountryModel> countryList = new ();

        [ObservableProperty]
        int countrySizeList = 0;


        public MainViewModel(ICountryService service, 
                             ICustomAppInfo appInfo, 
                             HttpClient httpClient,
                             IMainThreadInvoker mainThreadInvoker)
        {
            _service = service;
            _appInfo = appInfo;
            _httpClient = httpClient;
            _mainThreadInvoker = mainThreadInvoker;

            BundleId = _appInfo.PackageName;

            _mainThreadInvoker.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(async () => await LoadImageFromUrlAsync());
                await Task.Run(async () => await GetCountryData());
            });
        }

        public MainViewModel(ICountryService serviceTest)
        {
            _service = serviceTest;
        }

        public MainViewModel()
        {
        }


        public async Task LoadImageFromUrlAsync()
        {

            try
            {
                LoadingImage = true;
                using HttpClient httpClient = new ();
                var response = await httpClient.GetAsync(Constants.ImageBaseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    SourceImage = ImageSource.FromStream(() => stream);
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
            }
            finally
            {
                LoadingImage = false;
            }

        }

        [RelayCommand]
        private async Task PickImage()
        {

            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Pick Image Please",
                    FileTypes = FilePickerFileType.Images
                });

                if (result == null)
                {
                    IsBusy = false;
                    return;
                }

                Stream stream = await result.OpenReadAsync();
                SourceImage = ImageSource.FromStream(() => stream);

                IsBusy = false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error picking image: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }

        }

        [RelayCommand]
        private async Task Reload()
        {

            if (IsBusy) return;

            try
            {
                await GetCountryData();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in reload data list {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }

        }
        
        public async Task GetCountryData()
        {

            try
            {
                VisibleRefresh = false;
                LoadingList = true;
                var list = await _service.GetListAsync();
                

                if (list.Count >0 )
                {
                    CountryList.Clear();
                    CountryList = new ObservableRangeCollection<CountryModel>(list);
                    CountrySizeList = _service.GetSizeList();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in get data list: {ex.Message}");
            }
            finally
            {
                LoadingList = false;
                VisibleRefresh = true;
            }

        }

        [RelayCommand]
        public async Task GetNextData()
        {
            try
            {
                if (LoadingList || IsBusy || LoadingNext) { return; }

                var loadedItemsCount = CountryList.Count;

                if (CountrySizeList > 0 && CountrySizeList > loadedItemsCount)
                {
                    LoadingNext = true;
                    CountryList.AddRange(await _service.GetListAsync(loadedItemsCount));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                LoadingNext = false;
            }
        }



    }
}
