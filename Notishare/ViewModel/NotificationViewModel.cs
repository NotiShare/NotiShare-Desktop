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
using Notishare.Model.DataTypes;
using Notishare.Model.HttpWorker;

namespace Notishare.ViewModel
{
    public class NotificationViewModel: BaseViewModel
    {

        private WebSocket socket;

        private string id;

        private string deviceDbId;

        private string userDbId;



        public NotificationViewModel(string id, string deviceDbId, string userDbId)
        {
            this.id = id;
            this.deviceDbId = deviceDbId;
            this.userDbId = userDbId;
            socket = new WebSocket("notificationSocket", 3031, id, deviceDbId, userDbId, "win", PutNotificationToList);
            socket.Init();
            NotificationList = new ObservableCollection<NotificationList>();
        }



        private ObservableCollection<NotificationList> list;

        public ObservableCollection<NotificationList> NotificationList
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
            var notification = new NotificationList
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
