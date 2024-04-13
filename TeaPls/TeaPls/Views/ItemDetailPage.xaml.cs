using System.ComponentModel;
using TeaPls.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TeaPls.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ItemDetailViewModel viewModel)
            {
                var position = new Position(viewModel.Latitude, viewModel.Longitude);
                itemMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.5)));

                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = viewModel.Text,
                    Address = viewModel.Description
                };

                itemMap.Pins.Add(pin);
            }
        }
    }
}