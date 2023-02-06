using System.ComponentModel;
using TeaPls.ViewModels;
using Xamarin.Forms;

namespace TeaPls.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}