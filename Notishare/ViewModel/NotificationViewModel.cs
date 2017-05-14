using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }



        private List<NotificationList> list;

        public List<NotificationList> NotificationList
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

        }
    }
}
