using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Runtime.CompilerServices;
using TeaPls.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TeaPls.Services;

namespace TeaPls.ViewModels
{
    public class AforismModel : INotifyPropertyChanged
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        string _text;
        [BsonElement("Text")]

        public string Text
        {
            get => _text; set
            {
                if (_text == value) return;

                _text = value;
                HandlePropertyChanged();

            }
        }

        string _description;
        [BsonElement("Description")]
        public string Description
        {
            get => _description; set
            {
                if (_description == value) return;

                _description = value;
                HandlePropertyChanged();

            }
        }

        string _complete;
        [BsonElement("Complete")]
        public string Complete
        {
            get => _complete; set
            {
                if (_complete == value) return;

                _complete = value;
                HandlePropertyChanged();

            }
        }

        DateTime _dueDate = DateTime.Today.AddDays(7);
        [BsonElement("DueDate")]
        public DateTime DueDate
        {
            get => _dueDate; set
            {
                if (_dueDate == value) return;

                _dueDate = value;
                HandlePropertyChanged();

            }
        }
        public ObservableCollection<string> Items { get; set; }
        public ObservableRangeCollection<TeaAforism> Tea { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand AddCommand { get; set; }
        // public AsyncCommand<TeaAforism> RemoveCommand { get; set; }
        public AsyncCommand<TeaAforism> DeleteCommand { get; set; }
        public bool IsBusy { get; private set; }
        public object AforismListView { get; private set; }

        TeaAforism _tea;

        AforismModel viewModel;


        void Button_ClickedAsync(object sender, EventArgs e)
        {
            AddCommand.ExecuteAsync();
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
            var teas = await MongoService.GetAllAforisms();
            Tea.AddRange(teas);
            IsBusy = false;
        }

        void HandlePropertyChanged([CallerMemberName]string propertyName ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
