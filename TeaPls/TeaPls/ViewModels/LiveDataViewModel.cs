using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TeaPls.ViewModels
{
    public class RSSViewModel : BaseViewModel
    {

        public RSSViewModel()
        {
            Title = "LiveData";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

        }
        public ICommand OpenWebCommand { get; }
    }
}
