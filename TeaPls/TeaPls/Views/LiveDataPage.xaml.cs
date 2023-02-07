using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeaPls.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RSS : ContentPage
    {
        public RSS()
        {
            Title = "Feed";
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}