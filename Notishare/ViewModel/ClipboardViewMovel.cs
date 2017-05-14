using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notishare.Model.DataTypes;
using Notishare.Model.HttpWorker;

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
            socket = new WebSocket("clipboardSocket", 3032, id, deviceDbId, userDbId, "win", PutClipboardToList); 
            socket.Init();
        }


        private List<ClipboardList> list;

        public List<ClipboardList> ClipboardList
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
            
        }
    }
}
