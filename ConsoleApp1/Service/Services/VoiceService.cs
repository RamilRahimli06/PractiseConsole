using System.Globalization;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace ConsoleApp1.Services
{
    public class VoiceService
    {
        private readonly SpeechSynthesizer _synth;
        private readonly SpeechRecognitionEngine _recog;

        public VoiceService()
        {
           
            _synth = new SpeechSynthesizer();
            _synth.Volume = 100;
            _synth.Rate = 0;

   
            try
            {
                _recog = new SpeechRecognitionEngine(new CultureInfo("en-US"));
            }
            catch
            {
                _recog = new SpeechRecognitionEngine();
            }

            _recog.SetInputToDefaultAudioDevice();
            _recog.LoadGrammar(new DictationGrammar());
        }

        public void Speak(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            
            _synth.SpeakAsync(text);
        }

        public string Listen()
        {
            Console.WriteLine("Danış ...");

            try
            {
                var result = _recog.Recognize();
                return result?.Text ?? "";
            }
            catch
            {
                return "";
            }
        }
    }
}