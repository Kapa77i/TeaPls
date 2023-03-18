using MvvmHelpers.Commands;
using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeaPls.Models;
using TeaPls.Services;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using TeaPls.ViewModels;

namespace TeaPls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AforismPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        public ObservableRangeCollection<TeaAforism> Tea { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand AddCommand { get; set; }
       // public AsyncCommand<TeaAforism> RemoveCommand { get; set; }
        public AsyncCommand<TeaAforism> DeleteCommand { get; set; }
        TeaAforism _tea;

        AforismModel viewModel;

        public AforismPage()
        {
            InitializeComponent();

            viewModel = new AforismModel();
            BindingContext = viewModel;
            BindingContext = this;

           

            Tea = new ObservableRangeCollection<TeaAforism>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            //RemoveCommand = new AsyncCommand<TeaAforism>(Remove);
            DeleteCommand = new AsyncCommand<TeaAforism>(Delete);

            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

        
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AforismListView.ItemsSource = await MongoService.GetAllAforisms();
        }

        void Button_ClickedAsync(object sender, EventArgs e)
        {
            AddCommand.ExecuteAsync();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async Task Add()
        {
            var text = await App.Current.MainPage.DisplayPromptAsync("Aforism of the tea", "Name of Aforism");
            var description = await App.Current.MainPage.DisplayPromptAsync("The Aforism", "Tell me your aforism");
            await MongoService.CreateAf(text, description);
            await Refresh();

            //var route = $"{nameof(ItemDetailPage)}?Name=Motz";
            //await Shell.Current.GoToAsync(route);
        }

        //async Task Remove(TeaAforism tea)
        //{
        //    //await TeaService.RemoveTea(tea.Id);
        //    //await TeaService.RemoveTea(tea.Id);
        //    await Refresh();
        //}

        async Task Delete(TeaAforism tea)
        {
            await MongoService.DeleteAforism(tea);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Tea.Clear();
            AforismListView.ItemsSource = await MongoService.GetAllAforisms();
            IsBusy = false;
        }

        async void SwipeItem_Invoked(object sender, EventArgs e)
        {

            var item = sender as SwipeItem;
            var emp = item.CommandParameter as TeaAforism;
            var result = await DisplayAlert("Delete", $"Delete {emp.Text} from Database?", "Yes", "No");

            if (result)
            {
                await MongoService.DeleteAforism(emp);
                await Refresh();
            }
        }

        async void SwipeItem_Invoked_2(object sender, EventArgs e)
        {

            var item = sender as SwipeItem;
            var emp = item.CommandParameter as TeaAforism;

            var text = await App.Current.MainPage.DisplayPromptAsync("Aforism of the tea", "Update the Aforism name", initialValue: $"{emp.Text}");
            var description = await App.Current.MainPage.DisplayPromptAsync("The Aforism", "Update the Aforism", initialValue: $"{emp.Description}");

            emp.Text = text;
            emp.Description = description;

            await MongoService.UpdateAforism(emp);
            await Refresh();

        }
    }
}
