using POSUNO.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System;
using POSUNO.Helpers;

namespace POSUNO.Dialogs
{
    public sealed partial class CustomerDialog : ContentDialog
    {
        public CustomerDialog(Customer customer)
        {
            InitializeComponent();
            Customer = customer;
            if (Customer.IsEdit)
            {
                TitleTextBlock.Text = $"Editar el cliente: {Customer.FullName}";
            }
            else
            {
                TitleTextBlock.Text = "Nuevo cliente";
            }
        }

        public Customer Customer { get; set; }

        private void CloseImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Customer.WasSaved = false;
            Hide();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Customer.WasSaved = false;
            Hide();
        }

        private async void SeveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = await ValidateFormAsync();
            if (!isValid)
            {
                return;
            }

            Customer.WasSaved = true;
            Hide();
        }

        private async Task<bool> ValidateFormAsync()
        {
            MessageDialog messageDialog;

            if (string.IsNullOrEmpty(Customer.FirstName))
            {
                messageDialog = new MessageDialog("Debes ingresar nombres del cliente.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (string.IsNullOrEmpty(Customer.LastName))
            {
                messageDialog = new MessageDialog("Debes ingresar apellidos del cliente.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (string.IsNullOrEmpty(Customer.Phonenumber))
            {
                messageDialog = new MessageDialog("Debes ingresar teléfono del cliente.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (string.IsNullOrEmpty(Customer.Address))
            {
                messageDialog = new MessageDialog("Debes ingresar dirección del cliente.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (string.IsNullOrEmpty(Customer.Email))
            {
                messageDialog = new MessageDialog("Debes ingresar email del cliente.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            if (!RegexUtilities.IsValidEmail(Customer.Email))
            {
                messageDialog = new MessageDialog("Debes ingresar un email válido.", "Error");
                await messageDialog.ShowAsync();
                return false;
            }

            return true;
        }
    }
}
