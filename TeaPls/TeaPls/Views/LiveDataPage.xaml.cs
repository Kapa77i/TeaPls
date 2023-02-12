using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeaPls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LiveDataPage : ContentPage
    {

        public LiveDataPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}