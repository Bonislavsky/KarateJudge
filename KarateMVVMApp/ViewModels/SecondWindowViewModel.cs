using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using KarateMVVMApp.Helpers;
using System;
using System.Linq;

namespace KarateMVVMApp.ViewModels
{
    public partial class SecondWindowViewModel : ViewModelBase
    {
        public GameSettings Settings { get; }

        [ObservableProperty] private int _akaPoints;
        [ObservableProperty] private int _aoPoints;
        [ObservableProperty] private int _akaCat1Count;
        [ObservableProperty] private int _akaCat2Count;
        [ObservableProperty] private int _aoCat1Count;
        [ObservableProperty] private int _aoCat2Count;
        [ObservableProperty] private bool _isGameOver;
        [ObservableProperty] private string _winnerColor;
        [ObservableProperty] private string _winnerText;
        [ObservableProperty] private string _timerColor;
        [ObservableProperty] private bool _visibilityCata;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayTimer))]
        private string _timerDisplay;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayAkaName))]
        private string _akaName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayAoName))]
        private string _aoName;

        private DispatcherTimer? _gameOverTimer;

        public string DisplayAkaName => string.IsNullOrWhiteSpace(_akaName) ? "AKA" : _akaName;
        public string DisplayAoName => string.IsNullOrWhiteSpace(_aoName) ? "AO" : _aoName;
        public string DisplayTimer => string.IsNullOrWhiteSpace(_timerDisplay) ? "00:00" : _timerDisplay;

        public SecondWindowViewModel(GameSettings settings)
        {
            Settings = settings;
        }

        public void GameOver(string winColor)
        {
            IsGameOver = true;
            WinnerColor = winColor;
            WinnerText = winColor.Equals(Settings.DrawColor) ? "DRAW" : "WINNER!";
        }

        public void ExecuteFullScreenSpectatorsWindow()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var spectatorWindow = desktop.Windows.FirstOrDefault(w => w.DataContext == this);

                if (spectatorWindow != null)
                {
                    spectatorWindow.WindowState = spectatorWindow.WindowState == WindowState.FullScreen
                        ? WindowState.Normal
                        : WindowState.FullScreen;
                }
            }
        }

        partial void OnIsGameOverChanged(bool value)
        {
            if (value)
            {
                StartAutoHide();
            }
            else
            {
                StopGameOverTimer();
            }
        }

        private void StartAutoHide()
        {
            _gameOverTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };

            _gameOverTimer.Tick += (s, e) =>
            {
                IsGameOver = false;
                StopGameOverTimer();
            };

            _gameOverTimer.Start();
        }

        private void StopGameOverTimer()
        {
            if (_gameOverTimer != null)
            {
                _gameOverTimer.Stop();
                _gameOverTimer = null;
            }
        }
    }
}
