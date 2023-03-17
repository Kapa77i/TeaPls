using System;
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
            List<Tea> teaList = new List<Tea>();
            List<Int16> teaListId = new List<Int16>();

            teaList = await TeaService.GetTeasit();

            foreach (var item in teaList)
            {
                var id = item.Id;
                teaListId.Add((short)id);
            }


            Random random = new Random();
            int randomId = random.Next(teaListId.Count);
            int rnd = teaListId[randomId];


            while (rnd == int.Parse(randomIdAforism.Text))
            {
                randomId = random.Next(teaListId.Count);
                rnd = teaListId[randomId];
            }
            Tea tea = new Tea();
            tea = await TeaService.GetTea(rnd);
            randomIdAforism.Text = $"{tea.Id}";
            randomPerson.Text = $"{tea.Text}";
            randomAforims.Text = $"{tea.Description}";

            //Tea tea = new Tea();

            //if(tea == null)
            //{
            //    while (tea == null)
            //    {
            //        randomId = random.Next(1, 27);

            //        tea = await TeaService.GetTea(rnd);
            //        randomIdAforism.Text = $"{tea.Id}";
            //        randomPerson.Text = $"{tea.Text}";
            //        randomAforims.Text = $"{tea.Description}";
            //    }
            //}
           
            //else
            //{
            //    tea = await TeaService.GetTea(randomId);

            //    randomIdAforism.Text = $"{tea.Id}";
            //    randomPerson.Text = $"{tea.Text}";
            //    randomAforims.Text = $"{tea.Description}";
            //}

        }
    }
}
