using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IVoiceService
    {
        void Speak(string text);
        string Listen();
    }
}
