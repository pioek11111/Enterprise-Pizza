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
using System.Windows.Shapes;
using EnterprisePizzaImproved.Entities;

namespace EnterprisePizzaImproved
{
    /// <summary>
    /// Interaction logic for ChooseTimeIntervalDialog.xaml
    /// </summary>
    public partial class ChooseTimeIntervalDialog : Window
    {
        public TimeInterval TimeInterval { get; set; }

        public ChooseTimeIntervalDialog()
        {
            InitializeComponent();
            TimeInterval = new TimeInterval() {From = DateTime.Now, To = DateTime.Now};
            DataContext = TimeInterval;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
