using POSUNO.Models;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;


namespace POSUNO.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public User User { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            User = (User)e.Parameter;
            WelcomeTextBlock.Text = $"Bienvenid@: {User.FullName}";
            MyFrame.Navigate(typeof(CustomersPage));
        }

        private async void LogoutImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentDialogResult dialog = await ConfirmLeaveAsync();
            if (dialog == ContentDialogResult.Primary)
            {
                Frame.Navigate(typeof(LoginPage));
            }
        }

        private async Task<ContentDialogResult> ConfirmLeaveAsync()
        {
            ContentDialog confirmDialog = new ContentDialog
            {
                Title = "Confirmación",
                Content = "Estas seguro de salir?",
                PrimaryButtonText = "Sí",
                CloseButtonText = "No"
            };

            return await confirmDialog.ShowAsync();
        }

        private void CustomersNavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(CustomersPage));
        }

        private void ProductsNavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(ProductsPage));
        }
    }
}
