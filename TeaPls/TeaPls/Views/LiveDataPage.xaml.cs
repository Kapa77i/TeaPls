using MvvmHelpers.Commands;
using MvvmHelpers;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using TeaPls.Models;
using TeaPls.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;
using TeaPls.ViewModels;

namespace TeaPls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LiveDataPage : ContentPage
    {

        public ObservableRangeCollection<Tea> Tea { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand AddCommand { get; set; }
        public AsyncCommand<Tea> RemoveCommand { get; set; }

        public LiveDataPage()
        {
            InitializeComponent();
            BindingContext = this;

            Tea = new ObservableRangeCollection<Tea>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Tea>(Remove);

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LiveDataListView.ItemsSource = await TeaService.GetTeas();
        }

        void Button_ClickedAsync(object sender, EventArgs e)
        {
            AddCommand.ExecuteAsync();
        }



        async Task Add()
        {
            var text = await App.Current.MainPage.DisplayPromptAsync("Text", "Name of Tea");
            var description = await App.Current.MainPage.DisplayPromptAsync("Description", "Tell me about this tea");
            await TeaService.AddTea(text, description);
            await Refresh();

            //var route = $"{nameof(ItemDetailPage)}?Name=Motz";
            //await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Tea tea)
        {
            await TeaService.RemoveTea(tea.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Tea.Clear();   
            LiveDataListView.ItemsSource = await TeaService.GetTeas();
            IsBusy = false;
        }

        
    }
}