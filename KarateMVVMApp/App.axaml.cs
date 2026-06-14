using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using KarateMVVMApp.Helpers;
using KarateMVVMApp.Services;
using KarateMVVMApp.ViewModels;
using KarateMVVMApp.Views;
using System.Linq;

namespace KarateMVVMApp
{
    public partial class App : Application
    {
        public static GameSettings GameSettings { get; } = new GameSettings();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DisableAvaloniaDataAnnotationValidation();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}
