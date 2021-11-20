using System;
using System.Collections.Generic;
using DartsPractice.ViewModels;
using Xamarin.Forms;

namespace DartsPractice.Views
{
    public partial class SwitchingPage : ContentPage
    {
        public SwitchingPage()
        {
            InitializeComponent();
            BindingContext = new SwitchingViewModel();
        }
    }
}
