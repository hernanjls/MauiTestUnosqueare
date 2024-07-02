using CommunityToolkit.Mvvm.ComponentModel;

namespace TechnicalAxos_HernanLagrava.ViewModels
{
    public partial class BaseViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private string? title;

        [ObservableProperty]
        private bool isBusy = false;
    }
}
