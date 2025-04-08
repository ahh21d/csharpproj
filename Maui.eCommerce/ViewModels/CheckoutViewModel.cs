using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;

namespace Maui.eCommerce.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> CartItems { get; set; }

        public CheckoutViewModel()
        {
            CartItems = new ObservableCollection<Item>(
                ShoppingCartService.Current.CartItems
                    .Where(i => (i.Quantity ?? 0) > 0)
            );
        }


        public double Subtotal => CartItems.Sum(i => (i.Price ?? 0) * (i.Quantity ?? 0));
        public double Tax => Subtotal * 0.3;
        public double Total => Subtotal + Tax;

        public string SubtotalDisplay => $"Subtotal: ${Subtotal:F2}";
        public string TaxDisplay => $"Tax (7%): ${Tax:F2}";
        public string TotalDisplay => $"Total: ${Total:F2}";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
