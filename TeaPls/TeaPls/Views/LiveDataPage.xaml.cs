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
        public AsyncCommand<Tea> DeleteCommand { get; set; }
        Tea _tea;


        public LiveDataPage()
        {
            InitializeComponent();
            BindingContext = this;

            Tea = new ObservableRangeCollection<Tea>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Tea>(Remove);
            DeleteCommand = new AsyncCommand<Tea>(Delete);

        }

        //public LiveDataPage(Tea tea)
        //{
        //    InitializeComponent();
        //    BindingContext = this;


        //}


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LiveDataListView.ItemsSource = await TeaService.GetTeas();
        }

        void Button_ClickedAsync(object sender, EventArgs e)
        {
            AddCommand.ExecuteAsync();
        }

        async void SwipeItem_Invoked(object sender, EventArgs e)
        {

            var item = sender as SwipeItem;
            var emp = item.CommandParameter as Tea;
            var result = await DisplayAlert("Delete", $"Delete {emp.Text} from Database?", "Yes", "No");

            if (result)
            {
                await TeaService.DeleteTea(emp);
                await Refresh();
            }
        }

        async void SwipeItem_Invoked_2(object sender, EventArgs e)
        {

            var item = sender as SwipeItem;
            var emp = item.CommandParameter as Tea;

            var text = await App.Current.MainPage.DisplayPromptAsync("Aforism of the tea", "Update the Aforism name", initialValue: $"{emp.Text}");
            var description = await App.Current.MainPage.DisplayPromptAsync("The Aforism", "Update the Aforism", initialValue: $"{emp.Description}");

            emp.Text = text;
            emp.Description = description;

            await TeaService.UpdateTea(emp);
            await Refresh();

        }

        async Task Add()
        {
            var text = await App.Current.MainPage.DisplayPromptAsync("Aforism of the tea", "Name of Aforism");
            var description = await App.Current.MainPage.DisplayPromptAsync("The Aforism", "Tell me your aforism");
            await TeaService.AddTea(text, description);
            await Refresh();

            //var route = $"{nameof(ItemDetailPage)}?Name=Motz";
            //await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Tea tea)
        {
            await TeaService.RemoveTea(tea.Id);
            //await TeaService.RemoveTea(tea.Id);
            await Refresh();
        }

        async Task Delete(Tea tea)
        {
            await TeaService.DeleteTea(tea);
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