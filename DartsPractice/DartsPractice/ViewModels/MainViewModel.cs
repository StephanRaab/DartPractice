using System;
using System.Windows.Input;
using DartsPractice.Views;
using MvvmHelpers;
using Xamarin.Forms;

namespace DartsPractice.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand RVBCommand { get; }
        public ICommand PostItCommand { get; }
        public ICommand PowerCommand { get; }

        public MainViewModel()
        {
            RVBCommand = new Command(RvbGame);
            PostItCommand = new Command(PostItGame);
            PowerCommand = new Command(PowerGame);
        }

        private async void RvbGame()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RvbGamePage());
        }

        private async void PostItGame()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PostItGamePage());
        }

        private async void PowerGame()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PowerGamePage());
        }
    }
}
