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
using Notishare.ViewModel;

namespace Notishare.Views
{
    /// <summary>
    /// Interaction logic for NotificationList.xaml
    /// </summary>
    public partial class NotificationListControl : UserControl
    {
        public NotificationListControl(string id, string userDb, string deviceDb)
        {
            InitializeComponent();
            DataContext = new NotificationViewModel(id , deviceDb, userDb);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
             
        }
    }
}
