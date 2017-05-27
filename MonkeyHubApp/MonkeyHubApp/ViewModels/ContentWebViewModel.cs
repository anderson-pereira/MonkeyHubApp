using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyHubApp.Models;

namespace MonkeyHubApp.ViewModels
{
    public class ContentWebViewModel : BaseViewModel
    {
        public Content Content { get; }

        public ContentWebViewModel(Content content)
        {
            Content = content;
        }
    }
}
