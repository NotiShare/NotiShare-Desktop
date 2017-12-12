using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Newtonsoft.Json;
using Notishare.Ws;
using NotiShareModel.DataTypes;

namespace Notishare.ViewModel
{
    public class ClipboardViewMovel:BaseViewModel
    {
        private WebSocket socket;

        private string id;

        private string deviceDbId;

        private string userDbId;

        public ClipboardViewMovel(string id, string deviceDbId, string userDbId)
        {
            this.id = id;
            this.deviceDbId = deviceDbId;
            this.userDbId = userDbId;
            ClipboardList = new ObservableCollection<ClipboardList>();
            socket = new WebSocket("clipboardSocket", 3032, id, deviceDbId, userDbId, "win", PutClipboardToList); 
            socket.Init();
        }


        private ObservableCollection<ClipboardList> list;

        public ObservableCollection<ClipboardList> ClipboardList
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
            var clipboard = new ClipboardList
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
