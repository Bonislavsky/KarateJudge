using CommunityToolkit.Mvvm.Input;

namespace KarateMVVMApp.ViewModels
{
    public partial class MainWindowViewModel
    {
        [RelayCommand]
        private void AddAkaPoints(string? pointsString)
        {
            if (int.TryParse(pointsString, out int points))
            {
                AkaPoints = AkaPoints + points >= 0 ? AkaPoints + points : 0;
                CheckForWinner();
            }
        }

        [RelayCommand]
        private void AddAoPoints(string? pointsString)
        {
            if (int.TryParse(pointsString, out int points))
            {
                AoPoints = AoPoints + points >= 0 ? AoPoints + points : 0;
                CheckForWinner();
            }
        }

        [RelayCommand]
        private void AddAkaCat1()
        {
            if (AkaCat1Count < 4)
            {
                AkaCat1Count++;
                CheckForWinner();
            }
        }

        [RelayCommand]
        private void AddAkaCat2()
        {
            if (AkaCat2Count < 4)
            {
                AkaCat2Count++;
                CheckForWinner();
            }
        }

        [RelayCommand]
        private void AddAoCat1()
        {
            if (AoCat1Count < 4)
            {
                AoCat1Count++;
                CheckForWinner();
            }
        }

        [RelayCommand]
        private void AddAoCat2()
        {
            if (AoCat2Count < 4)
            {
                AoCat2Count++;
                CheckForWinner();
            }
        }

        [RelayCommand]
        private void SubtractAkaCat1()
        {
            if (AkaCat1Count > 0)
            {
                AkaCat1Count--;
            }
        }

        [RelayCommand]
        private void SubtractAkaCat2()
        {
            if (AkaCat2Count > 0)
            {
                AkaCat2Count--;
            }
        }

        [RelayCommand]
        private void SubtractAoCat1()
        {
            if (AoCat1Count > 0)
            {
                AoCat1Count--;
            }
        }

        [RelayCommand]
        private void SubtractAoCat2()
        {
            if (AoCat2Count > 0)
            {
                AoCat2Count--;
            }
        }
    }
}