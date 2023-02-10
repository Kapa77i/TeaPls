using System;
using Xamarin.Forms;

namespace TeaPls.Views
{
    public partial class AboutPage : ContentPage
    {
        // Arvot random teehen
        private readonly string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple", "HotPink" };
        private readonly string[] adjectives = { "Vibrant", "Rich", "Calming", "Cheerful", "Elegant", "Satisfyer" };

        public AboutPage()
        {
            InitializeComponent();
            RandomTeaGenerator();

        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Tämä on vain nappia varten, joka luo uuden nimen teelle..
            RandomTeaGenerator();
        }
        private void RandomTeaGenerator()
        {
            //Arvotaan randomi väri ja adjektiivi
            Random random = new Random();
            int RandomColor = random.Next(0, colors.Length);
            int RandomAdjective = random.Next(0, adjectives.Length);
            //muodostetaan noilla randomeilla uusi tee ja syötetään se frinttiin HotTea nimiseen elementtiin "frontissa".
            HotTea.Text = $"{colors[RandomColor]} {adjectives[RandomAdjective]}";
        }

    }
}
