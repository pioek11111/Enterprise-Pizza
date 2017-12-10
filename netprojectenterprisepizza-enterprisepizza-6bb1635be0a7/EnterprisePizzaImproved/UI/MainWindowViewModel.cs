using System.Collections.ObjectModel;
using EnterprisePizzaImproved.Entities;
using EnterprisePizzaImproved.Logic.Repositories;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using EnterprisePizzaImproved.Abstract;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using EnterprisePizzaImproved.Logic.Filters;

namespace EnterprisePizzaImproved
{
    public class MainWindowViewModel : BindableBase
    {
        private EntityDataModel context;

        private Customer selectedCustomer;
        private Product selectedProduct;
        public ICustomerRepository customerRepository;
        public IAllergenRepository allergenRepository;
        public IEmployeeRepository EmployeeRepository;
        public IProductRepository ProductRepository;
        private IProductCategoryRepository productCategoryRepository;
        public IToppingRepository toppingRepository;
        public IToppingCategoryRepository toppingCategoryRepository;
        public IOrderRepository OrdersRepository;
        public ICustomizedProductRepository customizedProductRepository;

        private List<Customer> ListOfCustomerChanges;
        public PagingInfo productsPageInfo;
        ObservableCollection<Product> _products;

        public MainWindowViewModel()
        {
            context = new EntityDataModel();
            customerRepository = new EFCustomerRepository(context);
            allergenRepository = new EFAllergenRepository(context);
            EmployeeRepository = new EFEmployeeRepository(context);
            toppingRepository = new EFToppingRepository(context);
            OrdersRepository = new EFOrderRepository(context);
            customizedProductRepository = new EFCustomizedProductRepository(context);
            Allergens = new ObservableCollection<Allergen>(allergenRepository.AllergenRepository);
            Customers = new ObservableCollection<Customer>(customerRepository.CustomerRepository);
            Employees = new ObservableCollection<Employee>(EmployeeRepository.EmployeeRepository);
            Toppings = new ObservableCollection<Topping>(toppingRepository.ToppingRepository);
            Orders = new ObservableCollection<Order>(OrdersRepository.OrderRepository);

            ChangeCustomerCommand = new DelegateCommand(() => saveChangesOnCustomers());
            ListOfCustomerChanges = new List<Customer>();
            NextPage = new DelegateCommand(() => nextPage());
            PrevPage = new DelegateCommand(() => prevPage());
            ResetCustomersSearch = new DelegateCommand(() => resetCustomersSearch());
            CustomersSearch = new DelegateCommand<string>((name) => FilterCustomersByName(name));
            ResetOrdersSearch = new DelegateCommand(() => resetOrdersSearch());
            OrdersSearch = new DelegateCommand<string>((name) => FilterOrdersByCustomerName(name));

            ProductRepository = new EFProductRepository(context);

            productsPageInfo = new PagingInfo
            {
                TotalItems = ProductRepository.ProductRepository.Count(),
                CurrentPage = 1,
                ItemsPerPage = 5
            };
            Products = new ObservableCollection<Product>(ProductRepository.ProductRepository.OrderBy(p => p.ProductId)
                                                        .Skip((productsPageInfo.CurrentPage - 1) * productsPageInfo.ItemsPerPage)
                                                        .Take(productsPageInfo.ItemsPerPage));

            productCategoryRepository = new EFProductCategoryRepository(context);
            ProductCategory = new ObservableCollection<ProductCategory>(productCategoryRepository.ProductCategoryRepository);
            toppingCategoryRepository = new EFToppingCategoryRepository(context);
            ToppingCategory = new ObservableCollection<ToppingCategory>(toppingCategoryRepository.ToppingCategoryRepository);
            CustomizedProducts = new ObservableCollection<CustomizedProduct>(customizedProductRepository.CustomizedProductRepository);
        }

        public ObservableCollection<Allergen> Allergens { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }

