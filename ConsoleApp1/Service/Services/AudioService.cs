using NAudio.Wave;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    using NAudio.Wave;

    public class AudioService : IAudioService
    {
        private WaveOutEvent _output;
        private AudioFileReader _file;

        private List<string> _playlist;
        private int _currentIndex = 0;

        public AudioService()
        {
            _playlist = new List<string>
        {
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Music", "music1.mp3"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Music", "music2.mp3"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Music", "music3.mp3")
        };
        }

        public void Play()
        {
            Stop();

            _file = new AudioFileReader(_playlist[_currentIndex]);
            _output = new WaveOutEvent();

            _output.Init(_file);
            _output.Play();

            Console.WriteLine("Music playing...");
        }

        public void Next()
        {
            _currentIndex++;

            if (_currentIndex >= _playlist.Count)
                _currentIndex = 0;

            Play();
        }

        public void VolumeUp()
        {
            if (_file != null)
                _file.Volume = Math.Min(_file.Volume + 0.1f, 1.0f);
        }

        public void VolumeDown()
        {
            if (_file != null)
                _file.Volume = Math.Max(_file.Volume - 0.1f, 0.0f);
        }

        public void Stop()
        {
            _output?.Stop();
            _file?.Dispose();
            _output?.Dispose();

            _output = null;
            _file = null;
        }
    }
}
