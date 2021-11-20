using System;
using System.Diagnostics;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace DartsPractice.ViewModels
{
    public class SwitchingViewModel : BaseViewModel
    {        
        public ICommand ReturnHomeCommand { get; }
        public ICommand QuitGameCommand { get; }
        public ICommand NewGameCommand { get; }

        public Command<string> ScoreInputCommand { get; }
        public ICommand ScoreCommand { get; }
        public ICommand ClrCommand { get; }

        private bool _finished = false;

        private int _lastScore = 0;
        private int _roundCount = 0;


        private string _headerText = "Switching Target";
        public string HeaderText
        {
            get => _headerText;
            set => SetProperty(ref _headerText, value);
        }

        private bool _showEndOptions = false;
        public bool ShowEndOptions
        {
            get => _showEndOptions;
            set => SetProperty(ref _showEndOptions, value);
        }

        private string _nineteenTotal;
        public string NineteenTotal
        {
            get => _nineteenTotal;
            set => SetProperty(ref _nineteenTotal, value);
        }
        private string _eightteenTotal;
        public string EighteenTotal
        {
            get => _eightteenTotal;
            set => SetProperty(ref _eightteenTotal, value);
        }
        private string _seventeenTotal;
        public string SeventeenTotal
        {
            get => _seventeenTotal;
            set => SetProperty(ref _seventeenTotal, value);
        }
        private string _sixteenTotal;
        public string SixteenTotal
        {
            get => _sixteenTotal;
            set => SetProperty(ref _sixteenTotal, value);
        }
        private string _fifteenTotal;
        public string FifteenTotal
        {
            get => _fifteenTotal;
            set => SetProperty(ref _fifteenTotal, value);
        }

        private int _runningTotal = 0;
        public int RunningTotal
        {
            get => _runningTotal;
            set => SetProperty(ref _runningTotal, value);
        }

        private string _currentTarget = "19";
        public string CurrentTarget
        {
            get => _currentTarget;
            set => SetProperty(ref _currentTarget, value);
        }

        private string _totalHit = string.Empty;
        public string TotalHit
        {
            get => _totalHit;
            set => SetProperty(ref _totalHit, value);
        }

        private void hitInput(string hitInput)
        {
            if (TotalHit.ToCharArray().Length < 2)
            {
                TotalHit += hitInput;
            }
        }

        private string calculateCurrentTarget()
        {
            switch (_roundCount)
            {
                case 0:
                    CurrentTarget = "19";
                    return NineteenTotal;                    
                case 1:
                    CurrentTarget = "18";
                    return EighteenTotal;                    
                case 2:
                    CurrentTarget = "17";
                    return SeventeenTotal;                    
                case 3:
                    CurrentTarget = "16";
                    return SixteenTotal;                    
                case 4:
                    CurrentTarget = "15";
                    return FifteenTotal;
            }

            return null;
        }        

        private async void returnHomeCommand()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void quitGameCommand()
        {
            returnHomeCommand();
        }

        private void ClearScore()
        {
            TotalHit = "";
        }

        private void updateScore()
        {
            if (string.IsNullOrEmpty(TotalHit))
                TotalHit = "0";

            switch (_roundCount)
            {
                case 0:
                    CurrentTarget = "18";
                    NineteenTotal = TotalHit;
                    break;
                case 1:
                    CurrentTarget = "17";
                    EighteenTotal = TotalHit;
                    break;
                case 2:
                    CurrentTarget = "16";
                    SeventeenTotal = TotalHit;
                    break;
                case 3:
                    CurrentTarget = "15";
                    SixteenTotal = TotalHit;
                    break;
                case 4:
                    FifteenTotal = TotalHit;
                    ShowEndOptions = true;
                    _finished = true;
                    break;
            }
            
            RunningTotal += Convert.ToInt32(TotalHit);

            if (_finished)
            {
                HeaderText = "Total";
                CurrentTarget = Convert.ToString(RunningTotal);
            }

            _roundCount++;
            ClearScore();
        }

        private void resetGame()
        {
            _roundCount = 0;
            _finished = false;
            CurrentTarget = "19";
            ShowEndOptions = false;
            HeaderText = "Switching Target";

            NineteenTotal = string.Empty;
            EighteenTotal = string.Empty;
            SeventeenTotal = string.Empty;
            SixteenTotal = string.Empty;
            FifteenTotal = string.Empty;
            RunningTotal = 0;
        }

        private async void exitGame()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public SwitchingViewModel()
        {
            Title = "Switching Routine";
            ScoreInputCommand = new Command<string>(hitInput);
            ScoreCommand = new Command(updateScore);
            ClrCommand = new Command(ClearScore);
            NewGameCommand = new Command(resetGame);
            QuitGameCommand = new Command(exitGame);
        }
    }
}
