using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using DartsPractice.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace DartsPractice.ViewModels
{
    public class A1ViewModel : BaseViewModel
    {
        List<string> _targetList = new List<string> { "20", "19", "18", "17", "16", "15", "14", "13", "Bull" };
        public ICommand HitCommand { get; }
        public ICommand MissCommand { get; }
        public ICommand ReturnHomeCommand { get; }
        public ICommand NewGameCommand { get; }

        private int _currentTarget = 0;
        private int _roundCount = 0;
        private bool _gameStarted = false;
        private bool _endOfGame = false;
        private bool _showEndgameScreen = false;
        private int _closedSegmentCount = 0;
        private List<A1Target> newTargetList = new List<A1Target>();

        private string _dartsThrown;
        private string _totalTimeTaken;
        private Stopwatch _timer = new Stopwatch();
        
        private const int FIRST_ROUND = 0;
        private const int MAX_HITS = 5;
        private const int LAST_ROUND = 8;
        private const int TOTAL_TARGETS = 9;    

        public bool HideButtons
        {
            get => !_showEndgameScreen;
            set
            {
                HideButtons = !value;
                OnPropertyChanged(nameof(HideButtons));
            }            
        }

        public bool ShowEndgameScreen
        {
            get => _showEndgameScreen;
            set
            {
                if (_showEndgameScreen != value)
                    _showEndgameScreen = value;
                    OnPropertyChanged(nameof(ShowEndgameScreen));
                    OnPropertyChanged(nameof(HideButtons));
            }
        }

        public string TotalTimeTaken
        {
            get => _totalTimeTaken;
            set => SetProperty(ref _totalTimeTaken, value);
        }

        public string DartsThrown
        {
            get => _dartsThrown;
            set => SetProperty(ref _dartsThrown, value);
        }

        private A1Target _twentyTarget = new A1Target();

        public A1Target TwentyTarget
        {
            get => _twentyTarget;
            set => SetProperty(ref _twentyTarget, value);
        }
        private A1Target _nineteenTarget = new A1Target();
        public A1Target NineteenTarget
        {
            get => _nineteenTarget;
            set => SetProperty(ref _nineteenTarget, value);
        }
        private A1Target _eighteenTarget = new A1Target();
        public A1Target EighteenTarget
        {
            get => _eighteenTarget;
            set => SetProperty(ref _eighteenTarget, value);
        }
        private A1Target _seventeenTarget = new A1Target();
        public A1Target SeventeenTarget
        {
            get => _seventeenTarget;
            set => SetProperty(ref _seventeenTarget, value);
        }
        private A1Target _sixteenTarget = new A1Target();
        public A1Target SixteenTarget
        {
            get => _sixteenTarget;
            set => SetProperty(ref _sixteenTarget, value);
        }
        private A1Target _fifteenTarget = new A1Target();
        public A1Target FifteenTarget
        {
            get => _fifteenTarget;
            set => SetProperty(ref _fifteenTarget, value);
        }
        private A1Target _fourteenTarget = new A1Target();
        public A1Target FourteenTarget
        {
            get => _fourteenTarget;
            set => SetProperty(ref _fourteenTarget, value);
        }
        private A1Target _thirteenTarget = new A1Target();
        public A1Target ThirteenTarget
        {
            get => _thirteenTarget;
            set => SetProperty(ref _thirteenTarget, value);
        }
        private A1Target _bullTarget = new A1Target();
        public A1Target BullTarget
        {
            get => _bullTarget;
            set => SetProperty(ref _bullTarget, value);
        }

        private string getCurrentTarget()
        {
            return _targetList[_currentTarget];
        }

        private int getHitCount()
        {
            var hitcount = getScoringSegment().Hits.Where(x => x.Equals(true)).Count();
            return hitcount;
        }

        private void setInitialState()
        {
            newTargetList.Add(TwentyTarget);
            newTargetList.Add(NineteenTarget);
            newTargetList.Add(EighteenTarget);
            newTargetList.Add(SeventeenTarget);
            newTargetList.Add(SixteenTarget);
            newTargetList.Add(FifteenTarget);
            newTargetList.Add(FourteenTarget);
            newTargetList.Add(ThirteenTarget);
            newTargetList.Add(BullTarget);
            TwentyTarget.IsActive = true;
        }

        private A1Target getScoringSegment()
        {
            return newTargetList[_currentTarget];             
        }

        private void scoreSegment()
        {            
            var scoringSegment = getScoringSegment();
            ObservableCollection<bool> hits = new ObservableCollection<bool>();
            int hitCount = getHitCount();
            hitCount++;
            _roundCount++;

            // if this hit closes with max hits, it shouldn't show up here again
            if (hitCount == MAX_HITS)
            {
                scoringSegment.IsClosed = true;
                _closedSegmentCount++;
                checkEndOfGame();
            }            

            //add the hits
            for (int i = 0; i < hitCount; i++)
            {
                hits.Add(true);
            }

            //add the empty hits
            for (int i = hitCount; i < MAX_HITS; i++)
            {
                hits.Add(false);
            }

            scoringSegment.Hits = hits;
        }

        private void checkEndOfGame()
        {
            if (_closedSegmentCount == TOTAL_TARGETS)
            {
                // game over
                _timer.Stop();
                TotalTimeTaken = _timer.Elapsed.ToString(@"hh\:mm\:ss");

                _endOfGame = true;
                calculateTotalDartsThrown();

                // show endgame screen
                ShowEndgameScreen = true;
            }
        }

        private void calculateTotalDartsThrown()
        {
            DartsThrown = Convert.ToString(_roundCount * 3);
        }

        private void restartGame()
        {            
            _currentTarget = 0;
            _roundCount = 0;
            _gameStarted = false;
            _endOfGame = false;
            _closedSegmentCount = 0;
            ShowEndgameScreen = false;

            foreach (var target in newTargetList)
            {
                target.Hits = new ObservableCollection<bool>() { false, false, false, false, false};
                target.IsActive = false;
                target.IsClosed = false;
            }

            TwentyTarget.IsActive = true;
        }

        private void checkTargetIsOpen()
        {
            if (_endOfGame)
                return;

            goToNextTarget();

            var scoringTarget = getScoringSegment();

            if (scoringTarget.IsClosed)           
                checkTargetIsOpen();            
        }

        private void goToNextTarget()
        {
            if (_currentTarget == LAST_ROUND)
            {
                _currentTarget = FIRST_ROUND;
            }
            else
            {
                _currentTarget++;
            }
        }

        public A1ViewModel()
        {
            Title = "A1 - Practice Routine";
            setInitialState();
            HitCommand = new Command(TargetHit);
            MissCommand = new Command(TargetMissed);
            ReturnHomeCommand = new Command(returnHomeCommand);
            NewGameCommand = new Command(restartGame);
        }

        private void TargetMissed(object obj)
        {
            if (!_gameStarted)
                _timer.Start();

            Console.WriteLine($"Target was {getCurrentTarget()}");
            Console.WriteLine($"{getCurrentTarget()} has been hit {getHitCount()} times");

            // deactivate the last target
            getScoringSegment().IsActive = false;
            Console.WriteLine($"{getCurrentTarget()} is active: {getScoringSegment().IsActive}");

            //increase total round count
            _roundCount++;
            Console.WriteLine($"roundcount: {_roundCount}");

            // set the new target
            checkTargetIsOpen();

            //highlight and activate the new target
            getScoringSegment().IsActive = true;
            Console.WriteLine($"{getCurrentTarget()} is active: {getScoringSegment().IsActive}");

            Console.WriteLine($"New target is {getCurrentTarget()}\n");
        }

        private async void returnHomeCommand()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void TargetHit()
        {
            if (!_gameStarted)
                _timer.Start();

            scoreSegment();
            Console.WriteLine($"\nTarget was {getCurrentTarget()}");
            Console.WriteLine($"{getCurrentTarget()} has been hit {getHitCount()} times");

            // deactivate the last target
            getScoringSegment().IsActive = false;
            Console.WriteLine($"{getCurrentTarget()} is active: {getScoringSegment().IsActive}");            

            Console.WriteLine($"roundcount: {_roundCount}");

            //set the new target
            checkTargetIsOpen();

            // highlight the new active target
            getScoringSegment().IsActive = true;
            Console.WriteLine($"{getCurrentTarget()} is active: {getScoringSegment().IsActive}");

            Console.WriteLine($"New target is {getCurrentTarget()}\n");
        }
    }
}
