using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Notishare.Clipboard;
using Notishare.Helper;
using Notishare.ViewModel;
using NotiShareModel.DataTypes;
using NotiShareModel.HttpWorker;


namespace Notishare.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ClipboardListControl clipboardListView;
        private NotificationListControl notificationListView;
        private int userId;

        private ClipboardManager windowClipboardManager;
        private string lastClipboard = string.Empty;

        public MainWindow(int userId)
        { 
            InitializeComponent();
            this.userId = userId;
            
        }


        protected override async void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var registerDevice = new RegisterDeviceObject
            {
                DeviceId = IdHelper.GetDeviceId(),
                UserId = userId,
                DeviceType = 2,
                DeviceName = Environment.MachineName
            };
            var result = await HttpWorker.Instance.RegisterDevice(registerDevice);
            clipboardListView = new ClipboardListControl(userId, result.UserDeviceDbId, SubscribeEvent, UnsubcribeEvent);
            notificationListView = new NotificationListControl(userId, result.UserDeviceDbId);
            ContentControl.Content = clipboardListView;
            windowClipboardManager = new ClipboardManager(this);
            SubscribeEvent();
        }

        private void ButtonBaseNotification_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = notificationListView;
        }

        private void ButtonBaseClipboard_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = clipboardListView;
        }



        private void SubscribeEvent()
        {
            windowClipboardManager.ClipboardChanged += ClipboardChanged;
        }


        private void UnsubcribeEvent()
        {
            windowClipboardManager.ClipboardChanged -= ClipboardChanged;
        }

        private void ClipboardChanged(object sender, EventArgs e)
        {

            
            var currentClipboard = System.Windows.Clipboard.GetText();
            if (!currentClipboard.Equals(lastClipboard))
            {
                var clipboardContext = clipboardListView.DataContext as ClipboardViewMovel;
                var clipboardData = new ClipboardData
                {
                    ClipboardStringData = System.Windows.Clipboard.GetText(),
                    DataType = 1,
                    DatetimeCreation = DateTime.Now
                };
                clipboardContext?.SendClipboard(JsonConvert.SerializeObject(clipboardData));
                lastClipboard = currentClipboard;
            }
        }
    }
}
