using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TeaPls.ViewModels
{
    public class ThemeiewModel : BaseViewModel
    {
        private bool isDarkModeEnabled;
        public bool IsDarkModeEnabled
        {
            get => isDarkModeEnabled;
            set
            {
                isDarkModeEnabled = value;
                OnPropertyChanged();
                ApplyTheme(value);
            }
        }

        private void ApplyTheme(bool isDarkMode)
        {

        }
        public ThemeiewModel()
        {
            Title = "Theme";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

        }
        public ICommand OpenWebCommand { get; }
    }
}
