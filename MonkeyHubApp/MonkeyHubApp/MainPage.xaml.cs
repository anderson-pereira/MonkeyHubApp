using MonkeyHubApp.ViewModels;
using Xamarin.Forms;

namespace MonkeyHubApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            //A propriedade navigation vai ser nula, se não estiver dentro de uma navigation page.
            //Pode colocar qualquer página, até mesmo uma nova instância dela mesma.
            Navigation?.PushAsync(new MainPage());
        }

        private void ButtonModal_Clicked(object sender, System.EventArgs e)
        {
            //A propriedade navigation vai ser nula, se não estiver dentro de uma navigation page.
            //Pode colocar qualquer página, até mesmo uma nova instância dela mesma.
            Navigation?.PushAsync(new NavigationPage(new MainPage()));
        }

        private void ButtonVoltarModal_Clicked(object sender, System.EventArgs e)
        {
            Navigation?.PopModalAsync();
        }
    }
}
