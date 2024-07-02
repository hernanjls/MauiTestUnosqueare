using Microsoft.Maui.Controls.Internals;
using NSubstitute;
using System.Net;
using TechnicalAxos_HernanLagrava.Services;
using TechnicalAxos_HernanLagrava.ViewModels;

namespace MauiTestProject
{
    public class MainViewModelTest
    {
        // Simulation for correct response for not real call
        // Also show how use dependency injection for  mock scenarios
        [Fact]
        public async Task LoadImageFromUrlAsync_SuccessfulResponse_SetsSourceImage()
        {
            // Arrange
            var countryService = Substitute.For<ICountryService>();
            var appInfo = Substitute.For<ICustomAppInfo>();
            var mainThreadInvoker = Substitute.For<IMainThreadInvoker>();
            appInfo.PackageName.Returns("com.unosquare.technicalaxoshlagrava");

            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StreamContent(new MemoryStream(new byte[0]))
            };

            var httpMessageHandler = new TestHttpMessageHandler(responseMessage);
            var httpClient = new HttpClient(httpMessageHandler);

            var viewModel = new MainViewModel(countryService, appInfo, httpClient, mainThreadInvoker);

            // Act
            await viewModel.LoadImageFromUrlAsync();

            // Assert
            Assert.NotNull(viewModel.SourceImage);
            Assert.False(viewModel.LoadingImage);
        }

        [Fact]
        public async Task LoadImageFromRealUrlAsync_SuccessfulResponse_SetsSourceImage()
        {
            // Arrange
            var viewModel = new MainViewModel();

            // Act
            await viewModel.LoadImageFromUrlAsync();

            // Assert
            Assert.NotNull(viewModel.SourceImage);
            Assert.False(viewModel.LoadingImage); // Ensures LoadingImage is set correctly
        }


        [Fact]
        public async Task GetCountryData_SuccessfulResponse_UpdatesCountryList()
        {
            // Arrange

            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICountryService, CountryService>() // Implementation for test real service
                .BuildServiceProvider();

            TechnicalAxos_HernanLagrava.Services.DependencyResolver.SetServiceProvider(serviceProvider);

            var viewModel = new MainViewModel(serviceProvider.GetRequiredService<ICountryService>());

            // Act
            await viewModel.GetCountryData();

            // Assert
            Assert.True(viewModel.CountryList.Count > 0);
            Assert.True(viewModel.CountrySizeList >= viewModel.CountryList.Count);


        }

        [Fact]
        public async Task GetNextData_SuccessfulResponse_AddsToCountryList()
        {
            // Arrange
           
            var serviceProvider = new ServiceCollection()
              .AddSingleton<ICountryService, CountryService>() // Implementación real para pruebas
              .BuildServiceProvider();

            TechnicalAxos_HernanLagrava.Services.DependencyResolver.SetServiceProvider(serviceProvider);

            var viewModel = new MainViewModel(serviceProvider.GetRequiredService<ICountryService>());


            await viewModel.GetCountryData(); // You can load initial data for CountryList and CountrySizeList
            int tt = viewModel.CountryList.Count;

            // Act
            await viewModel.GetNextData();

            // Assert
            Assert.True(viewModel.CountryList.Count > 0); 
            Assert.True(viewModel.CountryList.Count > tt); // Verify if the current list is greater that the initial
            Assert.False(viewModel.LoadingNext); // Verify that LoadingNext has been reset to false after completing the operation
        }

    }
}
