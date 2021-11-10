using System;
using System.Collections.Generic;
using DartsPractice.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace DartsPractice.Popups
{
    public partial class A1PopupPage : PopupPage
    {
        public A1PopupPage()
        {
            InitializeComponent();
            BindingContext = new A1ViewModel();
        }
    }
}
