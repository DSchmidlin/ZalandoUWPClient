using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Zalando.Client.Commands;
using Zalando.Client.Notification;
using Zalando.Dto;
using Zalando.Services.Contracts;
using Zalando.Services.Extensions;

namespace Zalando.Client.ViewModels
{
    public class SearchPageViewModel : NotificationBase
    {
        private string _search;
        private IEnumerable<FacetType> _facetTypes;
        private string _selectedFacet;
        private FacetType _genderFacetType;
        private bool _isLoading;

        public delegate void SearchAction(IEnumerable<FacetType> facetTypes);
        public event SearchAction SearchParametersSelected;        

        public SearchPageViewModel()
        {
            IsLoading = true;

            PossibleGenders = new List<string>();

            _genderFacetType = new FacetType
            {
                Filter = Constants.GenderFacetName,
                Facets = new[] { new Facet { Key = Constants.GenderMale, DisplayName = Constants.GenderMale } }
            };

            Matches = new ObservableCollection<string>();
            SearchCommand = new SimpleCommand(SearchCommandExecuted, CanSearchCommandExecute);
        }        

        public ICommand SearchCommand { get; private set; }

        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                RaisePropertyChanged(nameof(SearchCommand));

                SearchFacets(_search);
            }
        }

        public IEnumerable<string> PossibleGenders { get;  private set; }

        public string SelectedGender
        {
            get { return _genderFacetType.Facets.First().DisplayName; }
            set
            {
                _genderFacetType.Facets.First().DisplayName = value;
                _genderFacetType.Facets.First().Key = value;
            }
        }

        public FacetType GenderFacetType
        {
            get { return _genderFacetType; }
            set
            {
                _genderFacetType = value;

                RaisePropertyChanged(nameof(GenderFacetType));
            }
        }

        public string SelectedFacet
        {
            get { return _selectedFacet; }
            set
            {
                _selectedFacet = value;

                var matchingFacetTypes = _facetTypes.FilterByExactName(_selectedFacet);
                IssueSearchEvent(matchingFacetTypes);
            }
        }

        public ObservableCollection<string> Matches { get; private set; }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;

                RaisePropertyChanged(nameof(IsLoading));
            }
        }

        public async Task InitializeAsync(IFacetsService facetsService)
        {

            _facetTypes = await facetsService.GetFacetTypesAsync();

            PossibleGenders = new List<string> { Constants.GenderMale, Constants.GenderFemale };
            IsLoading = false;

            RaisePropertyChanged(nameof(PossibleGenders));
            RaisePropertyChanged(nameof(SelectedGender));
        }


        private void SearchCommandExecuted()
        {
            var matchingFacetTypes = _facetTypes.FilterByPartialName(Search);
            IssueSearchEvent(matchingFacetTypes);
        }

        private bool CanSearchCommandExecute()
        {
            return !string.IsNullOrWhiteSpace(Search) && !IsLoading;
        }

        private void SearchFacets(string searchKeyword)
        {
            Matches.Clear();
            var matchingFacets = _facetTypes.SelectMany(x => x.Facets.Where(y => y.DisplayName.ToLower()
                                                                                        .Contains(searchKeyword.ToLower())))
                                            .Select(x => x.DisplayName)
                                            .Distinct();

            foreach( var facet in matchingFacets)
            {
                Matches.Add(facet);
            }
        }

        private void IssueSearchEvent(IEnumerable<FacetType> facetTypes)
        {
            if (SearchParametersSelected != null)
            {                                
                SearchParametersSelected(facetTypes.Concat( new[] { _genderFacetType }));
            }
        }
    }
}
