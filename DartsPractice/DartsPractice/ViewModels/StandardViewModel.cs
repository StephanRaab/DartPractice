using System;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace DartsPractice.ViewModels
{
    public class StandardViewModel : BaseViewModel
    {
        public Command<string> ScoreCommand { get; }

        public ICommand ReturnHomeCommand { get; }
        public ICommand QuitGameCommand { get; }
        public ICommand NewGameCommand { get; }

        private string _scoreBtn = "NO SCORE";
        public string ScoreBtn
        {
            get => _scoreBtn;
            set => SetProperty(ref _scoreBtn, value);
        }

        private int _standardTotal = 5001;
        public int StandardTotal
        {
            get => _standardTotal;
            set => SetProperty(ref _standardTotal, value);
        }

        private int _runningAverage = 0;
        public int RunningAverage
        {
            get => _runningAverage;
            set => SetProperty(ref _runningAverage, value);
        }

        private int _dartsThrown = 0;
        public int DartsThrown
        {
            get => _dartsThrown;
            set => SetProperty(ref _dartsThrown, value);
        }

        private void updateScore(string score)
        {
            StandardTotal
        }

            private async void returnHomeCommand()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void quitGameCommand()
        {
            returnHomeCommand();
        }

        private void newGameCommand()
        {
            ResetGame();
        }

        private void ResetGame()
        {

        }

        public StandardViewModel()
        {
            Title = "x01";
            ScoreCommand = new Command<string>(updateScore);
            ReturnHomeCommand = new Command(returnHomeCommand);
            QuitGameCommand = new Command(quitGameCommand);
            NewGameCommand = new Command(newGameCommand);
        }
    }
}

