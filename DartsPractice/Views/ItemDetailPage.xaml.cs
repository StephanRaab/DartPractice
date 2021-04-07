using System.ComponentModel;
using Xamarin.Forms;
using DartsPractice.ViewModels;

namespace DartsPractice.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}