using TechnicalAxos_HernanLagrava.ViewModels;

namespace TechnicalAxos_HernanLagrava.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }

}
