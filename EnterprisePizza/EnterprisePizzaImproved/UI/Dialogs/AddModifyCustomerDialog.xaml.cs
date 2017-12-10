using EnterprisePizzaImproved.Entities;
using System.Windows;

namespace EnterprisePizzaImproved
{
    /// <summary>
    /// Interaction logic for AddModifyCustomerDialog.xaml
    /// </summary>
    public partial class AddModifyCustomerDialog : Window
    {
        public AddModifyCustomerDialog(Customer customer = null)
        {
            InitializeComponent();
            Customer = customer ?? new Customer();
            DataContext = Customer;
        }

        public Customer Customer { get; set; }

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
