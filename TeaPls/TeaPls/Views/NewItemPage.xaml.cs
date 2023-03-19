using System;
using System.Collections.Generic;
using System.ComponentModel;
using TeaPls.Models;
using TeaPls.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System.Xml;

namespace TeaPls.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        public string PhotoPath { get; private set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();

            mySlider.ValueChanged += OnSliderValueChanged;


            takePhoto.Clicked += async (sender, args) =>
            {
                await TakePhotoAsync();

            };

        }

        void Slider_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / 100); mySlider.Value = newStep * 100; lblText.Text = mySlider.Value.ToString(); lblText.TranslateTo(mySlider.Value * ((mySlider.Width - 40) / mySlider.Maximum), 0, 100);
        }
        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newValue = e.NewValue;
            
        }
        void Map_MapClicked(System.Object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            var newItemViewModel = BindingContext as NewItemViewModel;
            newItemViewModel.Latitude = e.Position.Latitude;
            newItemViewModel.Longitude = e.Position.Longitude;
            DisplayAlert("Alert", "Coordinates saved", "OK");
        }

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {photo}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }


        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                photo = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;

            // Set the PhotoSource property of the ViewModel
            var viewModel = BindingContext as NewItemViewModel;
            viewModel.PhotoSource = ImageSource.FromFile(PhotoPath);
        }
    }
}
