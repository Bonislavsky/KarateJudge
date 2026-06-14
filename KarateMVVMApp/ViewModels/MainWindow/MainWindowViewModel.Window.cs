using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;
using KarateMVVMApp.Views;
using System.Linq;

namespace KarateMVVMApp.ViewModels
{
    public partial class MainWindowViewModel
    {
        [RelayCommand]
        private void ToggleFullScreen()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = desktop.MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.WindowState = mainWindow.WindowState == WindowState.FullScreen
                        ? WindowState.Normal
                        : WindowState.FullScreen;
                }
            }
        }

        [RelayCommand]
        private void FullScreenSpectatorsWindow()
        {
            _secondWindowVM?.ExecuteFullScreenSpectatorsWindow();
        }

        [RelayCommand]
        private void CloseApplication()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }

        [RelayCommand]
        private void OpenSettings()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var settingsWindow = new SettingsWindow
                {
                    DataContext = new SettingsWindowViewModel(this, Settings, () =>
                    {
                        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime d)
                        {
                            d.Windows.FirstOrDefault(w => w is SettingsWindow)?.Close();
                        }
                    })
                };

                settingsWindow.ShowDialog(desktop.MainWindow);
            }
        }
    }
}
