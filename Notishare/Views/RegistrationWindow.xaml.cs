using System;
using System.Diagnostics;
using System.Windows;
using Notishare.Clipboard;

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

            // Initialize the clipboard now that we have a window soruce to use
            var windowClipboardManager = new ClipboardManager(this);
            windowClipboardManager.ClipboardChanged += ClipboardChanged;
        }

        private void ClipboardChanged(object sender, EventArgs e)
        {
            // Handle your clipboard update here, debug logging example:
            if (System.Windows.Clipboard.ContainsText())
            {
                Debug.WriteLine(System.Windows.Clipboard.GetText());
            }
        }
    }
}
