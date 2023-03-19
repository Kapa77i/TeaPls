//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using TeaPls.Models;
//using TeaPls.ViewModels;
//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;
//using Plugin.Media;
//using System.Threading.Tasks;
//using Xamarin.Essentials;
//using System.IO;

//namespace TeaPls.Views
//{
//    public partial class NewItemPage : ContentPage
//    {
//        public Item Item { get; set; }
//        public string PhotoPath { get; private set; }

//        public NewItemPage()
//        {
//            InitializeComponent();
//            BindingContext = new NewItemViewModel();


//            takePhoto.Clicked += async (sender, args) =>
//            {
//                await TakePhotoAsync();

//            };

//        }

//        void Map_MapClicked(System.Object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
//        {
//            DisplayAlert("Coordinates", $"Lat: {e.Position.Latitude}, Long: {e.Position.Longitude}", "OK");
//        }
//        async Task TakePhotoAsync()
//        {
//            try
//            {
//                var photo = await MediaPicker.CapturePhotoAsync();
//                await LoadPhotoAsync(photo);
//                Console.WriteLine($"CapturePhotoAsync COMPLETED: {photo}");
//            }
//            catch (FeatureNotSupportedException fnsEx)
//            {
//                // Feature is not supported on the device
//            }
//            catch (PermissionException pEx)
//            {
//                // Permissions not granted
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
//            }
//        }

//        async Task LoadPhotoAsync(FileResult photo)
//        {
//            // canceled
//            if (photo == null)
//            {
//                photo = null;
//                return;
//            }
//            // save the file into local storage
//            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
//            using (var stream = await photo.OpenReadAsync())
//            using (var newStream = File.OpenWrite(newFile))
//                await stream.CopyToAsync(newStream);

//            PhotoPath = newFile;
//        }
//    }
//}

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

namespace TeaPls.Views
{
    public partial class NewItemPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string photoPath;
        public string PhotoPath
        {
            get { return photoPath; }
            private set
            {
                if (photoPath != value)
                {
                    photoPath = value;
                    OnPropertyChanged(nameof(PhotoPath));
                }
            }
        }


        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
            //BindingContext = this;
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
        }

        void takePhoto_Clicked(object sender, EventArgs e)
        {
            Task.Run(TakePhotoAsync);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
