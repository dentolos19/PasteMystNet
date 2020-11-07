using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PasteMystNet;
using PasteMystTest.Bindings;

namespace PasteMystTest.Graphics
{

    public partial class WnMain
    {

        public WnMain()
        {
            InitializeComponent();
        }

        private async void GetUserProfile(object sender, RoutedEventArgs args)
        {
            var username = UsernameBox.Text;
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Enter your username to get your profile.", "PasteMyst.NET");
                return;
            }
            if (!await PasteMystUser.UserExistsAsync(username))
            {
                MessageBox.Show("User does not exist in database.", "PasteMyst.NET");
                return;
            }
            var user = await PasteMystUser.GetUserAsync(username);
            if (user == null)
            {
                MessageBox.Show("Unable to retrieve user from database.", "PasteMyst.NET");
                return;
            }
            AvatarImage.Source = new BitmapImage(new Uri(user.AvatarUrl));
            UserPropertyList.Items.Clear();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(user))
            {
                var item = new PropertyItemBinding
                {
                    Property = property.DisplayName,
                    Value = property.GetValue(user)?.ToString() ?? "Unable to get value."
                };
                UserPropertyList.Items.Add(item);
            }
        }

        private void CopyUserValue(object sender, RoutedEventArgs args)
        {
            if (UserPropertyList.SelectedItem is PropertyItemBinding binding)
                Clipboard.SetText(binding.Value);
        }

        private async void DetectLanguage(object sender, RoutedEventArgs args)
        {
            var input = LanguageInputBox.Text;
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Enter input for detecting languages.", "PasteMyst.NET");
                return;
            }
            var method = (string)((ComboBoxItem)IdentificationMethodBox.SelectedItem).Tag;
            PasteMystLanguage language;
            switch (method)
            {
                case "IBN":
                    language = await PasteMystLanguage.IdentifyByNameAsync(input);
                    break;
                case "IBE":
                    language = await PasteMystLanguage.IdentifyByExtensionAsync(input);
                    break;
                default:
                    MessageBox.Show("Select a valid method for detecting language.", "PasteMyst.NET");
                    return;
            }
            if (language == null)
            {
                MessageBox.Show("Language does not exist in database.", "PasteMyst.NET");
                return;
            }
            LanguagePropertyList.Items.Clear();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(language))
            {
                var item = new PropertyItemBinding
                {
                    Property = property.DisplayName,
                    Value = property.GetValue(language)?.ToString() ?? "Unable to get value."
                };
                LanguagePropertyList.Items.Add(item);
            }
        }

        private void CopyLanguageValue(object sender, RoutedEventArgs args)
        {
            if (LanguagePropertyList.SelectedItem is PropertyItemBinding binding)
                Clipboard.SetText(binding.Value);
        }

        private void ToggleCredentialBoxes(object sender, RoutedEventArgs args)
        {
            if (CredentialOption.IsChecked == true)
            {
                UsernameCredentialBox.IsEnabled = true;
                KeyCredentialBox.IsEnabled = true;
            }
            else
            {
                UsernameCredentialBox.IsEnabled = false;
                KeyCredentialBox.IsEnabled = false;
            }
        }

    }

}