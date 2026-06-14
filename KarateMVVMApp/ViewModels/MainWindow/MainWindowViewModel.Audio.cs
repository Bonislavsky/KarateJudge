using Avalonia.Platform;
using NAudio.Wave;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KarateMVVMApp.ViewModels
{
    public partial class MainWindowViewModel
    {
        private CancellationTokenSource? _soundCts;

        public async Task PlayAudioAsync(string filePath)
        {
            if (!OperatingSystem.IsWindows())
                return;

            if (_isPlaying) 
                return;
            _isPlaying = true;

            try
            {
                var assetStream = AssetLoader.Open(new Uri(filePath));

                if (assetStream == null)
                {
                    _isPlaying = false;
                    return;
                }

                var audioStream = new Mp3FileReader(assetStream);
                var audioPlayer = new WaveOutEvent();

                audioPlayer.Init(audioStream);

                _audioStream = audioStream;
                _audioPlayer = audioPlayer;

                audioPlayer.PlaybackStopped += (s, e) =>
                {
                    CleanupAudio(audioPlayer);
                };

                audioPlayer.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Audio error: " + ex.Message);
                _isPlaying = false;
                CleanupAudio();
            }
        }

        public void StopAudio()
        {
            if (!OperatingSystem.IsWindows())
                return;

            try
            {
                CleanupAudio();
            }
            catch
            {
                CleanupAudio();
            }
        }

        public void PauseAudio()
        {
            if (OperatingSystem.IsWindows() && _isPlaying)
            {
                _audioPlayer?.Pause();
            }
        }

        public void ResumeAudio()
        {
            if (OperatingSystem.IsWindows() && _isPlaying)
            {
                _audioPlayer?.Play();
            }
        }

        private void CleanupAudio(IWavePlayer? expectedPlayer = null)
        {
            if (expectedPlayer != null && !ReferenceEquals(_audioPlayer, expectedPlayer))
                return;

            var audioPlayer = _audioPlayer;
            var audioStream = _audioStream;

            _audioPlayer = null;
            _audioStream = null;
            _isPlaying = false;

            try
            {
                audioPlayer?.Stop();
                audioPlayer?.Dispose();
                audioStream?.Dispose();
            }
            catch { }
        }

        private async void PlayWinnerSound(string path)
        {
            _soundCts?.Cancel();
            _soundCts?.Dispose();
            _soundCts = new CancellationTokenSource();

            var token = _soundCts.Token;

            StopAudio();

            try
            {
                await PlayAudioAsync(path);
                await Task.Delay(3000, token);
                StopAudio();
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}
