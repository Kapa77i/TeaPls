using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Runtime.CompilerServices;


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


        void HandlePropertyChanged([CallerMemberName]string propertyName ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
