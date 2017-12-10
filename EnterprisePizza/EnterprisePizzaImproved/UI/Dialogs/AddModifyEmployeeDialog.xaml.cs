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
    /// Interaction logic for AddModifyEmployeeDialog.xaml
    /// </summary>
    public partial class AddModifyEmployeeDialog : Window
    {
        public Employee Employee { get; set; }

        public AddModifyEmployeeDialog(Employee employee = null)
        {
            InitializeComponent();
            Employee = employee ?? new Employee() {AvailableIntervals = new List<TimeInterval>()};
            DataContext = Employee;
        }

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddIntervalButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ChooseTimeIntervalDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                Employee.AvailableIntervals.Add(dialog.TimeInterval);
                DataContext = null;
                DataContext = Employee;
            }
        }

        private void RemoveIntervalButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            TimeInterval interval = (TimeInterval) button.DataContext;
            Employee.AvailableIntervals.Remove(interval);
            DataContext = null;
            DataContext = Employee;
        }
    }
}