        // Customer

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }



        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                if (value != selectedCustomer)
                {
                    SetProperty(ref selectedCustomer, value);
                    ListOfCustomerChanges.Add(value);
                }
            }
        }

        public ICommand ChangeCustomerCommand { get; set; }
        public ICommand EditCustomer { get; set; }

        public void saveChangesOnCustomers()
        {
            foreach (var cus in ListOfCustomerChanges)
            {
                if (cus.CustomerId == 0)
                {
                    customerRepository.AddNewCustomer(cus);
                }
                else
                {
                    Customer oldCustomer = customerRepository.CustomerRepository.Where(
                    c => c.CustomerId == cus.CustomerId).First();
                    var r = customerRepository.ChangeCustomer(oldCustomer, cus);
                }
            }
        }

        // Product
        #region Product
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                SetProperty(ref _products, value);
            }
        }

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                if (value != selectedProduct)
                {
                    SetProperty(ref selectedProduct, value);
                }
            }
        }

        public ICommand NextPage { get; set; }
        public ICommand PrevPage { get; set; }

        public void nextPage()
        {
            if (productsPageInfo.CurrentPage == productsPageInfo.TotalPages)
                return;
            productsPageInfo.CurrentPage++;
            var newProducts = new ObservableCollection<Product>(ProductRepository.ProductRepository.OrderBy(p => p.ProductId)
                                                        .Skip((productsPageInfo.CurrentPage - 1) * productsPageInfo.ItemsPerPage)
                                                        .Take(productsPageInfo.ItemsPerPage));
            Products = newProducts;
        }

        public void prevPage()
        {
            if (productsPageInfo.CurrentPage == 0)
                return;
            productsPageInfo.CurrentPage--;
            var newProducts = new ObservableCollection<Product>(ProductRepository.ProductRepository.OrderBy(p => p.ProductId)
                                                        .Skip((productsPageInfo.CurrentPage - 1) * productsPageInfo.ItemsPerPage)
                                                        .Take(productsPageInfo.ItemsPerPage));
            Products = newProducts;
        }
        #endregion
        // Product Category
        public ObservableCollection<ProductCategory> ProductCategory { get; set; }

        #region Topping
        public ObservableCollection<Topping> Toppings { get; set; }
        #endregion

        public ObservableCollection<ToppingCategory> ToppingCategory { get; set; }

        public ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }

        #region CustomizedProduct
        public ObservableCollection<CustomizedProduct> CustomizedProducts { get; set; }

        public CustomizedProduct _selectedCustomizedProduct;

        public CustomizedProduct SelectedCustomizedProduct
        {
            get { return _selectedCustomizedProduct; }
            set
            {
                if (value != _selectedCustomizedProduct)
                {
                    SetProperty(ref _selectedCustomizedProduct, value);
                }
            }
        }
        #endregion

        public ICommand ResetCustomersSearch { get; set; }
        public ICommand CustomersSearch { get; set; }
        public ICommand ResetOrdersSearch { get; set; }
        public ICommand OrdersSearch { get; set; }

        public void resetCustomersSearch()
        {
            Customers = new ObservableCollection<Customer>(customerRepository.CustomerRepository);
        }

        public void FilterCustomersByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                resetCustomersSearch();
                return;
            }

            var filter = new ClientsFilter(context);
            Customers = new ObservableCollection<Customer>(filter.FindCustomersByNameSubstring(name));
        }

        public void resetOrdersSearch()
        {
            Orders = new ObservableCollection<Order>(OrdersRepository.OrderRepository);
        }

        public void FilterOrdersByCustomerName(string customerName)
        {
            if (string.IsNullOrEmpty(customerName))
            {
                resetOrdersSearch();
                return;
            }

            var filter = new OrdersFilter(context);
            Orders = new ObservableCollection<Order>(filter.FindOrdersByCustomerNameSubstring(customerName));
        }
    }

    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }

    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return _canExecute == null ? true : _canExecute(parameters);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameters)
        {
            _execute(parameters);
        }

        #endregion // ICommand Members
    }
}