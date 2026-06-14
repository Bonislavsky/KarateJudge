using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KarateMVVMApp.Helpers;
using KarateMVVMApp.Models.Options;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KarateMVVMApp.ViewModels
{
    public partial class SettingsWindowViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainWindowVM;
        private readonly Action _closeAction;
        public GameSettings Settings { get; }

        [ObservableProperty] private int? _pointsToWin;
        [ObservableProperty] private bool _hasTimeLimit;
        [ObservableProperty] private ObservableCollection<ColorOption> _availableColors;
        [ObservableProperty] private ColorOption _selectedAkaColor;
        [ObservableProperty] private ColorOption _selectedAoColor;
        [ObservableProperty] private bool _haslimitedPoints;
        [ObservableProperty] private bool _clearNameOnReset;
        [ObservableProperty] private ObservableCollection<ResetTimeOption> _timeToReset;
        [ObservableProperty] private ResetTimeOption _selectedTimeToReset;

        public SettingsWindowViewModel(MainWindowViewModel mainWindowVM, GameSettings settings, Action closeAction)
        {
            _mainWindowVM = mainWindowVM;
            _closeAction = closeAction;
            Settings = settings;

            _availableColors = new ObservableCollection<ColorOption>
            {
                new ColorOption("Red", "#E57272"),
                new ColorOption("Blue", "#729CE5"),
                new ColorOption("White", "#FFFFFF")
            };

            _timeToReset = new ObservableCollection<ResetTimeOption>
            {
                new("00:00", TimeSpan.Zero),
                new("00:30", TimeSpan.FromSeconds(30)),
                new("01:00", TimeSpan.FromMinutes(1)),
                new("01:30", TimeSpan.FromSeconds(90)),
                new("02:00", TimeSpan.FromMinutes(2)),
                new("02:30", TimeSpan.FromSeconds(150)),
                new("03:00", TimeSpan.FromMinutes(3)),
                new("03:30", TimeSpan.FromSeconds(210)),
                new("04:00", TimeSpan.FromMinutes(4)),
                new("05:00", TimeSpan.FromMinutes(5)),
                new("06:00", TimeSpan.FromMinutes(6)),
                new("07:00", TimeSpan.FromMinutes(7)),
                new("08:00", TimeSpan.FromMinutes(8)),
                new("09:00", TimeSpan.FromMinutes(9)),
                new("10:00", TimeSpan.FromMinutes(10)),
            };

            _pointsToWin = Settings.PointsToWin;

            _haslimitedPoints = Settings.HasPointLimit;
            _hasTimeLimit = Settings.HasTimeLimit;
            _clearNameOnReset = Settings.ClearNameOnReset;

            _selectedTimeToReset = _timeToReset.FirstOrDefault(t => t.Value.Equals(Settings.SelectedTimeToReset?.Value)) ?? _timeToReset[0];

            _selectedAkaColor = _availableColors.FirstOrDefault(c => c.HexValue == Settings.ColorAka) ?? _availableColors[0];
            _selectedAoColor = _availableColors.FirstOrDefault(c => c.HexValue == Settings.ColorAo) ?? _availableColors[1];
        }

        [RelayCommand]
        private async Task UploadLogo()
        {
            if (App.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            {
                return;
            }

            var storageProvider = desktop.MainWindow?.StorageProvider;
            if (storageProvider is null) return;

            var imageTypes = new FilePickerFileType("Images")
            {
                Patterns = new[] { "*.png", "*.jpg", "*.jpeg" }
            };

            var result = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select Tournament Logo",
                AllowMultiple = false,
                FileTypeFilter = [imageTypes]
            });

            if (result.Count == 0) return;

            try
            {
                var file = result[0];
                using var stream = await file.OpenReadAsync();
                var bitmap = new Bitmap(stream);
                Settings.TournamentLogo = bitmap;
            }
            catch (Exception)
            {
            }
        }

        [RelayCommand]
        private void ResetLogo()
        {
            Settings.TournamentLogo = null;
        }

        [RelayCommand]
        private void ApplyPreset(string? preset)
        {
            if (string.IsNullOrEmpty(preset))
                return;

            if (preset.Equals(Settings.InfiniteTimeDisplay))
            {
                _mainWindowVM.TimerDisplay = Settings.InfiniteTimeDisplay;
                return;
            }

            var parts = preset.Split(':');
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int minutes) &&
                int.TryParse(parts[1], out int seconds))
            {
                _mainWindowVM.SelectedMinutes = minutes;
                _mainWindowVM.SelectedSeconds = seconds;

            }
        }

        private void ApplyPresetConfiguration(bool valueToSet)
        {
            HaslimitedPoints = valueToSet;
            HasTimeLimit = valueToSet;
            ToggleCATAVisibility(valueToSet);
            ToggleTimerSeterVisibility(valueToSet);
        }

        [RelayCommand]
        private void ApplyMatchPreset(string? presetName)
        {
            if (string.IsNullOrEmpty(presetName))
                return;

            _mainWindowVM.ResetForPreset();

            switch (presetName)
            {
                case "ShobuIppon":
                    ApplyPresetConfiguration(true);
                    SelectedAkaColor = _availableColors.First(c => c.Name == "Red");
                    SelectedAoColor = _availableColors.First(c => c.Name == "White");
                    ApplyPreset("2:00");
                    PointsToWin = 2;
                    SetResetTime(TimeSpan.FromMinutes(2));
                    break;

                case "ShobuSanbon":
                    ApplyPresetConfiguration(true);
                    SelectedAkaColor = _availableColors.First(c => c.Name == "Red");
                    SelectedAoColor = _availableColors.First(c => c.Name == "Blue");
                    ApplyPreset("2:00");
                    PointsToWin = 6;
                    SetResetTime(TimeSpan.FromMinutes(2));
                    break;

                case "ShobuNihon":
                    ApplyPresetConfiguration(true);
                    SelectedAkaColor = _availableColors.First(c => c.Name == "Red");
                    SelectedAoColor = _availableColors.First(c => c.Name == "Blue");
                    SetResetTime(TimeSpan.FromSeconds(90));
                    ApplyPreset("1:30");
                    PointsToWin = 4;
                    break;

                case "Kata":
                    ApplyPresetConfiguration(false);
                    SelectedAkaColor = _availableColors.First(c => c.Name == "Red");
                    SelectedAoColor = _availableColors.First(c => c.Name == "Blue");
                    ApplyPreset(Settings.InfiniteTimeDisplay);
                    break;
            }
        }

        private void SetResetTime(TimeSpan time)
        {
            SelectedTimeToReset = _timeToReset.FirstOrDefault(t => t.Value.Equals(time)) ?? _timeToReset[0];
        }

        [RelayCommand]
        private void ApplyAndClose()
        {
            Settings.HasPointLimit = HaslimitedPoints;
            Settings.PointsToWin = HaslimitedPoints ? (PointsToWin ?? 0) : 0;

            Settings.ColorAka = SelectedAkaColor?.HexValue ?? "#E57272";
            Settings.ColorAo = SelectedAoColor?.HexValue ?? "#729CE5";

            Settings.HasTimeLimit = HasTimeLimit;
            Settings.ClearNameOnReset = ClearNameOnReset;

            Settings.SelectedTimeToReset = SelectedTimeToReset;

            _mainWindowVM.StopAudio();
            Close();
        }

        private void ToggleCATAVisibility(bool value)
        {
            _mainWindowVM.VisibilityCata = value;
        }

        private void ToggleTimerSeterVisibility(bool value)
        {
            _mainWindowVM.VisibilityTimer= value;
        }

        [RelayCommand]
        private void Close()
        {
            _closeAction?.Invoke();
        }

        partial void OnHasTimeLimitChanged(bool value)
        {
            ToggleTimerSeterVisibility(value);
            Settings.HasTimeLimit = value;
        }
    }
}
