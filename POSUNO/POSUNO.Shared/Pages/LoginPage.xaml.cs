using POSUNO.Helpers;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace POSUNO.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = await ValidForm();
            if (!isValid)
            {
                return;
            }

            MessageDialog messageDialog = new MessageDialog("Vamos bien!", "Ok");
            await messageDialog.ShowAsync();
        }

        private async Task<bool> ValidForm()
        {
            MessageDialog messageDialog;

            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                messageDialog = new MessageDialog("Debes ingresar tu email.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (!RegexUtilities.IsValidEmail(EmailTextBox.Text))
            {
                messageDialog = new MessageDialog("Debes ingresar un email válido.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (string.IsNullOrEmpty(PasswordPasswordBox.Password))
            {
                messageDialog = new MessageDialog("Debes ingresar tu contraseña.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            return true;
        }
    }
}
