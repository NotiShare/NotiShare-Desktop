using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Notishare.Clipboard;
using Notishare.ViewModel;

namespace Notishare.Views
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var windowClipboardManager = new ClipboardManager(this);
            windowClipboardManager.ClipboardChanged += ClipboardChanged;
        }

        private void ClipboardChanged(object sender, EventArgs e)
        {

            if (System.Windows.Clipboard.ContainsText())
            {
                Debug.WriteLine(System.Windows.Clipboard.GetText());
            }
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var context = DataContext as RegistrationViewModel;
            var password = sender as PasswordBox;
            if (context != null)
            {
                context.PasswordString = password?.Password;
            }
        }

        private void PasswordBox_OnPasswordRepeatChanged(object sender, RoutedEventArgs e)
        {
            var context = DataContext as RegistrationViewModel;
            var password = sender as PasswordBox;
            if (context != null)
            {
                context.PasswordRepeatString = password?.Password;
            }
        }
    }
}
