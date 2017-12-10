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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnterprisePizzaImproved
{
    /// <summary>
    /// Interaction logic for AddModifyToppingDialog.xaml
    /// </summary>
    public partial class AddModifyToppingDialog : Window
    {
        public AddModifyToppingDialog(Topping topping = null, List<Allergen> allergens = null, List<ToppingCategory> tc = null)
        {
            InitializeComponent();
            Topping = topping ?? new Topping();
            List<CheckedListItem<Allergen>> listOfAllergens = new List<CheckedListItem<Allergen>>();

            foreach (var item in allergens)
            {
                var toAdd = new CheckedListItem<Allergen>(item);
                if (topping != null && Topping.Allergens.Any(p => p.AllergenId == toAdd.Item.AllergenId))
                    toAdd.IsChecked = true;
                listOfAllergens.Add(toAdd);
            }

            ModifyTopping = new ModifyTopping
            {
                Topping = Topping,
                BAllergens = new ObservableCollection<CheckedListItem<Allergen>>(listOfAllergens),
                TCategory = new ObservableCollection<ToppingCategory>(tc),
                SelectedTC = topping == null ? null : topping.ToppingCategory
            };

            DataContext = ModifyTopping;            
        }
        public ModifyTopping ModifyTopping;
        public Topping Topping { get; set; }
        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }

    public class ModifyTopping : BindableBase
    {
        public Topping Topping { get; set; }

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

        public ObservableCollection<ToppingCategory> TCategory { get; set; }

        private ToppingCategory _selectedTC;

        public ToppingCategory SelectedTC
        {
            get { return _selectedTC; }
            set
            {
                SetProperty(ref _selectedTC, value);
            }
        }
    }

    /*public class CheckedListItem<T> : INotifyPropertyChanged
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
        }*/
    }

