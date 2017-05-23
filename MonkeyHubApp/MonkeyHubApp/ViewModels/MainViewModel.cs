using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using MonkeyHubApp.Models;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private const string BaseUrl = "https://monkey-hub-api.azurewebsites.net/api/";

        public async Task<List<Tag>> GetTagsAsyng()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}Tags").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<Tag>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }

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

        public MainViewModel()
        {
            Resultados = new ObservableCollection<Tag>();

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

                var tagsRetornadasDoServico = await GetTagsAsyng();

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
