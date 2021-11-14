using System;
using System.Collections.Generic;
using DartsPractice.ViewModels;
using Xamarin.Forms;

namespace DartsPractice.Views
{
    public partial class A1Page : ContentPage
    {
        public A1Page()
        {
            InitializeComponent();
            BindingContext = new A1ViewModel();
        }
    }
}
