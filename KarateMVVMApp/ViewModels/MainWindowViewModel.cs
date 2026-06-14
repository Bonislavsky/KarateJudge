using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using KarateMVVMApp.Helpers;
using KarateMVVMApp.Services;
using NAudio.Wave;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace KarateMVVMApp.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private SecondWindowViewModel? _secondWindowVM;
        private readonly DispatcherTimer _timer;
        private TimeSpan _timeRemaining;
        private readonly KarateRulesService _rulesService;
        public GameSettings Settings { get; }

        private IWavePlayer? _audioPlayer;
        private WaveStream? _audioStream;
        private bool _isPlaying = false;
        public bool IsAudioPlaying => _isPlaying;

        [ObservableProperty] private ObservableCollection<int> _availableMinutesList;
        [ObservableProperty] private ObservableCollection<int> _availableSecondsList;
        [ObservableProperty] private int _selectedMinutes;
        [ObservableProperty] private int _selectedSeconds;
        [ObservableProperty] private int _akaPoints;
        [ObservableProperty] private int _aoPoints;
        [ObservableProperty] private int _akaCat1Count;
        [ObservableProperty] private int _akaCat2Count;
        [ObservableProperty] private int _aoCat1Count;
        [ObservableProperty] private int _aoCat2Count;
        [ObservableProperty] private string _timerDisplay;
        [ObservableProperty] private string _akaName;
        [ObservableProperty] private string _aoName;
        [ObservableProperty] private string _timerColor;
        [ObservableProperty] private bool _visibilityTimer;
        [ObservableProperty] private bool _isExportSuccess;
        [ObservableProperty] private bool _isTimerRunning;
        [ObservableProperty] private bool _visibilityCata;
        [ObservableProperty] private string _matchStatus;

        public MainWindowViewModel()
        {
            _akaPoints = 0;
            _akaCat1Count = 0;
            _akaCat2Count = 0;
            _akaName = "";

            _isTimerRunning = false;

            _aoPoints = 0;
            _aoCat1Count = 0;
            _aoCat2Count = 0;
            _aoName = "";

            _availableSecondsList = new ObservableCollection<int>(Enumerable.Range(0, 60));
            _availableMinutesList = new ObservableCollection<int>(Enumerable.Range(0, 11));

            _selectedSeconds = 0;
            _selectedMinutes = 0;

            _visibilityCata = true;
            _visibilityTimer = true;
            _matchStatus = "The match has not started yet";

            _timer = new DispatcherTimer();
            _timerDisplay = "00:00";
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            Settings = App.GameSettings;
            _rulesService = new KarateRulesService(Settings);

            _timerColor = Settings.DefaultColorTimer;
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Tick -= Timer_Tick;

            _soundCts?.Cancel();
            _soundCts?.Dispose();
            _soundCts = null;

            StopAudio();
        }
    }
}