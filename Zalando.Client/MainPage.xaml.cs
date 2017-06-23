using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Zalando.Client.Factories;
using Zalando.Client.ViewModels;
using Zalando.Services.Contracts;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Zalando.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {                                  
            ViewModel = new SearchPageViewModel();
            ViewModel.SearchParametersSelected += ViewModel_SearchParametersSelected;

            this.DataContext = ViewModel;

            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            this.InitializeComponent();
        }

        private void ViewModel_SearchParametersSelected(System.Collections.Generic.IEnumerable<Dto.FacetType> facetTypes)
        {
            this.Frame.Navigate(typeof(ResultsPage), facetTypes);
        }

        public SearchPageViewModel ViewModel { get; set; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }

            base.OnNavigatedTo(e);

            var container = ContainerFactory.GetContainer();
            var facetsService = container.Resolve<IFacetsService>();
            await ViewModel.InitializeAsync(facetsService);
        }
    }
}
