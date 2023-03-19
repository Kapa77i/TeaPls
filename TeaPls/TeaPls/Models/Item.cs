using SQLite;
using System;
using Xamarin.Forms;

namespace TeaPls.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public ImageSource PhotoSource { get; set; }
        public double Rating { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}