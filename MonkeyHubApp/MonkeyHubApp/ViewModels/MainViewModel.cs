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
            set { SetProperty(ref _searchTerm, value); }
        }

        //Não precisa ser Binding, pois a instância inicial não mudará.
        public Command SearchCommand { get; } //get only, Só è modificada no construtor.

        public MainViewModel()
        {
            SearchCommand = new Command(ExecuteSearchCommand);
        }

        void ExecuteSearchCommand()
        {

        }

    }
}
