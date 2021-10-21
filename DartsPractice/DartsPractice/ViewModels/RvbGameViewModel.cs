using System;
using System.Windows.Input;
using DartsPractice.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace DartsPractice.ViewModels
{
    public class RvbGameViewModel : BaseViewModel
    {
        public Command<string> ScoreCommand { get; }

        public ICommand ReturnHomeCommand { get; }
        public ICommand QuitGameCommand { get; }
        public ICommand NewGameCommand { get; }

        public RVBScore RVBScore = new RVBScore();

        private int _lastScore = 0;
        private int _runningTotal = 0;
        private int _roundCount = 0;
        private string _currentTarget = "20";
        private bool _showEndOptions = false;

        public bool ShowEndOptions
        {
            get => _showEndOptions;
            set => SetProperty(ref _showEndOptions, value);
        }

        public int RunningTotal
        {
            get => _runningTotal;
            set => SetProperty(ref _runningTotal, value);
        }   

        public string CurrentTarget
        {
            get => _currentTarget;
            set => SetProperty(ref _currentTarget, value);
        }

        private void calculateCurrentTarget()
        {
            switch (_roundCount)
            {
                case 0:
                    CurrentTarget = "20";
                    break;
                case 1:
                    CurrentTarget = "19";
                    break;
                case 2:
                    CurrentTarget = "18";
                    break;
                case 3:
                    CurrentTarget = "17";
                    break;
                case 4:
                    CurrentTarget = "16";
                    break;
                case 5:
                    CurrentTarget = "15";
                    break;
                case 6:
                    CurrentTarget = "Bull";
                    break;
            }            
        }

        public void Score(string score)
        {
            switch (_roundCount)
            {
                case 0:
                    RVBScore.Twenties = Convert.ToInt32(score);
                    break;
                case 1:
                    RVBScore.Nineteens = Convert.ToInt32(score);
                    break;
                case 2:
                    RVBScore.Eighteens = Convert.ToInt32(score);
                    break;
                case 3:
                    RVBScore.Seventeens = Convert.ToInt32(score);
                    break;
                case 4:
                    RVBScore.Sixteens = Convert.ToInt32(score);
                    break;
                case 5:
                    RVBScore.Fifteens = Convert.ToInt32(score);
                    break;
                case 6:
                    RVBScore.Bulls = Convert.ToInt32(score);
                    break;
            }
        }

        private void ResetGame()
        {
            _lastScore = 0;
            RunningTotal = 0;
            _roundCount = 0;
            calculateCurrentTarget();
            ShowEndOptions = false;
            RVBScore = new RVBScore();
        }

        private void updateScore(string score)
        {
            if (_roundCount <= 6)
            {
                // set lastScore to score
                _lastScore = Convert.ToInt32(score);

                Score(score);

                // update runningTotal by score
                RunningTotal += Convert.ToInt32(score);

                // update roundcount
                _roundCount++;

                // update currentTarget
                calculateCurrentTarget();

                if (_roundCount == 7)
                {                    
                    // show return button
                    ShowEndOptions = true;

                    RVBScore.Time = DateTime.UtcNow;
                    RVBScore.ScoreTotal = RunningTotal;

                    // save to db
                    // show statistics
                }
            }            
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

        public RvbGameViewModel()
        {
            Title = "RVB Game";
            ScoreCommand = new Command<string>(updateScore);
            ReturnHomeCommand = new Command(returnHomeCommand);
            QuitGameCommand = new Command(quitGameCommand);
            NewGameCommand = new Command(newGameCommand);
        }        
    }
}
