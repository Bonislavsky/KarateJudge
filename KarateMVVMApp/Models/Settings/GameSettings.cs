using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using KarateMVVMApp.Models.Options;

namespace KarateMVVMApp.Helpers
{
    public partial class GameSettings : ObservableObject
    {
        [ObservableProperty] private int _pointsToWin;
        [ObservableProperty] private int _warningsToDisqualify;
        [ObservableProperty] private string _colorAka;
        [ObservableProperty] private string _colorAo;
        [ObservableProperty] private string _defaultColorTimer;
        [ObservableProperty] private string _alertColorTimer;
        [ObservableProperty] private string _infiniteTimeDisplay;
        [ObservableProperty] private bool _hasTimeLimit;
        [ObservableProperty] private bool _hasPointLimit;
        [ObservableProperty] private bool _clearNameOnReset;
        [ObservableProperty] private Bitmap? _tournamentLogo;
        [ObservableProperty] private string? _selectedWarningSound;
        [ObservableProperty] private string? _selectedVictorySound;
        [ObservableProperty] private ResetTimeOption? _selectedTimeToReset;
        [ObservableProperty] private string _drawColor;

        public GameSettings()
        {
            _tournamentLogo = null;
            _infiniteTimeDisplay = "∞";
            _defaultColorTimer = "#FFFFFF";
            _alertColorTimer = "Orange";
            _warningsToDisqualify = 4;
            _selectedWarningSound = "avares://KarateMVVMApp/Assets/Sounds/TimerWarning0.mp3";
            _selectedVictorySound = "avares://KarateMVVMApp/Assets/Sounds/Finish.mp3";
            _pointsToWin = 10;
            _colorAka = "#E57272";
            _colorAo = "#729CE5";
            _drawColor = "#5a687f";
            _hasTimeLimit = true;
            _hasPointLimit = true;
            _clearNameOnReset = true;
        }
    }
}
