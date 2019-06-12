using WMPLib;

namespace App.Visualization
{
    public class AudioPlayer
    {
        private readonly WindowsMediaPlayer wmp;
        public AudioPlayer(string currentMusic)
        {
            wmp = new WindowsMediaPlayer
            {
                URL = currentMusic
            };
        }

        public void Play()
        {
            wmp.settings.volume = 100;
            wmp.controls.play();
        }

        public void Stop()
        {
            wmp.controls.stop();
        }

        public void ChangeMusic(string newMusic)
        {
            wmp.URL = newMusic;
        }
    }
}