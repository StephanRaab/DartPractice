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
        public ICommand A1Command { get; }

        public MainViewModel()
        {
            Title = "DartsPractice";

            RVBCommand = new Command(RvbGame);
            PostItCommand = new Command(PostItGame);
            PowerCommand = new Command(PowerGame);
            A1Command = new Command(A1Game);
        }

        private async void RvbGame()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RvbGamePage());
        }

        private async void A1Game()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new A1Page());
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
