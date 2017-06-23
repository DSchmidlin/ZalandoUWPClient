using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Zalando.Client.Factories;
using Zalando.Client.ViewModels;
using Zalando.Dto;
using Zalando.Services.Contracts;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Zalando.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResultsPage : Page
    {
        public ResultsPage()
        {
            var container = ContainerFactory.GetContainer();

            ViewModel = new ResultsPageViewModel();

            this.DataContext = ViewModel;

            this.InitializeComponent();
        }

        public ResultsPageViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {           
            var facetTypes = e.Parameter as IEnumerable<FacetType>;

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
            var articleSearvice = container.Resolve<IArticlesService>();

            ViewModel.Load(articleSearvice, facetTypes);
        }
    }
}
