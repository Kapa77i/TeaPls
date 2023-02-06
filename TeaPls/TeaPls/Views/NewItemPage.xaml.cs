using System;
using System.Collections.Generic;
using System.ComponentModel;
using TeaPls.Models;
using TeaPls.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeaPls.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}