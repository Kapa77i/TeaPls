using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeaPls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemePage : ContentPage
    {
        public ThemePage()
        {
            InitializeComponent();
        }
        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            //switchLabel.Text = $"Night mode: {(e.Value ? e.Value.ToString() : e.Value.ToString())}.";

            if (e.Value == true)
            {
                App.Current.UserAppTheme = OSAppTheme.Dark;
            }
            else
            {
                App.Current.UserAppTheme = OSAppTheme.Light;
            }
        }
    }

}