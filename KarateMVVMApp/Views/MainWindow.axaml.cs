using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using KarateMVVMApp.ViewModels;
using System;

namespace KarateMVVMApp.Views
{
    public partial class MainWindow : Window
    {
        private SecondWindow? _secondWindow;

        public MainWindow()
        {
            InitializeComponent();
            Opened += OnMainWindowOpened;
            Closed += OnMainWindowClosed;
        }

        private void OnMainWindowClosed(object? sender, EventArgs e)
        {
            _secondWindow?.Close();
            _secondWindow = null;

            if (DataContext is MainWindowViewModel vm)
            {
                vm.Dispose();
            }

            Closed -= OnMainWindowClosed;

            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }

        private void OnMainWindowOpened(object? sender, EventArgs e)
        {
            if (DataContext is MainWindowViewModel mainVM)
            {
                var secondVM = new SecondWindowViewModel(App.GameSettings);
                mainVM.SetSecondWindowViewModel(secondVM);

                _secondWindow = new SecondWindow
                {
                    DataContext = secondVM
                };

                _secondWindow.Show(this);
            }

            Opened -= OnMainWindowOpened;
        }
    }
}
