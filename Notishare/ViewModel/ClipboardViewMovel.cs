using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Newtonsoft.Json;
using Notishare.LocalModel;
using Notishare.Ws;
using NotiShareModel.DataTypes;

namespace Notishare.ViewModel
{
    public class ClipboardViewMovel:BaseViewModel
    {
        private WebSocket socket;


        private int userDeviceDbId;

        private int userDbId;

        public ClipboardViewMovel(int userDeviceDbId, int userDbId)
        {
  
            this.userDeviceDbId = userDeviceDbId;
            this.userDbId = userDbId;
            ClipboardList = new ObservableCollection<LocalClipboard>();
            socket = new WebSocket("clipboardSocket", 3032, userDeviceDbId, this.userDbId, 2, PutClipboardToList); 
            socket.Init();
        }


        private ObservableCollection<LocalClipboard> list;

        public ObservableCollection<LocalClipboard> ClipboardList
        {
            get { return list; }
            set
            {
                list = value;
                OnPropertyChanged();
            }
        }


        private void PutClipboardToList(string message)
        {
            var clipboard = new LocalClipboard
            {
                ClipboardText = message
            };
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClipboardList.Add(clipboard);
            }, DispatcherPriority.DataBind);
        }

        internal void SendClipboard(string text)
        {
            socket.SendData(text);
        }
    }
}
