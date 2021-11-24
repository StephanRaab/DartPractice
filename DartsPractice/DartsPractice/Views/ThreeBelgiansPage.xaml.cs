using System;
using System.Collections.Generic;
using DartsPractice.ViewModels;
using Xamarin.Forms;

namespace DartsPractice.Views
{
    public partial class ThreeBelgiansPage : ContentPage
    {
        public ThreeBelgiansPage()
        {
            InitializeComponent();
            BindingContext = new ThreeBelgiansViewModel();
        }
    }
}
