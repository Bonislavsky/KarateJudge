namespace KarateMVVMApp.ViewModels
{
    public partial class MainWindowViewModel
    {
        partial void OnVisibilityCataChanged(bool value) => UpdateSecondWindow(vm => vm.VisibilityCata = value);
        partial void OnAkaPointsChanged(int value) => UpdateSecondWindow(vm => vm.AkaPoints = value);
        partial void OnAkaCat1CountChanged(int value) => UpdateSecondWindow(vm => vm.AkaCat1Count = value);
        partial void OnAkaCat2CountChanged(int value) => UpdateSecondWindow(vm => vm.AkaCat2Count = value);

        partial void OnAoPointsChanged(int value) => UpdateSecondWindow(vm => vm.AoPoints = value);
        partial void OnAoCat1CountChanged(int value) => UpdateSecondWindow(vm => vm.AoCat1Count = value);
        partial void OnAoCat2CountChanged(int value) => UpdateSecondWindow(vm => vm.AoCat2Count = value);

        partial void OnTimerDisplayChanged(string value) => UpdateSecondWindow(vm => vm.TimerDisplay = value);
        partial void OnTimerColorChanged(string value) => UpdateSecondWindow(vm => vm.TimerColor = value);

        partial void OnAkaNameChanged(string value) => UpdateSecondWindow(vm => vm.AkaName = value);
        partial void OnAoNameChanged(string value) => UpdateSecondWindow(vm => vm.AoName = value);

        private void UpdateSecondWindow(System.Action<SecondWindowViewModel> update)
        {
            if (_secondWindowVM != null)
            {
                update(_secondWindowVM);
            }
        }
    }
}
