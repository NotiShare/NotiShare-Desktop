using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Newtonsoft.Json;
using Notishare.LocalModel;
using NotiShareModel.DataTypes;
using Notishare.Ws;

namespace Notishare.ViewModel
{
    public class NotificationViewModel: BaseViewModel
    {

        private WebSocket socket;

        private int userDeviceDbId;

        private int userDbId;



        public NotificationViewModel(int userDeviceDbId, int userDbId)
        {
            this.userDeviceDbId = userDeviceDbId;
            this.userDbId = userDbId;
            socket = new WebSocket("notificationSocket", 3031, userDeviceDbId, userDbId, 2, PutNotificationToList);
            socket.Init();
            NotificationList = new ObservableCollection<LocalNotification>();
        }



        private ObservableCollection<LocalNotification> list;

        public ObservableCollection<LocalNotification> NotificationList
        {
            get { return list; }
            set
            {
                list = value;
                OnPropertyChanged();
            }
        }


        private void PutNotificationToList(string message)
        {
            var resultObject = JsonConvert.DeserializeObject<NotificationObject>(message);
            var notification = new LocalNotification
            {
                NotificationIcon = Base64StringToBitmap(resultObject.ImageBase64),
                Text = resultObject.NotificationText,
                Title = resultObject.Title
            };
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    NotificationList.Add(notification);
                }, DispatcherPriority.DataBind);
        }


        public static BitmapImage Base64StringToBitmap(string base64String)
        {
            byte[] binaryData = Convert.FromBase64String(base64String);

            var image = new BitmapImage();
            using (var mem = new MemoryStream(binaryData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
