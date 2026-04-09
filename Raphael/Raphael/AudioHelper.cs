using System;
using System.IO;
using System.Media;

namespace Raphael
{
    /// <summary>
    /// Handles WAV playback for the chatbot startup greeting.
    /// </summary>
    internal class AudioHelper
    {
        public string GreetingPath { get; set; }

        public AudioHelper()
        {
            GreetingPath = Path.Combine(AppContext.BaseDirectory, "greeting.wav");
        }

        public void PlayGreeting()
        {
            if (!File.Exists(GreetingPath))
            {
                Console.WriteLine("[System]: Greeting audio not found. Continuing...");
                return;
            }

            try
            {
                using SoundPlayer player = new SoundPlayer(GreetingPath);
                player.PlaySync();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("[System]: Audio playback error. Continuing...");
            }
        }
    }
}