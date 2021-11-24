using System;
using System.Collections.Generic;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace DartsPractice.ViewModels
{
    public class ThreeBelgiansViewModel : BaseViewModel
    {
        public ICommand SingleHitCommand { get; }
        public ICommand DoubleHitCommand { get; }
        public ICommand TrebleHitCommand { get; }
        public ICommand NoHitCommand { get; }

        public ICommand QuitGameCommand { get; }
        public ICommand NewGameCommand { get; }

        private int _currentTarget = 0;
        private bool firstTarget = true;
        private const int FINAL_TARGET = 12;


        private List<string> targetList = new List<string>
        {
            "S 6-3-11",
            "D 2-4-6",
            "T 12-13-14",
            "S 3-11-20",
            "D 8-10-12",
            "T 15-16-17",
            "S 11-20-6",
            "D 14-16-8",
            "T 18-19-20",
            "S 20-6-3",
            "D 20-20-B",
            "T 18-19-20",
            "F"
        };

        private void setNewTargetText()
        {
            if (firstTarget)
            {
                CurrentTargetText = targetList[_currentTarget];                
                return;
            } else if (_currentTarget == FINAL_TARGET){
                ShowEndOptions = true;
                CurrentTargetText = RunningScore.ToString();
            } else
            {
                CurrentTargetText = targetList[_currentTarget];
            }           
        }

        private int getTargetMultiplier()
        {
            if (firstTarget)
            {
                firstTarget = false;
                return 1;
            }

            var currentTarget = targetList[_currentTarget];
            switch (currentTarget[0].ToString())
            {
                case "S":
                    return 1;
                case "D":
                    return 2;
                case "T":
                    return 3;                    
                default:
                    break;
            }

            return 0;
        }

        private void scoreTargetsHit(string hitCountParam)
        {
            int hits = 0;
            int hitCount = Convert.ToInt32(hitCountParam);

            if (hitCount == 0)
            {
                _currentTarget++;
                setNewTargetText();
                return;
            }

            switch (getTargetMultiplier())
            {
                case 1:
                    if (hitCount == 3)
                    {
                        hits += hitCount + 3;
                    }
                    else
                    {
                        hits += hitCount;
                    }

                    // score hits
                    scoreHits(hits);

                    // increase target
                    _currentTarget++;
                    setNewTargetText();
                    break;
                case 2:
                    if (hitCount == 3)
                    {
                        hits += (hitCount * 2) + 6;
                    }
                    else if (hitCount == 2)
                    {
                        hits += (hitCount * 2) + 2;
                    }
                    else
                    {
                        hits += hitCount * 2;
                    }

                    // score hits
                    scoreHits(hits);

                    // increase target
                    _currentTarget++;
                    setNewTargetText();
                    break;
                case 3:
                    if (hitCount == 3)
                    {
                        hits += (hitCount * 3) + 9;
                    }
                    else if (hitCount == 2)
                    {
                        hits += (hitCount * 3) + 3;
                    }
                    else
                    {
                        hits += hitCount * 3;
                    }

                    // score hits
                    scoreHits(hits);

                    // increase target
                    _currentTarget++;
                    setNewTargetText();
                    break;
                default:
                    break;
            }
        }

        private void scoreHits(int hits)
        {
            RunningScore += hits;
        }

        private string _currentTargetText;
        public string CurrentTargetText
        {
            get => _currentTargetText;
            set => SetProperty(ref _currentTargetText, value);
        }

        private int _runningScore = 0;
        public int RunningScore
        {
            get => _runningScore;
            set => SetProperty(ref _runningScore, value);
        }

        private bool _showEndOptions = false;
        public bool ShowEndOptions
        {
            get => _showEndOptions;
            set => SetProperty(ref _showEndOptions, value);
        }

        public ThreeBelgiansViewModel()
        {
            Title = "Three Belgians";
            SingleHitCommand = new Command<string>(scoreTargetsHit);
            DoubleHitCommand = new Command<string>(scoreTargetsHit);
            TrebleHitCommand = new Command<string>(scoreTargetsHit);
            NoHitCommand = new Command<string>(scoreTargetsHit);
            QuitGameCommand = new Command(quit);
            NewGameCommand = new Command(newGame);
            setNewTargetText();
        }

        private void newGame()
        {
            _currentTarget = 0;
            RunningScore = 0;
            firstTarget = true;
            setNewTargetText();
            ShowEndOptions = false;
        }

        private async void quit()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
