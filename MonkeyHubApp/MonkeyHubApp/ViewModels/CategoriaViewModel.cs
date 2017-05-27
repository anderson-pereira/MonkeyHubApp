﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonkeyHubApp.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using MonkeyHubApp.Services;

namespace MonkeyHubApp.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {
        private readonly IMonkeyHubAppService _monkeyHubApiService;
        private readonly Tag _tag;

        public ObservableCollection<Content> Contents { get; }

        public Command<Content> ShowContentCommand { get; }

        public CategoriaViewModel(IMonkeyHubAppService monkeyHubApiService, Tag tag)
        {
            _monkeyHubApiService = monkeyHubApiService;
            _tag = tag;

            Contents = new ObservableCollection<Content>();
            ShowContentCommand = new Command<Content>(ExecuteShowContentCommand);

            Title = "Eventos";
        }

        private async void ExecuteShowContentCommand(Content content)
        {
            await PushAsync<ContentWebViewModel>(content);
        }

        public override async Task LoadAsync()
        {
            var contents = await _monkeyHubApiService.GetContentsByTagIdAsync(_tag.Id);

            Contents.Clear();
            foreach (var content in contents)
            {
                Contents.Add(content);
            }
        }
    }
}