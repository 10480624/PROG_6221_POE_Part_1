using System;
using System.IO;
using System.Media;
using System.Runtime.Versioning; // Needed for the attribute

namespace Raphael
{
    /// <summary>
    /// Plays the startup voice greeting. Only works on Windows because it uses SoundPlayer.
    /// If the file's missing or broken, it just prints a message and moves on – no crashes.
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal class AudioHelper
    {
        public string GreetingPath { get; set; }

        public AudioHelper()
        {
            // Look for the WAV file in the same folder as the .exe
            GreetingPath = Path.Combine(AppContext.BaseDirectory, "greeting.wav");
        }

        public void PlayGreeting()
        {
            // First check if the file even exists – no point trying if it doesn't
            if (!File.Exists(GreetingPath))
            {
                Console.WriteLine("[System]: Greeting audio not found. Continuing without sound...");
                return;
            }

            try
            {
                using SoundPlayer player = new SoundPlayer(GreetingPath);
                player.PlaySync(); // Blocks until audio finishes – fine for startup
            }
            catch (InvalidOperationException)
            {
                // Usually means the WAV format is weird (compressed, not PCM). App keeps going.
                Console.WriteLine("[System]: Audio playback error. Continuing without sound...");
            }
        }
    }
}
