﻿using System;
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

        //void Map_MapClicked(System.Object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        //{
        //    DisplayAlert("Coordinates", $"Lat: {e.Position.Latitude}, Long: {e.Position.Longitude}", "OK");
        //}
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

        //async Task LoadPhotoAsync(FileResult photo)
        //{
        //    // canceled
        //    if (photo == null)
        //    {
        //        photo = null;
        //        return;
        //    }
        //    // save the file into local storage
        //    var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
        //    using (var stream = await photo.OpenReadAsync())
        //    using (var newStream = File.OpenWrite(newFile))
        //        await stream.CopyToAsync(newStream);

        //    PhotoPath = newFile;
        //}


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
