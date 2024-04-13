using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using TeaPls.Services;
using TeaPls.ViewModels;
using TeaPls.Views;
using TeaPls.Models;
using MvvmHelpers;
using System;
using System.Diagnostics;

namespace TeaPls.ViewModels
{

    public class LiveDataViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Tea> Tea { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand AddCommand { get; set; }
        public AsyncCommand LoadCommand { get; set; }
        public AsyncCommand<Tea> RemoveCommand { get; set; }


        public LiveDataViewModel()
        {

            //Tea = new ObservableRangeCollection<Tea>();

            //LoadCommand = new AsyncCommand(GetAllTeas);


            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Tea>(Remove);
        }

        async Task GetAllTeas()
        {
            await TeaService.GetTeas();
            await Refresh();
        }

        public void OnAppearing()
        {
            IsBusy = true;
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
            var teas = await TeaService.GetTeas();
            Tea.AddRange(teas);
            IsBusy = false;
        }

    }
}