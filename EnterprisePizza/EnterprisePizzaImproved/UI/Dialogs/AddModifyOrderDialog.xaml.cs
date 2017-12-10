using EnterprisePizzaImproved.Entities;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace EnterprisePizzaImproved.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for AddModifyOrderDialog.xaml
    /// </summary>
    public partial class AddModifyOrderDialog : Window
    {
        public AddModifyOrderDialog(Order order = null, List<CustomizedProduct> customizedProducts = null, List<Employee> allDeliveryman = null,
                                    List<Customer> listOfCustomers = null, List<Topping> lOT = null, List<Product> lOP = null)
        {
            InitializeComponent();
            Order = order ?? new Order();
            List<CheckedListItem<CustomizedProduct>> listOfcustomizedProducts = new List<CheckedListItem<CustomizedProduct>>();
            List<CheckedListItem<Employee>> listOfDeliverymans = new List<CheckedListItem<Employee>>();
            ListOfToppings = new List<Topping>(lOT);
            ListOfProducts = new List<Product>(lOP);

            foreach (var item in allDeliveryman)
            {
                listOfDeliverymans.Add(new CheckedListItem<Employee>(item));
            }

            foreach (var item in customizedProducts)
            {
                var toAdd = new CheckedListItem<CustomizedProduct>(item);
                if (order != null && Order.OrderedProducts.Any(o => o.CustomizedProductId == toAdd.Item.CustomizedProductId))
                    toAdd.IsChecked = true;
                listOfcustomizedProducts.Add(toAdd);
            }

            ModifyOrders = new ModifyOrder
            {
                Order = Order,
                CustomizedProducts = new ObservableCollection<CustomizedProduct>(customizedProducts),
                Deliveryman = new ObservableCollection<Employee>(allDeliveryman),
                SelectedDeliveryman = order == null ? null : order.Deliveryman,
                Customers = new ObservableCollection<Customer>(listOfCustomers),
                SelectedCustomer = order == null ? null : order.Customer,
                ChangedCustomizedProducts = new List<CustomizedProduct>()
            };
            DataContext = ModifyOrders;
        }
        public List<Topping> ListOfToppings;
        public List<Product> ListOfProducts;
        public Order Order { get; set; }

        public ModifyOrder ModifyOrders;

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void EditOrderedProduct(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CustomizedProduct copy = new CustomizedProduct(button.DataContext as CustomizedProduct);
            var dialog = new AddModifyCustomizedProduct(copy, ListOfToppings, ListOfProducts);
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                copy.BaseProduct = dialog.ModifyCustomizeProducts.SelectedBP;
                Order.OrderedProducts.FirstOrDefault(op => op.CustomizedProductId == copy.CustomizedProductId).BaseProduct = copy.BaseProduct;
                Order.OrderedProducts.FirstOrDefault(op => op.CustomizedProductId == copy.CustomizedProductId).CustomerWish = copy.CustomerWish;
                Order.OrderedProducts.FirstOrDefault(op => op.CustomizedProductId == copy.CustomizedProductId).Toppings.RemoveAll(p => true);
                foreach (var item in dialog.ModifyCustomizeProducts.Toppings)
                {
                    var c = Order.OrderedProducts.FirstOrDefault(o => o.CustomizedProductId == copy.CustomizedProductId);
                    if (item.IsChecked)
                        c.Toppings.Add(item.Item);
                }
                DataContext = null;
                DataContext = ModifyOrders;
            }
        }

        private void RemoveOrderedProduct(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CustomizedProduct customizeP = button.DataContext as CustomizedProduct;
            Order.OrderedProducts.RemoveAll(op => op.CustomizedProductId == customizeP.CustomizedProductId);
            DataContext = null;
            DataContext = ModifyOrders;
            //_viewModel.customizedProductRepository.RemoveCustomizedProduct(topping);
        }

        private void AddNewCustomizeProduct(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dialog = new AddModifyCustomizedProduct(null, ListOfToppings, ListOfProducts);
            var copy = dialog.ModifyCustomizeProducts.CustomizedProduct; 
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                var customizedPToAdd = dialog.ModifyCustomizeProducts.CustomizedProduct;
                customizedPToAdd.Toppings = new List<Topping>();
                customizedPToAdd.BaseProduct = dialog.ModifyCustomizeProducts.SelectedBP;
                foreach (var item in dialog.ModifyCustomizeProducts.Toppings)
                {
                    if (item.IsChecked)
                        customizedPToAdd.Toppings.Add(item.Item);
                }
                if (Order.OrderedProducts == null)
                    Order.OrderedProducts = new List<CustomizedProduct>();
                Order.OrderedProducts.Add(customizedPToAdd);
                DataContext = null;
                DataContext = ModifyOrders;
            }
        }
    }

    public class ModifyOrder : BindableBase
    {
        public Order Order { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                SetProperty(ref _selectedCustomer, value);
            }
        }

        public ObservableCollection<CustomizedProduct> CustomizedProducts { get; set; }

        public List<CustomizedProduct> ChangedCustomizedProducts { get; set; }

        private CustomizedProduct _selectedCustomizedProduct;

        public CustomizedProduct SelectedCustomizedProduct
        {
            get { return _selectedCustomizedProduct; }
            set
            {
                SetProperty(ref _selectedCustomizedProduct, value);
            }
        }

        public ObservableCollection<Employee> Deliveryman { get; set; }

        private Employee _selectedDeliveryman;

        public Employee SelectedDeliveryman
        {
            get { return _selectedDeliveryman; }
            set
            {
                SetProperty(ref _selectedDeliveryman, value);
            }
        }
    }
}
