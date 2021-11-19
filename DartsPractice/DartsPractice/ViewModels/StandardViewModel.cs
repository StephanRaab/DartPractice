using System;
using System.Diagnostics;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace DartsPractice.ViewModels
{
    public class StandardViewModel : BaseViewModel
    {
        public ICommand ScoreCommand { get; }
        public ICommand ClrCommand { get; }
        public Command<string> ScoreInputCommand { get; }
        public Command<string> QuickScoreInputCommand { get; }

        public ICommand ReturnHomeCommand { get; }
        public ICommand QuitGameCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand StartGameCommand { get; }
        private double _hitAverageTotal = 0.00;

        private int _highThrow;
        public int HighThrow
        {
            get => _highThrow;
            set => SetProperty(ref _highThrow, value);
        }

        private string _totalHit = string.Empty;
        public string TotalHit
        {
            get => _totalHit;
            set => SetProperty(ref _totalHit, value);
        }

        private int _totalLeft;
        public int TotalLeft
        {
            get => _totalLeft;
            set => SetProperty(ref _totalLeft, value);
        }

        private int _inputTotal = 501;
        public int InputTotal
        {
            get => _inputTotal;
            set
            {
                _inputTotal = value;
                TotalLeft = value;
                OnPropertyChanged(nameof(InputTotal));
                OnPropertyChanged(nameof(TotalLeft));                
            }
        }

        private bool _initialOptions = true;
        public bool InitialOptions
        {
            get => _initialOptions;
            set => SetProperty(ref _initialOptions, value);
        }        

        private string _scoreBtn = "NO SCORE";
        public string ScoreBtn
        {
            get => _scoreBtn;
            set => SetProperty(ref _scoreBtn, value);
        }

        private void UpdateScoringBtn()
        {
            if (string.IsNullOrEmpty(_totalHit))
            {
                ScoreBtn = "NO SCORE";
            } else
            {
                ScoreBtn = "OK";
            }
        }

        private string _runningAverage = "0.00";
        public string RunningAverage
        {
            get => _runningAverage;
            set => SetProperty(ref _runningAverage, value);
        }

        private int _roundCount = 0;
        public int RoundCount
        {
            get => _roundCount;
            set => SetProperty(ref _roundCount, value);
        }

        private int _dartsThrown = 0;
        public int DartsThrown
        {
            get => _dartsThrown;
            set => SetProperty(ref _dartsThrown, value);
        }

        private void hitInput(string hitInput)
        {            
            if (TotalHit.ToCharArray().Length < 3)
            {
                TotalHit += hitInput;
                UpdateScoringBtn();
            }            
        }

        private void quickHitInput(string quickHitInput)
        {
            TotalHit = quickHitInput;
            updateScore();
        }

        private int ToInt(string stringNeedingInt)
        {
            try
            {
                if (string.IsNullOrEmpty(stringNeedingInt))
                    stringNeedingInt = "0";
                return Convert.ToInt32(stringNeedingInt);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error converting to int: {ex}");
            }

            return 0;
        }

        private string IntToString(int intNeedingString)
        {
            return Convert.ToString(intNeedingString);
        }

        private async void updateScore()
        {            
            var totalhit = ToInt(_totalHit);            
            
            if (totalhit > 180)
            {
                ClearScore();
            } else
            {
                // if CurrentTotal -= totalhit <0 ..bust
                // else if
                if ((TotalLeft - totalhit) < 0)
                {
                    // you've busted..increase roundcount, calculate average, dartsThrown
                    UpdateUI();
                } else if ((TotalLeft - totalhit) == 0)
                {
                    _hitAverageTotal += totalhit;
                    updateHighScore(totalhit);
                    // get finishing dartcount from popup
                    string checkoutCount = await Application.Current.MainPage.DisplayActionSheet("How many darts to checkout?", null, null, "1", "2", "3");
                    TotalLeft -= totalhit;
                    UpdateUI(ToInt(checkoutCount));

                    // show stats and endgame options
                    return;
                } else
                {
                    // keep going, you've got this
                    _hitAverageTotal += totalhit;
                    updateHighScore(totalhit);
                    TotalLeft -= totalhit;
                    UpdateUI();
                }                                                                            
            }
        }

        private void updateHighScore(int totalhit)
        {
            if (totalhit > HighThrow)
                HighThrow = totalhit;
        }

        private void UpdateUI(int endingDarts = 3)
        {
            IncreaseRndCount(endingDarts);
            ClearScore();
            UpdateScoringBtn();
            CalculateAverage();
        }

        private void CalculateAverage()
        {
            var test = ((_hitAverageTotal / DartsThrown) * 3.00);
            RunningAverage = string.Format(string.Format("{0:0.#0}", test));
        }

        private void IncreaseRndCount(int dartsThrown = 3)
        {
            RoundCount++;
            DartsThrown += dartsThrown;
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
            InitialOptions = true;
        }

        public StandardViewModel()
        {
            Title = "x01";
            ScoreCommand = new Command(updateScore);
            ClrCommand = new Command(ClearScore);
            ScoreInputCommand = new Command<string>(hitInput);
            QuickScoreInputCommand = new Command<string>(quickHitInput);
            ReturnHomeCommand = new Command(returnHomeCommand);
            QuitGameCommand = new Command(quitGameCommand);
            NewGameCommand = new Command(newGameCommand);
            StartGameCommand = new Command(StartThrowing);
        }

        private void StartThrowing()
        {
            InitialOptions = false;
        }

        private void ClearScore()
        {
            TotalHit = "";
            UpdateScoringBtn();
        }
    }
}

