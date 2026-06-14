using CommunityToolkit.Mvvm.Input;
using KarateMVVMApp.Models;
using KarateMVVMApp.Services;
using System.Net.NetworkInformation;

namespace KarateMVVMApp.ViewModels
{
    public partial class MainWindowViewModel
    {
        public void SetSecondWindowViewModel(SecondWindowViewModel vm)
        {
            _secondWindowVM = vm;
            _secondWindowVM.TimerDisplay = TimerDisplay;
            _secondWindowVM.TimerColor = TimerColor;

            _secondWindowVM.AkaName = AkaName;
            _secondWindowVM.AkaPoints = AkaPoints;
            _secondWindowVM.AkaCat1Count = AkaCat1Count;
            _secondWindowVM.AkaCat2Count = AkaCat2Count;

            _secondWindowVM.AoName = AoName;
            _secondWindowVM.AoPoints = AoPoints;
            _secondWindowVM.AoCat1Count = AoCat1Count;
            _secondWindowVM.AoCat2Count = AoCat2Count;

            _secondWindowVM.VisibilityCata = VisibilityCata;
        }

        private void CheckForWinner()
        {
            MatchResult? result = _rulesService.Check(new MatchState(
                AkaPoints: AkaPoints,
                AoPoints: AoPoints,
                AkaCat1Count: AkaCat1Count,
                AoCat1Count: AoCat1Count,
                AkaCat2Count: AkaCat2Count,
                AoCat2Count: AoCat2Count
            ));

            HandleMatchResult(result);
        }

        private void CheckWinnerAfterTimeout()
        {
            MatchResult result = _rulesService.CheckWinnerAfterTimeout(AkaPoints, AoPoints);
            HandleMatchResult(result);
        }

        private void HandleMatchResult(MatchResult? result)
        {
            if (result == null) return;

            DisplayWinner(result.WinnerColor);
            MatchStatusFinished();

            if (_timer.IsEnabled)
            {
                _timer.Stop();
                IsTimerRunning = false;
            }
        }

        private void DisplayWinner(string winColor)
        {
            _secondWindowVM?.GameOver(winColor);

            if (!string.IsNullOrEmpty(Settings.SelectedVictorySound)) PlayWinnerSound(Settings.SelectedVictorySound);
        }

        [RelayCommand]
        private void PauseResumeMatch()
        {
            if (IsTimerRunning) PauseTimer();
            else if (!IsTimerRunning) StartTimer();
        }

        public void MatchStatusNotStarted()
        {
            MatchStatus = "The match has not started yet";
        }

        public void MatchStatusInProgress()
        {
            MatchStatus = "The match is still in progress";
        }

        public void MatchStatusFinished()
        {
            MatchStatus = "The match is over, viewers can see the winner";
        }
    }
}
