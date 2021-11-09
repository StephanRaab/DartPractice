﻿using System;
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
        List<int> initialState = new List<int> { 0,0,0,0,0 };
        List<string> _targetList = new List<string> { "20", "19", "18", "17", "16", "15", "14", "13", "Bull" };
        public ICommand HitCommand { get; }
        public ICommand MissCommand { get; }
        private int _currentTarget = 0;
        private int _roundCount = 0;
        private const int MAXHITS = 5;

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
                target.Hits = initialState;
            }            
        }

        private string getCurrentTarget()
        {
            return _targetList[_currentTarget];          
        }

        private int getHitCount()
        {
            var hitcount =  newTargetList[_currentTarget].Hits.Where(x => x.Equals(1)).Count();
            return hitcount;
        }

        private void scoreSegment()
        {
            int hitCount = getHitCount();
            hitCount++;

            if (_targetList.Count == 0)
            {
                //game over
                //show dialog with overview of total darts shot and time taken
                Console.WriteLine("GAME OVER!!");
            } else if (hitCount == MAXHITS) {
                //remove option from list
                _targetList.RemoveAt(_currentTarget);
                Console.WriteLine($"{getCurrentTarget()} is now CLOSED!");
            }
            else 
            {
                // remove old data
                A1Target currentTarget = newTargetList[_currentTarget];
                currentTarget.Hits.Clear();

                //add the hits
                for (int i = 0; i < hitCount; i++)
                {
                    currentTarget.Hits.Add(1);
                }

                //add the remaining misses
                for (int i = hitCount; i < 5; i++)
                {
                    currentTarget.Hits.Add(0);
                }
            }
        }

        private void increaseCurrentTarget()
        {
            if (_currentTarget == 8)
                _currentTarget = 0;
            else
                _currentTarget++;
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

            increaseCurrentTarget();
            _roundCount++;

            Console.WriteLine($"New target is {getCurrentTarget()}");
        }

        private void TargetHit()
        {
            scoreSegment();
            Console.WriteLine($"Target was {getCurrentTarget()}");
            Console.WriteLine($"{getCurrentTarget()} has been hit {getHitCount()} times");

            increaseCurrentTarget();
            _roundCount++;

            Console.WriteLine($"New target is {getCurrentTarget()}");
        }
    }
}
