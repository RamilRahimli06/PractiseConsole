using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAudioService
    {
        void Play();
        void Next();
        void VolumeUp();
        void VolumeDown();
        void Stop();
    }
}
