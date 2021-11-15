using System;
using System.Collections.Generic;
using DartsPractice.ViewModels;
using Xamarin.Forms;

namespace DartsPractice.Views
{
    public partial class StandardPage : ContentPage
    {
        public StandardPage()
        {
            InitializeComponent();
            BindingContext = new StandardViewModel();
        }
    }
}

