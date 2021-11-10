using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DartsPractice.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace DartsPractice.ViewModels
{
    public class A1ViewModel : BaseViewModel
    {
        List<int> initialState = new List<int> { 0, 0, 0, 0, 0 };
        List<string> _targetList = new List<string> { "20", "19", "18", "17", "16", "15", "14", "13", "Bull" };
        public ICommand HitCommand { get; }
        public ICommand MissCommand { get; }
        private int _currentTarget = 0;
        private int _roundCount = 0;
        private const int MAX_HITS = 5;
        private const int LAST_ROUND = 8;
        private const int FIRST_ROUND = 0;

        //SlateGray = inactive
        //LightBlue = active

        public A1Target twentyTarget = new A1Target();
        private A1Target nineteenTarget = new A1Target();
        private A1Target eighteenTarget = new A1Target();
        private A1Target seventeenTarget = new A1Target();
        private A1Target sixteenTarget = new A1Target();
        private A1Target fifteenTarget = new A1Target();
        private A1Target fourteenTarget = new A1Target();
        private A1Target thirteentwentyTarget = new A1Target();
        private A1Target bullTarget = new A1Target();

        List<A1Target> newTargetList = new List<A1Target>();

        private void setInitialState()
        {
            newTargetList.Add(twentyTarget);
            newTargetList.Add(nineteenTarget);
            newTargetList.Add(eighteenTarget);
            newTargetList.Add(seventeenTarget);
            newTargetList.Add(sixteenTarget);
            newTargetList.Add(fifteenTarget);
            newTargetList.Add(fourteenTarget);
            newTargetList.Add(thirteentwentyTarget);
            newTargetList.Add(bullTarget);

            foreach (var target in newTargetList)
            {
                target.Hits = new List<int>();
            }
        }

        private string getCurrentTarget()
        {
            return _targetList[_currentTarget];
        }

        private int getHitCount()
        {
            var hitcount = getScoringSegment().Hits.Where(x => x.Equals(1)).Count();
            return hitcount;
        }

        private A1Target getScoringSegment()
        {
            return newTargetList[_currentTarget];
        }

        private void scoreSegment()
        {
            var scoringSegment = getScoringSegment();
            int hitCount = getHitCount();
            hitCount++;

            // if this hit closes with max hits, it shouldn't show up here again
            if (hitCount == MAX_HITS)
            {
                scoringSegment.IsClosed = true;
            }

            // remove old data
            scoringSegment.Hits.Clear();

            //add the hits
            for (int i = 0; i < hitCount; i++)
            {
                scoringSegment.Hits.Add(1);
            }

            //add the remaining misses
            for (int i = hitCount; i < MAX_HITS; i++)
            {
                scoringSegment.Hits.Add(0);
            }
            //}
        }

        private void checkTargetIsOpen()
        {
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
        }

        private void TargetMissed(object obj)
        {
            Console.WriteLine($"Target was {getCurrentTarget()}");
            Console.WriteLine($"{getCurrentTarget()} has been hit {getHitCount()} times");

            // deactivate the last target
            getScoringSegment().IsActive = false;
            Console.WriteLine($"{getCurrentTarget()} is active: {getScoringSegment().IsActive}");

            // set the new target
            checkTargetIsOpen();

            //highlight and activate the new target
            getScoringSegment().IsActive = true;
            Console.WriteLine($"{getCurrentTarget()} is active: {getScoringSegment().IsActive}");

            //increase total round count
            _roundCount++;

            Console.WriteLine($"New target is {getCurrentTarget()}\n");
        }

        private void TargetHit()
        {
            scoreSegment();
            Console.WriteLine($"\nTarget was {getCurrentTarget()}");
            Console.WriteLine($"{getCurrentTarget()} has been hit {getHitCount()} times");

            // deactivate the last target
            getScoringSegment().IsActive = false;
            Console.WriteLine($"{getCurrentTarget()} is active: {getScoringSegment().IsActive}");

            //set the new target
            checkTargetIsOpen();

            // highlight the new active target
            getScoringSegment().IsActive = true;
            Console.WriteLine($"{getCurrentTarget()} is active: {getScoringSegment().IsActive}");

            //increase total round count
            _roundCount++;

            Console.WriteLine($"New target is {getCurrentTarget()}\n");
        }
    }
}
