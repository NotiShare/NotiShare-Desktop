﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Notishare.Clipboard;
using Notishare.Helper;
using Notishare.Model.DataTypes;
using Notishare.Model.HttpWorker;


namespace Notishare.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ClipboardListControl clipboardListView;
        private NotificationListControl notificationListView;
        private string email;

        public MainWindow(string email)
        { 
            InitializeComponent();
            this.email = email;
        }


        protected override async void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var registerDevice = new RegisterDeviceObject
            {
                DeviceId = IdHelper.GetDeviceId(),
                Email = email,
                DeviceType = "win"
            };
            var result = await HttpWorker.Instance.RegisterDevice(registerDevice);
            clipboardListView = new ClipboardListControl(registerDevice.DeviceId, result.UserDbId, result.DeviceDbId);
            notificationListView = new NotificationListControl(registerDevice.DeviceId, result.UserDbId, result.DeviceDbId);
            ContentControl.Content = clipboardListView;
            var windowClipboardManager = new ClipboardManager(this);
            windowClipboardManager.ClipboardChanged += ClipboardChanged;
        }

        private void ButtonBaseNotification_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = notificationListView;
        }

        private void ButtonBaseClipboard_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = clipboardListView;
        }


        private void ClipboardChanged(object sender, EventArgs e)
        {

            if (System.Windows.Clipboard.ContainsText())
            {
                Debug.WriteLine(System.Windows.Clipboard.GetText());
            }
        }
    }
}
