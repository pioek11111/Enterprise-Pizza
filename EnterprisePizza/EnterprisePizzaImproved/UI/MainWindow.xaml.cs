using EnterprisePizzaImproved.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using EnterprisePizzaImproved.DatabaseFilling;
using Unity;
using EnterprisePizzaImproved.Abstract;
using EnterprisePizzaImproved.Logic.Filters;
using EnterprisePizzaImproved.Logic.Repositories;
using EnterprisePizzaImproved.UI.Dialogs;

namespace EnterprisePizzaImproved
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        private EntityDataModel context;
        public MainWindow()
        {
            context = new EntityDataModel();
            //DatabaseFiller.ClearDatabase(context);
            //DatabaseFiller.FillWithData(context);

            InitializeComponent();

            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;


        }

        #region Allergen
        private void EditAllergenButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Allergen copy = new Allergen(button.DataContext as Allergen);
            var dialog = new AddModifyAllergenDialog(copy);
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                for (int i = 0; i < _viewModel.Allergens.Count; i++)
                {
                    if (_viewModel.Allergens[i].AllergenId == copy.AllergenId)
                    {
                        _viewModel.allergenRepository.ChangeAllergen(_viewModel.Allergens[i], copy);
                        _viewModel.Allergens[i] = copy;
                    }
                }
            }
        }

        private void RemoveAllergenButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Allergen allergen = button.DataContext as Allergen;
            _viewModel.Allergens.Remove(allergen);
            _viewModel.allergenRepository.RemoveProductAllergen(allergen);
        }

        private void AddAllergenButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dialog = new AddModifyAllergenDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                var allergen = dialog.Allergen;
                _viewModel.allergenRepository.AddNewAllergen(allergen);
                _viewModel.Allergens.Add(allergen);
            }
        }
        #endregion

        #region Employee
        private void AddEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dialog = new AddModifyEmployeeDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                var employee = dialog.Employee;
                _viewModel.EmployeeRepository.AddNewEmployee(employee);
                _viewModel.Employees.Add(employee);
            }
        }

        private void EditEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Employee copy = new Employee(button.DataContext as Employee);

            var dialog = new AddModifyEmployeeDialog(copy);
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                for (int i = 0; i < _viewModel.Employees.Count; i++)
                {
                    if (_viewModel.Employees[i].EmployeeId == copy.EmployeeId)
                    {
                        _viewModel.EmployeeRepository.ChangeEmployee(_viewModel.Employees[i], copy);
                        _viewModel.Employees[i] = copy;
                    }
                }
            }
        }

        private void RemoveEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Employee employee = button.DataContext as Employee;
            _viewModel.EmployeeRepository.RemoveEmployee(employee);
            _viewModel.Employees.Remove(employee);
        }
        #endregion

        #region Customer
        private void EditCustomerButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Customer copy = new Customer(button.DataContext as Customer);
            var dialog = new AddModifyCustomerDialog(copy);
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                for (int i = 0; i < _viewModel.Customers.Count; i++)
                {
                    if (_viewModel.Customers[i].CustomerId == copy.CustomerId)
                    {
                        _viewModel.customerRepository.ChangeCustomer(_viewModel.Customers[i], copy);
                        _viewModel.Customers[i] = copy;
                    }
                }
            }
        }

        private void RemoveCustomerButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Customer customer = button.DataContext as Customer;
            _viewModel.Customers.Remove(customer);
            _viewModel.customerRepository.RemoveCustomer(customer);
        }

        private void AddCustomerButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dialog = new AddModifyCustomerDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                var customer = dialog.Customer;
                _viewModel.customerRepository.AddNewCustomer(customer);
                _viewModel.Customers.Add(customer);
            }
        }
        #endregion

        #region Product
        private void EditProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = _viewModel.SelectedProduct;
            Button button = sender as Button;
            Product copy = new Product(button.DataContext as Product);
            var dialog = new AddModifyProductDialog(copy, _viewModel.Allergens.ToList(), _viewModel.ProductCategory.ToList());
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                List<Allergen> listOfAllergens = new List<Allergen>();
                foreach (var item in dialog.ModifyProducts.BAllergens)
                {
                    if (item.IsChecked)
                        listOfAllergens.Add(new Allergen(item.Item));
                }
                copy.Allergens = listOfAllergens;
                copy.ProductCategory = dialog.ModifyProducts.SelectedPC;
                for (int i = 0; i < _viewModel.Products.Count; i++)
                {
                    if (_viewModel.Products[i].ProductId == copy.ProductId)
                    {
                        _viewModel.ProductRepository.ChangeProduct(_viewModel.Products[i], copy);
                        _viewModel.Products[i] = copy;
                    }
                }
            }
        }

        private void RemoveProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Product product = button.DataContext as Product;
            _viewModel.Products.Remove(product);
            _viewModel.ProductRepository.RemoveProduct(product);
            _viewModel.productsPageInfo.TotalItems = _viewModel.ProductRepository.ProductRepository.Count();
            if (_viewModel.productsPageInfo.CurrentPage > _viewModel.productsPageInfo.TotalPages)
            {
                _viewModel.productsPageInfo.CurrentPage--;
                _viewModel.Products = new ObservableCollection<Product>(_viewModel.ProductRepository.ProductRepository.OrderBy(p => p.ProductId)
                                                        .Skip((_viewModel.productsPageInfo.CurrentPage - 1) * _viewModel.productsPageInfo.ItemsPerPage)
                                                        .Take(_viewModel.productsPageInfo.ItemsPerPage));
            }
            DataContext = null;
            DataContext = _viewModel;
        }

        private void AddProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dialog = new AddModifyProductDialog(null, _viewModel.Allergens.ToList(), _viewModel.ProductCategory.ToList());
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                List<Allergen> listOfAllergens = new List<Allergen>();
                var product = dialog.Product;
                foreach (var item in dialog.ModifyProducts.BAllergens)
                {
                    if (item.IsChecked)
                    {
                        listOfAllergens.Add(item.Item);
                    }
                }
                product.ProductCategory = dialog.ModifyProducts.SelectedPC;
                product.Allergens = listOfAllergens;
                _viewModel.ProductRepository.AddNewProduct(product);
                _viewModel.Products.Add(product);
                _viewModel.productsPageInfo.TotalItems = _viewModel.ProductRepository.ProductRepository.Count();
                if (_viewModel.productsPageInfo.CurrentPage < _viewModel.productsPageInfo.TotalPages)
                {
                    _viewModel.productsPageInfo.CurrentPage++;
                    _viewModel.Products = new ObservableCollection<Product>(_viewModel.ProductRepository.ProductRepository.OrderBy(p => p.ProductId)
                                                            .Skip((_viewModel.productsPageInfo.CurrentPage - 1) * _viewModel.productsPageInfo.ItemsPerPage)
                                                            .Take(_viewModel.productsPageInfo.ItemsPerPage));
                }
                DataContext = null;
                DataContext = _viewModel;
            }
        }
        #endregion

        #region Topping
        private void AddToppingButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dialog = new AddModifyToppingDialog(null, _viewModel.Allergens.ToList(), _viewModel.toppingCategoryRepository.ToppingCategoryRepository.ToList());
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                List<Allergen> listOfAllergens = new List<Allergen>();
                var topping = dialog.Topping;
                foreach (var item in dialog.ModifyTopping.BAllergens)
                {
                    if (item.IsChecked)
                    {
                        listOfAllergens.Add(item.Item);
                    }
                }
                topping.ToppingCategory = dialog.ModifyTopping.SelectedTC;
                topping.Allergens = listOfAllergens;
                _viewModel.toppingRepository.AddNewTopping(topping);
                _viewModel.Toppings.Add(topping);
            }
        }

        private void EditToppingButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Topping copy = new Topping(button.DataContext as Topping);
            var dialog = new AddModifyToppingDialog(copy, _viewModel.Allergens.ToList(), _viewModel.toppingCategoryRepository.ToppingCategoryRepository.ToList());
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                List<Allergen> listOfAllergens = new List<Allergen>();
                foreach (var item in dialog.ModifyTopping.BAllergens)
                {
                    if (item.IsChecked)
                        listOfAllergens.Add(new Allergen(item.Item));
                }
                copy.Allergens = listOfAllergens;
                copy.ToppingCategory = dialog.ModifyTopping.SelectedTC;
                for (int i = 0; i < _viewModel.Toppings.Count; i++)
                {
                    if (_viewModel.Toppings[i].ToppingId == copy.ToppingId)
                    {
                        _viewModel.toppingRepository.ChangeTopping(_viewModel.Toppings[i], copy);
                        _viewModel.Toppings[i] = copy;
                    }
                }
            }
        }

        private void RemoveToppingButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Topping topping = button.DataContext as Topping;
            _viewModel.Toppings.Remove(topping);
            _viewModel.toppingRepository.RemoveTopping(topping);
        }
        #endregion

        #region Order
        private void AddOrderButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddModifyOrderDialog(null, _viewModel.customizedProductRepository.CustomizedProductRepository.ToList(),
                                                        _viewModel.Employees.ToList(), _viewModel.Customers.ToList(),
                                                        _viewModel.Toppings.ToList(), _viewModel.Products.ToList());

            Order copy = new Order();
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                copy.Deliveryman = dialog.ModifyOrders.SelectedDeliveryman;
                copy.Customer = dialog.ModifyOrders.SelectedCustomer;

                // wypełnienie pól not null niebędących w formularzu
                copy.OrderedProducts = dialog.ModifyOrders.Order.OrderedProducts;
                copy.OrderStatus = OrderStatus.Cooking;
                copy.CookingDeadline = DateTime.Parse("15/11/2017 9:00");
                copy.DeliveryDeadline = DateTime.Parse("15/11/2017 9:00");
                copy.OrderCreated = DateTime.Parse("15/11/2017 9:00");
                copy.TotalPrice = 0;

                _viewModel.OrdersRepository.AddNewOrder(copy);
                _viewModel.Orders.Add(copy);
                DataContext = null;
                DataContext = _viewModel;
            }
        }


        private void EditOrderButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Order copy = new Order(button.DataContext as Order);
            var dialog = new AddModifyOrderDialog(copy, _viewModel.customizedProductRepository.CustomizedProductRepository.ToList(),
                                                        _viewModel.Employees.ToList(), _viewModel.Customers.ToList(),
                                                        _viewModel.Toppings.ToList(), _viewModel.Products.ToList());

            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {

                copy.OrderStatus = OrderStatus.Cooking;
                copy.CookingDeadline = DateTime.Parse("15/11/2017 9:00");
                copy.DeliveryDeadline = DateTime.Parse("15/11/2017 9:00");
                copy.OrderCreated = DateTime.Parse("15/11/2017 9:00");
                copy.TotalPrice = 0;

                copy.Deliveryman = dialog.ModifyOrders.SelectedDeliveryman;
                copy.Customer = dialog.ModifyOrders.SelectedCustomer;
                copy.OrderedProducts = dialog.ModifyOrders.Order.OrderedProducts;
                for (int i = 0; i < _viewModel.Orders.Count; i++)
                {
                    if (_viewModel.Orders[i].OrderId == copy.OrderId)
                    {
                        _viewModel.OrdersRepository.ChangeOrder(_viewModel.Orders[i], copy);
                        _viewModel.Orders[i] = copy;
                        DataContext = null;
                        DataContext = _viewModel;
                    }
                }
            }
        }

        private void RemoveOrderButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Order order = button.DataContext as Order;
            _viewModel.OrdersRepository.RemoveOrder(order);
            _viewModel.Orders.Remove(order);
        }
        #endregion

        #region CustomizedProduct
        private void AddCustomizedProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dialog = new AddModifyCustomizedProduct(null, _viewModel.Toppings.ToList(), _viewModel.Products.ToList());
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                List<Topping> listOfToppings = new List<Topping>();
                var customizeP = dialog.ModifyCustomizeProducts.CustomizedProduct;
                foreach (var item in dialog.ModifyCustomizeProducts.Toppings)
                {
                    if (item.IsChecked)
                    {
                        listOfToppings.Add(item.Item);
                    }
                }
                customizeP.BaseProduct = dialog.ModifyCustomizeProducts.SelectedBP;
                customizeP.Toppings = listOfToppings;
                _viewModel.customizedProductRepository.AddNewCustomizedProduct(customizeP);
                _viewModel.CustomizedProducts.Add(customizeP);
            }
        }

        private void EditCustomizedProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CustomizedProduct copy = new CustomizedProduct(button.DataContext as CustomizedProduct);
            var dialog = new AddModifyCustomizedProduct(copy, _viewModel.Toppings.ToList(), _viewModel.Products.ToList());
            dialog.Owner = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                List<Topping> listOfToppings = new List<Topping>();
                var customizeP = dialog.ModifyCustomizeProducts.CustomizedProduct;
                foreach (var item in dialog.ModifyCustomizeProducts.Toppings)
                {
                    if (item.IsChecked)
                    {
                        listOfToppings.Add(item.Item);
                    }
                }
                copy.Toppings = listOfToppings;
                copy.BaseProduct = dialog.ModifyCustomizeProducts.SelectedBP;
                for (int i = 0; i < _viewModel.CustomizedProducts.Count; i++)
                {
                    if (_viewModel.CustomizedProducts[i].CustomizedProductId == copy.CustomizedProductId)
                    {
                        _viewModel.customizedProductRepository.ChangeCustomizedProduct(_viewModel.CustomizedProducts[i], copy);
                        _viewModel.CustomizedProducts[i] = copy;
                    }
                }
            }
        }

        private void RemoveCustomizedProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CustomizedProduct topping = button.DataContext as CustomizedProduct;
            _viewModel.CustomizedProducts.Remove(topping);
            _viewModel.customizedProductRepository.RemoveCustomizedProduct(topping);
        }
        #endregion
        
    }
}