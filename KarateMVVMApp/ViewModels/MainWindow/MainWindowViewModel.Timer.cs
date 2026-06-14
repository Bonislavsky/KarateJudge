using CommunityToolkit.Mvvm.Input;
using System;

namespace KarateMVVMApp.ViewModels
{
    public partial class MainWindowViewModel
    {
        private async void Timer_Tick(object? sender, EventArgs e)
        {
            if (!Settings.HasTimeLimit)
            {
                TimerDisplay = Settings.InfiniteTimeDisplay;
                return;
            }

            _timeRemaining = _timeRemaining.Subtract(TimeSpan.FromSeconds(1));
            TimerDisplayToString();

            if (_timeRemaining.TotalSeconds <= 15 && _timeRemaining.TotalSeconds > 0)
            {
                TimerColor = Settings.AlertColorTimer;
                if (!string.IsNullOrEmpty(Settings.SelectedWarningSound)) await PlayAudioAsync(Settings.SelectedWarningSound);
            }
            else
            {
                TimerColor = Settings.DefaultColorTimer;
            }

            if (_timeRemaining.TotalSeconds <= 0)
            {
                _timer.Stop();
                StopAudio();
                IsTimerRunning = false;
                TimerColor = Settings.DefaultColorTimer;

                CheckWinnerAfterTimeout();
            }
        }

        [RelayCommand]
        private void StartTimer()
        {
            if (_timer.IsEnabled) return;
            if (Settings.HasTimeLimit && _timeRemaining.TotalSeconds <= 0) return;

            _timer.Start();
            ResumeAudio();
            IsTimerRunning = true;
            MatchStatusInProgress();
        }

        [RelayCommand]
        private void PauseTimer()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                PauseAudio();
                IsTimerRunning = false;
                _selectedMinutes = _timeRemaining.Minutes;
                _selectedSeconds = _timeRemaining.Seconds;
                OnPropertyChanged(nameof(SelectedMinutes));
                OnPropertyChanged(nameof(SelectedSeconds));
            }
        }

        partial void OnSelectedMinutesChanged(int value)
        {
            if (!_timer.IsEnabled && Settings.HasTimeLimit)
            {
                _timeRemaining = new TimeSpan(0, value, _timeRemaining.Seconds);
                TimerDisplayToString();
                StopAudio();
            }
        }

        partial void OnSelectedSecondsChanged(int value)
        {
            if (!_timer.IsEnabled && Settings.HasTimeLimit)
            {
                _timeRemaining = new TimeSpan(0, _timeRemaining.Minutes, value);
                TimerDisplayToString();
                StopAudio();
            }
        }

        private void TimerDisplayToString()
        {
            if (Settings.HasTimeLimit)
            {
                TimerDisplay = _timeRemaining.ToString(@"mm\:ss");
            }
            else
            {
                TimerDisplay = Settings.InfiniteTimeDisplay;
            }
        }

        [RelayCommand]
        private void AddOverTime(string value)
        {
            if (!_timer.IsEnabled && Settings.HasTimeLimit)
            {
                _timeRemaining = _timeRemaining.Add(TimeSpan.FromSeconds(int.Parse(value)));

                _selectedMinutes = _timeRemaining.Minutes;
                _selectedSeconds = _timeRemaining.Seconds;

                OnPropertyChanged(nameof(SelectedMinutes));
                OnPropertyChanged(nameof(SelectedSeconds));

                TimerDisplayToString();
                StopAudio();
            }
        }
    }
}