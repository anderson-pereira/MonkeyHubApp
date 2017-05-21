using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _searchTerm;     

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (SetProperty(ref _searchTerm, value))
                    SearchCommand.ChangeCanExecute(); //Invoca o CanExecuteSearchCommand com todos os parâmetros.
            }
        }

        //Lista que pode ser carregada por demanda.
        public ObservableCollection<string> Resultados { get; }

        //Não precisa ser Binding, pois a instância inicial não mudará.
        public Command SearchCommand { get; } //get only, Só è modificada no construtor.

        public MainViewModel()
        {
            Resultados = new ObservableCollection<string>(new[] {"abc", "cde" });

            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
        }

        async void ExecuteSearchCommand()
        {
            //await Task.Delay(2000);

            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp",
                 $"Você pesquisou por '{SearchTerm}'?", "Sim", "Não");

            if (resposta)
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Obrigado.", "Ok");
                Resultados.Add("Sim");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Reporte seu erro.", "Ok");
                Resultados.Add("Não");
            }
        }

        bool CanExecuteSearchCommand()
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
            Resultados.Add("Não");
        }

    }
}
