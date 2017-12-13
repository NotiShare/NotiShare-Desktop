using System.Windows.Media.Imaging;

namespace Notishare.LocalModel
{
    public class LocalNotification
    {

        public BitmapImage NotificationIcon { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }
}
