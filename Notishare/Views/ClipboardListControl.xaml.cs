﻿using System;
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
    /// Interaction logic for ClipboardList.xaml
    /// </summary>
    public partial class ClipboardListControl : UserControl
    {

        private Action subcribeEvent;

        private Action unsubcribeEvent;


        public ClipboardListControl(int userDb, int userDeviceDb, Action subcribeEvent, Action unsubcribeEvent)
        {
            InitializeComponent();
            DataContext = new ClipboardViewMovel(userDeviceDb, userDb);
            this.subcribeEvent = subcribeEvent;
            this.unsubcribeEvent = unsubcribeEvent;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            unsubcribeEvent.Invoke();
            var context = DataContext as ClipboardViewMovel;
            try
            {
                var currentString = context?.ClipboardList[ClipboardCurrentList.SelectedIndex];
                if (currentString != null)
                {
                    System.Windows.Clipboard.SetText(currentString.ClipboardText);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }

            subcribeEvent.Invoke();
        }
    }
}
