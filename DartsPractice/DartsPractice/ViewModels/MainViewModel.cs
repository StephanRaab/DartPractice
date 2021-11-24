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
        public ICommand StandardGameCommand { get; }
        public ICommand SwitchingCommand { get; }
        public ICommand ThreeBelgiansCommand { get; }

        public MainViewModel()
        {
            Title = "DartsPractice";

            RVBCommand = new Command(RvbGame);
            PostItCommand = new Command(PostItGame);
            PowerCommand = new Command(PowerGame);
            A1Command = new Command(A1Game);
            StandardGameCommand = new Command(StandardGame);
            SwitchingCommand = new Command(SwitchingGame);
            ThreeBelgiansCommand = new Command(ThreeBelgiansGame);
        }

        private async void ThreeBelgiansGame(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ThreeBelgiansPage());
        }

        private async void SwitchingGame()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SwitchingPage());
        }

        private async void StandardGame()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new StandardPage());
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
