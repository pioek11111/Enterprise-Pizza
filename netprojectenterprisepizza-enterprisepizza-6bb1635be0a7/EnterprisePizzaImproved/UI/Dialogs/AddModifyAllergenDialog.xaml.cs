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
    /// Interaction logic for AddModifyAllergenDialog.xaml
    /// </summary>
    public partial class AddModifyAllergenDialog : Window
    {
        public Allergen Allergen { get; set; }

        public AddModifyAllergenDialog(Allergen allergen = null)
        {
            InitializeComponent();
            Allergen = allergen ?? new Allergen();
            DataContext = Allergen;
        }

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
