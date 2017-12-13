using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Notishare.LocalModel;
using NotiShareModel.DataTypes;
using Notishare.ViewModel;

namespace Notishare.Views
{
    /// <summary>
    /// Interaction logic for NotificationList.xaml
    /// </summary>
    public partial class NotificationListControl : UserControl
    {
        public NotificationListControl(int userDb, int userDeviceDb)
        {
            InitializeComponent();
            DataContext = new NotificationViewModel(userDeviceDb, userDb);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var smth = button?.DataContext as LocalNotification;
            (DataContext as NotificationViewModel)?.NotificationList.Remove(smth);
        }
    }
}
