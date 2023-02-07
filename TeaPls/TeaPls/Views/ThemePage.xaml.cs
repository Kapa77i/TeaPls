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
            switchLabel.Text = $"The switch is {(e.Value ? "on" : "off")}.";
        }
    }

}