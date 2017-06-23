using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Zalando.Client.Notification;
using Zalando.Client.Sources;
using Zalando.Dto;
using Zalando.Services.Contracts;

namespace Zalando.Client.ViewModels
{
    public class ResultsPageViewModel : NotificationBase
    {
        private IncrementalLoadingCollection<ArticleSource,Article> _articles;
        private bool _isLoading;
        private string _noResultsMessage;

        public ResultsPageViewModel()
        {
            NoResultsMessage = string.Empty;
        }

        public void Load(IArticlesService articleService, IEnumerable<FacetType> faceTypes)
        {
            IsLoading = true;

            var incrementalSource = new ArticleSource(articleService, faceTypes);

            _articles = new IncrementalLoadingCollection<ArticleSource, Article>(incrementalSource, 10, 
                LoadingStarted, LoadingComplete, LoadingError);

            RaisePropertyChanged(nameof(Articles));
        }

        public ObservableCollection<Article> Articles
        {
            get { return _articles; }
        }

        private void LoadingStarted()
        {
            //happens each time loading is called
        }

        private void LoadingComplete()
        {
            IsLoading = false;

            if(!Articles.Any())
            {
                NoResultsMessage = "No articles found";
            }
        }

        private void LoadingError(Exception ex)
        {
            IsLoading = false;
            NoResultsMessage = ex.Message;
        }

        public string NoResultsMessage
        {
            get { return _noResultsMessage; }
            set
            {
                _noResultsMessage = value;

                RaisePropertyChanged(nameof(NoResultsMessage));
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;

                RaisePropertyChanged(nameof(IsLoading));
            }
        }
    }
}
