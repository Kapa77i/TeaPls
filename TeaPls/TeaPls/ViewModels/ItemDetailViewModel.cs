using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TeaPls.Models;
using Xamarin.Forms;

namespace TeaPls.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private double latitude;
        private double longitude;
        private double rating;
        public string Id { get; set; }
        private ImageSource _photoSource;
        public ImageSource PhotoSource
        {
            get { return _photoSource; }
            set { SetProperty(ref _photoSource, value); }
        }
        public double Longitude
        {
            get => longitude;
            set => SetProperty(ref longitude, value);
        }
        public double Latitude
        {
            get => latitude;
            set => SetProperty(ref latitude, value);
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public double Rating
        {
            get => rating;
            set => SetProperty(ref rating, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                PhotoSource = item.PhotoSource;
                Rating = item.Rating;
                Longitude = item.Longitude;
                Latitude = item.Latitude;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
