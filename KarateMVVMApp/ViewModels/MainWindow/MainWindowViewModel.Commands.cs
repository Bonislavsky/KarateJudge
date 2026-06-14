using CommunityToolkit.Mvvm.Input;
using System;

namespace KarateMVVMApp.ViewModels
{
    public partial class MainWindowViewModel
    {
        [RelayCommand]
        private void ResetMatch()
        {
            ResetForPreset();
            MatchStatusNotStarted();
        }

        public void ResetForPreset()
        {
            ResetMatchData();
            ResetTimer();
            ResetSecondWindow();
        }

        private void ResetMatchData()
        {
            if (Settings.ClearNameOnReset)
            {
                AkaName = string.Empty;
                AoName = string.Empty;
            }

            AkaPoints = 0;
            AkaCat1Count = 0;
            AkaCat2Count = 0;
            AoPoints = 0;
            AoCat1Count = 0;
            AoCat2Count = 0;
        }

        private void ResetTimer()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }

            IsTimerRunning = false;
            StopAudio();
            TimerColor = Settings.DefaultColorTimer;

            _timeRemaining = Settings.SelectedTimeToReset?.Value ?? TimeSpan.Zero;
            TimerDisplayToString();
            SelectedMinutes = Settings.SelectedTimeToReset?.Value.Minutes ?? 0;
            SelectedSeconds = Settings.SelectedTimeToReset?.Value.Seconds ?? 0;
        }

        private void ResetSecondWindow()
        {
            if (_secondWindowVM != null)
            {
                _secondWindowVM.IsGameOver = false;
            }
        }
    }
}