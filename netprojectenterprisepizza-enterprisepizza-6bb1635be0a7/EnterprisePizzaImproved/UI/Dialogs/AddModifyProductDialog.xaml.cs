using EnterprisePizzaImproved.Entities;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EnterprisePizzaImproved
{
    /// <summary>
    /// Interaction logic for AddModifyProductDialog.xaml
    /// </summary>
    public partial class AddModifyProductDialog : Window
    {
        public AddModifyProductDialog(Product product = null, List<Allergen> baseAllergens = null, List<ProductCategory> pc = null)
        {
            InitializeComponent();
            Product = product ?? new Product();
            List<CheckedListItem<Allergen>> listOfAllergens = new List<CheckedListItem<Allergen>>();

            foreach (var item in baseAllergens)
            {
                var toAdd = new CheckedListItem<Allergen>(item);
                if (product != null && Product.Allergens.Any(p => p.AllergenId == toAdd.Item.AllergenId))
                    toAdd.IsChecked = true;
                listOfAllergens.Add(toAdd);
            }
            
            ModifyProducts = new ModifyProducts { Product = Product,
                                                  BAllergens = new ObservableCollection<CheckedListItem<Allergen>>(listOfAllergens),
                                                  PCategory = new ObservableCollection<ProductCategory>(pc),
                                                  SelectedPC = product == null ? null : product.ProductCategory };
            DataContext = ModifyProducts;
        }

        public Product Product { get; set; }

        public ModifyProducts ModifyProducts;

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

    public class ModifyProducts : BindableBase
    {
        public Product Product { get; set; }

        public ObservableCollection<CheckedListItem<Allergen>> BAllergens { get; set; }

        private CheckedListItem<Allergen> _selectedAllergen;

        public CheckedListItem<Allergen> SelectedAllergen
        {
            get { return _selectedAllergen; }
            set
            {
                SetProperty(ref _selectedAllergen, value);
            }
        }

        public ObservableCollection<ProductCategory> PCategory { get; set; }

        private ProductCategory _selectedPC;

        public ProductCategory SelectedPC
        {
            get { return _selectedPC; }
            set
            {
                SetProperty(ref _selectedPC, value);
            }
        }
    }

    public class CheckedListItem<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isChecked;
        private T item;

        public CheckedListItem()
        { }

        public CheckedListItem(T item, bool isChecked = false)
        {
            this.item = item;
            this.isChecked = isChecked;
        }

        public T Item
        {
            get { return item; }
            set
            {
                item = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Item"));
            }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
            }
        }
    }
}
