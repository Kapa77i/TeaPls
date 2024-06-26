﻿using System;
using System.Collections.Generic;
using TeaPls.Models;
using TeaPls.Services;
using Xamarin.Forms;

namespace TeaPls.Views
{
    public partial class AboutPage : ContentPage
    {
        // Arvot random teehen
        private readonly string[] brands = { "Janne & Students", "Leafy Greens", "Soothing Sips", "Steeped Delights", "Aroma Blends", "Herbal Heaven", "Bold Brews", "Wild Teas", "Spice & Tonic", "Caffeine Buzz", "Minty Mixtures" };
        private readonly string[] colors = { "Black", "Green", "Yellow", "White", "Oolong", "Red", "Herbal", "Pu-erh", "Matcha", "Chai" };
        private readonly string[] adjectives = { "English Breakfast", "Vibrant", "Rich", "Calming", "Cheerful", "Elegant", "Satisfyer", "Vintage", "Early", "Midnight", "Dawn", "Noon", "Sunset" };

        public AboutPage()
        {
            InitializeComponent();
            RandomTeaGenerator();

        }
        private void RandomTeaButton(object sender, EventArgs e)
        {
            //Tämä on vain nappia varten, joka luo uuden nimen teelle..
            RandomTeaGenerator();
        }
        private void RandomTeaGenerator()
        {
            //Arvotaan randomi väri ja adjektiivi
            Random random = new Random();
            int RandomBrand = random.Next(0, brands.Length);
            int RandomColor = random.Next(0, colors.Length);
            int RandomAdjective = random.Next(0, adjectives.Length);
            //muodostetaan noilla randomeilla uusi tee ja syötetään se frinttiin HotTea nimiseen elementtiin "frontissa".
            randomBrand.Text = $"Brand: {brands[RandomBrand]}";
            randomTea.Text = $"Product: {adjectives[RandomAdjective]} {colors[RandomColor]} Tea.";

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            List<TeaAforism> teaAforisms= new List<TeaAforism>();
            List<string> teaAforismId = new List<string>();

            teaAforisms = await MongoService.GetAllAforisms();

            foreach (var item in teaAforisms)
            {
                var id = item._id;
                teaAforismId.Add(id);
            }


            Random random = new Random();
            int randomId = random.Next(teaAforismId.Count);
            string randomString = teaAforismId[randomId];

            //int rnd = teaListId[randomId];


            while (randomString == randomIdAforism.Text)
            {
                randomId = random.Next(teaAforismId.Count);
                randomString = teaAforismId[randomId];
            }

            TeaAforism aforism = new TeaAforism();
            aforism = await MongoService.GetAforismById(new Guid(randomString));
            randomIdAforism.Text = $"{aforism._id}";
            randomPerson.Text = $"{aforism.Text}";
            randomAforims.Text = $"{aforism.Description}";

        }
    }
}
