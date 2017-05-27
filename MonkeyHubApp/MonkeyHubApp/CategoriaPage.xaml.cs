using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MonkeyHubApp.ViewModels;
using MonkeyHubApp.Models;

namespace MonkeyHubApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriaPage : ContentPage
    {
        public CategoriaPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            (BindingContext as CategoriaViewModel)?.LoadAsync(); //Método para carregar a tela.
            base.OnAppearing();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var content = (sender as ListView).SelectedItem as Content;
            (BindingContext as CategoriaViewModel)?.ShowContentCommand.Execute(content);
        }
    }
}