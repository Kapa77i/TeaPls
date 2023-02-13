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
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        public string PhotoPath { get; private set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();


            takePhoto.Clicked += async (sender, args) =>
            {
                await TakePhotoAsync();
               
            };

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
    }
}