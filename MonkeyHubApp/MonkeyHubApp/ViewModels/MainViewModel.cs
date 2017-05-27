using System.Collections.ObjectModel;
using Xamarin.Forms;
using MonkeyHubApp.Models;
using MonkeyHubApp.Services;

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
        public ObservableCollection<Tag> Resultados { get; }

        //Não precisa ser Binding, pois a instância inicial não mudará.
        public Command SearchCommand { get; } //get only, Só è modificada no construtor.

        public Command AboutCommand { get; }

        public Command<Tag> ShowCategoriaCommand { get; }

        private readonly IMonkeyHubAppService _MonkeyHubAppService;

        public MainViewModel(IMonkeyHubAppService MonkeyHubAppService)
        {
            _MonkeyHubAppService = MonkeyHubAppService;

            Resultados = new ObservableCollection<Tag>();

            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);

            AboutCommand = new Command(ExecuteAboutCommand);

            ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);
        }

        private async void ExecuteShowCategoriaCommand(Tag tag)
        {
            await PushAsync<CategoriaViewModel>(_MonkeyHubAppService, tag);
        }

        async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        async void ExecuteSearchCommand()
        {
            //await Task.Delay(2000);

            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp",
                 $"Você pesquisou por '{SearchTerm}'?", "Sim", "Não");

            if (resposta)
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Obrigado.", "Ok");

                var tagsRetornadasDoServico = await _MonkeyHubAppService.GetTagsAsync();

                Resultados.Clear();
                if (tagsRetornadasDoServico != null)
                {
                    foreach (var tag in tagsRetornadasDoServico)
                    {
                        Resultados.Add(tag);
                    }
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Reporte seu erro.", "Ok");
                Resultados.Clear();
            }
        }

        bool CanExecuteSearchCommand()
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
        }

    }
}
