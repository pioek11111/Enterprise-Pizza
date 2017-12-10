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
    /// Interaction logic for AddModifyCustomizedProduct.xaml
    /// </summary>
    public partial class AddModifyCustomizedProduct : Window
    {
        public AddModifyCustomizedProduct(CustomizedProduct cP = null, List<Topping> listOfT = null, List<Product> listOfProducts = null)
        {
            InitializeComponent();
            CustomizedProduct cProduct = cP ?? new CustomizedProduct();
            List<CheckedListItem<Topping>> listOfToppings = new List<CheckedListItem<Topping>>();

            foreach (var item in listOfT)
            {
                var toAdd = new CheckedListItem<Topping>(item);
                if (cP != null && cP.Toppings.Any(c => c.ToppingId == toAdd.Item.ToppingId))
                    toAdd.IsChecked = true;
                listOfToppings.Add(toAdd);
            }

            ModifyCustomizeProducts = new ModifyCustomizeProduct
            {
                CustomizedProduct = cProduct,
                Toppings = new ObservableCollection<CheckedListItem<Topping>>(listOfToppings),
                SelectedBP = cProduct == null ? null : cProduct.BaseProduct,
                BaseProduct = new ObservableCollection<Product>(listOfProducts)
            };
            DataContext = ModifyCustomizeProducts;
        }
        public ModifyCustomizeProduct ModifyCustomizeProducts;

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

    public class ModifyCustomizeProduct : BindableBase
    {
        public CustomizedProduct CustomizedProduct { get; set; }

        public ObservableCollection<CheckedListItem<Topping>> Toppings { get; set; }

        private CheckedListItem<Topping> _selectedTopping;

        public CheckedListItem<Topping> SelectedTopping
        {
            get { return _selectedTopping; }
            set
            {
                SetProperty(ref _selectedTopping, value);
            }
        }

        public ObservableCollection<Product> BaseProduct { get; set; }

        private Product _selectedBP;

        public Product SelectedBP
        {
            get { return _selectedBP; }
            set
            {
                SetProperty(ref _selectedBP, value);
            }
        }
    }
}
