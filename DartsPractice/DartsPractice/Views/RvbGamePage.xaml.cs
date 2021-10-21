using System;
using System.Collections.Generic;
using DartsPractice.ViewModels;
using Xamarin.Forms;

namespace DartsPractice.Views
{
    public partial class RvbGamePage : ContentPage
    {
        public RvbGamePage()
        {
            InitializeComponent();
            BindingContext = new RvbGameViewModel();
        }
    }
}
