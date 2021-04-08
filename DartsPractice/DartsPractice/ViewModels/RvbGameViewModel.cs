using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace DartsPractice.ViewModels
{
    public class RvbGameViewModel : BaseViewModel
    {
        public Command<string> ScoreCommand { get; }
        public ICommand ReturnHomeCommand { get; }

        private int lastScore = 0;
        private int runningTotal = 0;
        private int roundCount = 0;

        public RvbGameViewModel()
        {
            Title = "RVB Game";
            ScoreCommand = new Command<string>(updateScore);
            ReturnHomeCommand = new Command(returnHomeCommand);
        }

        private void updateScore(string score)
        {
            if (roundCount < 10)
            {
                // set lastScore to runningTotal
                // update runningTotal by score
                // update roundcount
                // update Running total on the screen
            } else
            {
                // save to db
                // show statistics
                // show return button
            }
        }

        private async void returnHomeCommand()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
