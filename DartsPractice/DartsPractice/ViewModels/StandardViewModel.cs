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

        private Color _backgroundColor = Color.Orange;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => SetProperty(ref _backgroundColor, value);
        }

        private bool _inGameStats = true;
        public bool InGameStats
        {
            get => _inGameStats;
            set => SetProperty(ref _inGameStats, value);
        }

        private bool _showEndOptions = false;
        public bool ShowEndOptions
        {
            get => _showEndOptions;
            set => SetProperty(ref _showEndOptions, value);
        }

        private int _sixtyPlusHit;
        public int SixtyPlusHit
        {
            get => _sixtyPlusHit;
            set => SetProperty(ref _sixtyPlusHit, value);
        }

        private int _eightyPlusHit;
        public int EightyPlusHit
        {
            get => _eightyPlusHit;
            set => SetProperty(ref _eightyPlusHit, value);
        }

        private int _hundredPlusHit;
        public int HundredPlusHit
        {
            get => _hundredPlusHit;
            set => SetProperty(ref _hundredPlusHit, value);
        }

        private int _hundredTwentyPlusHit;
        public int HundredTwentyPlusHit
        {
            get => _hundredTwentyPlusHit;
            set => SetProperty(ref _hundredTwentyPlusHit, value);
        }

        private int _hundredFortyPlusHit;
        public int HundredFortyPlusHit
        {
            get => _hundredFortyPlusHit;
            set => SetProperty(ref _hundredFortyPlusHit, value);
        }

        private int _oneEightyHit;
        public int OneEightyHit
        {
            get => _oneEightyHit;
            set => SetProperty(ref _oneEightyHit, value);
        }

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

        private async void updateScore()
        {            
            var totalHit = ToInt(TotalHit);            
            
            if (totalHit > 180)
            {
                ClearScore();
            } else
            {
                if ((TotalLeft - totalHit) < 0)
                {
                    // you've busted..increase roundcount, calculate average, dartsThrown
                    UpdateUI();
                } else if ((TotalLeft - totalHit) == 0)
                {
                    updateStats(totalHit);
                    _hitAverageTotal += totalHit;
                    updateHighScore(totalHit);
                    // get finishing dartcount from popup
                    string checkoutCount = await Application.Current.MainPage.DisplayActionSheet("How many darts to checkout?", null, null, "1", "2", "3");
                    TotalLeft -= totalHit;
                    UpdateUI(ToInt(checkoutCount));

                    // show stats and endgame options
                    InGameStats = false; //endgamestats true :P
                    ShowEndOptions = true;
                    return;
                } else
                {
                    // keep going, you've got this
                    updateStats(totalHit);
                    _hitAverageTotal += totalHit;
                    updateHighScore(totalHit);
                    TotalLeft -= totalHit;
                    UpdateUI();
                }                                                                            
            }
        }

        private void updateStats(int totalHit)
        {
            if ((totalHit >= 60) && (totalHit < 80))
            {
                SixtyPlusHit++;
            } else if ((totalHit >= 80) && (totalHit < 100))
            {
                EightyPlusHit++;
            } else if ((totalHit >= 100) && (totalHit < 120))
            {
                HundredPlusHit++;
            } else if ((totalHit >= 120) && (totalHit < 140))
            {
                HundredTwentyPlusHit++;
            } else if ((totalHit >= 140) && (totalHit < 180))
            {
                HundredFortyPlusHit++;
            } else if (totalHit == 180)
            {
                OneEightyHit++;
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
            InitialOptions = true;
            ShowEndOptions = false;
            InGameStats = true;
            _hitAverageTotal = 0.00;
            SixtyPlusHit = 0;
            EightyPlusHit = 0;
            HundredPlusHit = 0;
            HundredTwentyPlusHit = 0;
            HundredFortyPlusHit = 0;
            OneEightyHit = 0;
            RoundCount = 0;
            RunningAverage = "0.00";
            HighThrow = 0;
            DartsThrown = 0;
            TotalLeft = InputTotal;
            BackgroundColor = Color.Orange;
            Title = "x01";
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
            ShowEndOptions = false;
            BackgroundColor = Color.FromHex("#333");
            Title = Convert.ToString(TotalLeft);
        }

        private void ClearScore()
        {
            TotalHit = "";
            UpdateScoringBtn();
        }
    }
}

